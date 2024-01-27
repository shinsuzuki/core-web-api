using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using mvc_api.Base;
using mvc_api.Models.Response;
using mvc_api.Util.Logger;
using mvc_api.Filter;
using System.Xml.Linq;
using System.Security.Claims;
using mvc_api.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using mvc_api.Config;

namespace mvc_api.Controllers.v1
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HogeController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMyConfig _myConfig;

        public HogeController(IConfiguration configuration, ILoggerManager logger, IHttpContextAccessor httpContextAccessor,
            IMyConfig myConfig)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _myConfig = myConfig;
        }

        [HttpGet("AnotherRole")]
        [Authorize(Roles = "AnotherRole")]
        public IActionResult AnotherRole()
        {
            _logger.LogDebug(this.ActionInfo() + "Hoge ====> AnotherRole" + _configuration["MyTest:Value"]);
            return Ok();
        }

        [HttpGet("NoRole")]
        public IActionResult NoRole()
        {
            _logger.LogDebug(this.ActionInfo() + "Hoge ====> NoRole");
            return Ok();
            //return Ok(new { test="999", message="test-message" });
        }

        [HttpGet("AdminRole")]
        [Authorize(Roles = "admin")]
        public IActionResult AdminOnly()
        {
            _logger.LogDebug(this.ActionInfo() + "Hoge ====> AdminOnly");
            return Ok();
        }

        [HttpGet("CheckParams")]
        public IActionResult CheckParams(string name, string city)
        {
            _logger.LogDebug(this.ActionInfo() + $"Hoge ====> CheckParams, {name} - {city}");
            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpGet("CheckAdminPolicy")]
        public IActionResult CheckAdminPolicy()
        {
            _logger.LogDebug(this.ActionInfo() + "Hoge ====> CheckAdminPolicy");
            return Ok();
        }

        [HttpGet("Person")]
        public IActionResult Person()
        {
            _logger.LogTrace(this.ActionInfo() + "Hoge ====> Person");
            _logger.LogDebug(this.ActionInfo() + "Hoge ====> Person");
            _logger.LogInfo(this.ActionInfo() + "Hoge ====> Person");
            _logger.LogWarn(this.ActionInfo() + "Hoge ====> Person");
            _logger.LogError(this.ActionInfo() + "Hoge ====> Person");
            _logger.LogFatal(this.ActionInfo() + "Hoge ====> Person");

            // cookie発行TEST
            //HttpContext.Response.Cookies.Append("actionPersonCookie1", "person_abc123", new CookieOptions { Path="/", HttpOnly=false});
            //HttpContext.Response.Cookies.Append("actionPersonCookie2", "person_abc123");
            //HttpContext.Response.Cookies.Append("actionPersonCookie3", "person_abc123");
            //HttpContext.Response.Cookies.Append("actionPersonCookie3", "person_abc123");

            // HttpContextを使いたい
            // https://stayg.jpn.org/wp/?page_id=254
            // https://stackoverflow.com/questions/37371264/invalidoperationexception-unable-to-resolve-service-for-type-microsoft-aspnetc
            var s = new PersonService(_httpContextAccessor, _myConfig);
            Debug.WriteLine(s.GetLoginName());

            return this.ToResult<ResponsePerson>(new()
            {
                Status = 200,
                fullName = "sato",
                Old = 20
            });
        }

        [HttpGet("TestException")]
        public IActionResult TestException()
        {
            throw new Exception("myException!");
            return Ok();
        }

    }
}
