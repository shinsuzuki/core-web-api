using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using mvc_api.Filter;
using mvc_api.Util.Logger;
using NLog;

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
            var provider = services.BuildServiceProvider();
            var loggerManager = provider.GetRequiredService<ILoggerManager>();

            services.AddControllers(config =>
            {
                config.Filters.Add(new GlobalActionFilter(loggerManager));
            });
        }

        /// <summary>
        /// ロガー設定
        /// </summary>
        public static void ConfigureLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }


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
