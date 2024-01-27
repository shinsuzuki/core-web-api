using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using mvc_api.Config;
using mvc_api.Filter;
using mvc_api.Models.Response;
using mvc_api.Util.Logger;
using NLog;
using System.Net;
using System.Text.Json;

namespace mvc_api.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// CORS設定
        /// </summary>
        public static void ConfigureCors(this IServiceCollection services) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder
                        //.WithOrigins("https://example.com")   < 制御
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            });
        }


        /// <summary>
        /// 認証設定
        /// </summary>
        public static void ConfigureAuthentication(this IServiceCollection services) 
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie((options) =>
                {
                    options.SlidingExpiration = true;
                    options.Events.OnRedirectToLogin = cxt =>
                    {
                        cxt.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToAccessDenied = cxt =>
                    {
                        cxt.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToLogout = cxt => Task.CompletedTask;
                });
        }


        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole(new[] { "admin" }));
            });
        }


        /// <summary>
        /// APIバージョン管理
        /// </summary>
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
            });
        }


        /// <summary>
        /// Swagger設定
        /// </summary>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "sample api",
                    Description = "テスト用プロジェクトサンプルAPI",
                });
            });
        }

        /// <summary>
        /// フィルター設定
        /// </summary>
        public static void ConfigureFilter(this IServiceCollection services)
        {
            //var provider = services.BuildServiceProvider();
            //var loggerManager = provider.GetRequiredService<ILoggerManager>();

            services.AddControllers(config =>
            {
                config.Filters.Add<GlobalActionFilter>();
                // todo グローバルの例外はフィルターが使いやすい
                // https://www.herlitz.io/2019/05/05/global-exception-handling-asp.net-core/
                config.Filters.Add<GlobalExceptionFilter>(); 
            });
        }

        /// <summary>
        /// ロガー設定
        /// </summary>
        public static void ConfigureLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>
        /// APIの振る舞い設定
        /// </summary>
        public static void ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // 自動的な400の動作を無効にする
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        /// <summary>
        /// 設定ファイル
        /// </summary>
        public static void ConfigureMyConfig(this IServiceCollection services)
        {
            services.AddSingleton<IMyConfig, MyConfig>();
        }

        ///// <summary>
        ///// グローバル例外
        ///// </summary>
        //public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        //{
        //    app.UseExceptionHandler(appError =>
        //    {
        //        appError.Run(async context =>
        //        {
        //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //            context.Response.ContentType = "application/json";
        //            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        //            if (contextFeature != null)
        //            {
        //                logger.LogError($"Something went wrong: {contextFeature.Error}");

        //                var errorResponse = new ErrorResponse();
        //                errorResponse.AddErrorList(HttpStatusCode.InternalServerError, "500100", contextFeature.Error.ToString());

        //                //await context.Response.WriteAsync(new ErrorDetails()
        //                //{
        //                //    StatusCode = context.Response.StatusCode,
        //                //    Message = "Internal Server Error.",
        //                //}.ToString());

        //                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        //            }
        //        });
        //    });
        //}

        ///// <summary>
        ///// IIS設定
        ///// </summary>
        //public static void ConfigureIISIntegration(this IServiceCollection services) =>
        //    services.Configure<IISOptions>(options =>
        //    {

        //    });


        ///// <summary>
        ///// リポジトリ設定
        ///// </summary>
        //public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        //    services.AddScoped<IRepositoryManager, RepositoryManager>();

        ///// <summary>
        ///// サービスマネージャ
        ///// </summary>
        //public static void ConfigureServiceManager(this IServiceCollection services) =>
        //    services.AddScoped<IServiceManager, ServiceManager>();

        ///// <summary>
        ///// SQLサーバー設定
        ///// </summary>
        //public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        //    services.AddDbContext<RepositoryContext>(opts =>
        //    opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    }
}
