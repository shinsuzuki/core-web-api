using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Asp.Versioning;
using mvc_api.Models.Request;

namespace mvc_api.Controllers.v1
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("Login")]
        //public async Task<IActionResult> Login(string loginName, string password)
        public async Task<IActionResult> Login(User? user)
        {
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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
