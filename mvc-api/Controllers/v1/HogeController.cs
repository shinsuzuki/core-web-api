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

        public HogeController(IConfiguration configuration, ILoggerManager logger)
        {
            _configuration = configuration;
            _logger = logger;
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
            // cookie発行TEST
            //HttpContext.Response.Cookies.Append("actionPersonCookie1", "person_abc123", new CookieOptions { Path="/", HttpOnly=false});
            //HttpContext.Response.Cookies.Append("actionPersonCookie2", "person_abc123");
            //HttpContext.Response.Cookies.Append("actionPersonCookie3", "person_abc123");
            //HttpContext.Response.Cookies.Append("actionPersonCookie3", "person_abc123");

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
