using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Asp.Versioning;
using mvc_api.Models.Request;
using mvc_api.Util.Logger;
using mvc_api.Filter;
using mvc_api.Base;

namespace mvc_api.Controllers.v1
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [TypeFilter(typeof(GlobalExceptionFilter))]
    public class AccountController : ApiControllerBase
    {
        private readonly ILoggerManager _logger;

        public AccountController(ILoggerManager logger)
        {
            _logger = logger;   
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        //public async Task<IActionResult> Login(string loginName, string password)
        public async Task<IActionResult> Login(RequestUser? user)
        {
            _logger.LogDebug(this.ActionInfo() + "login");

            //ログイン認証判定
            if (true)
            {
                // サインインに必要なプリンシパルを作る
                //var claims = new[] { new Claim(ClaimTypes.Name, user?.LoginName) };
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user?.LoginName));
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                claims.Add(new Claim(ClaimTypes.Role, "user"));
                claims.Add(new Claim(ClaimTypes.Role, "light_user"));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // 認証クッキーをレスポンスに追加
                await HttpContext.SignInAsync(principal);

                return Ok();

            }

            return BadRequest("ログイン認証 == false");
        }


        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogDebug(this.ActionInfo() + "logout");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
