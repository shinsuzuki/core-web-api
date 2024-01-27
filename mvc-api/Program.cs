using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using mvc_api.Extensions;
using mvc_api.Util.Logger;
using mvc_api.Config;
using Microsoft.AspNetCore.Mvc.Filters;
using mvc_api.Filter;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace mvc_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // service
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.ConfigureMyConfig();
            builder.Services.ConfigureLogger();
            builder.Services.ConfigureCors();
            builder.Services.ConfigureAuthentication();
            builder.Services.ConfigureAuthorization();
            builder.Services.ConfigureApiVersioning();
            builder.Services.ConfigureFilter();
            builder.Services.ConfigureApiBehaviorOptions();
            builder.Services.ConfigureSwagger();

            // HttpContextAccessorを各クラスへDIできるようにします→ 同等のコード：services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddHttpContextAccessor();




            // app, Configure the HTTP request pipeline.
            var app = builder.Build();

            // todo 設定ファイル読み込みは不要？ 自動で読まれているみたい？
            //var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            //System.Diagnostics.Debug.WriteLine("=========> " + config["Logging:LogLevel:Default"]);
            //System.Diagnostics.Debug.WriteLine("=========> " + config.GetValue<int>("MyTest:Value"));


            // ここでcookieを設定
            //app.Use(async (context, next) =>
            //{
            //    var cookieOptions = new CookieOptions()
            //    {
            //        Path = "/",
            //        Expires = DateTimeOffset.UtcNow,
            //        IsEssential = true,
            //        HttpOnly = true,
            //        Secure = true,
            //    };
            //    context.Response.Cookies.Append("myCookie", "cookieValue", cookieOptions);
            //    await next();
            //});
            


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();                   // Swaggerミドルウェアを追加
                app.UseSwaggerUI();                 // 静的ファイルミドルウェアを有効化
                app.UseDeveloperExceptionPage();    // 開発者例外ページを有効化
            } 
            else
            {
                app.UseHsts();                  // HTTPの代わりにHTTPSを使用するよう指示
            }

            app.UseCors("CorsPolicy");          // CORSを有効化
            app.UseStaticFiles();               // wwwrootに対して静的コンテンツサービスを登録
            app.UseHttpsRedirection();          // 強制的に HTTP 要求を HTTPS へリダイレクトします

            // todo グローバルの例外処理はフィルターが使い勝手が良い
            // https://www.herlitz.io/2019/05/05/global-exception-handling-asp.net-core/
            //app.UseExceptionHandler("/error");  // 例外処理、UseHttpsRedirectionの後？

            app.UseAuthentication();            // 認証を有効化
            app.UseAuthorization();             // 認可を有効化
            app.MapControllers();               // 属性ルーティング コントローラーがマップされます
            app.Run();
        }
    }
}
