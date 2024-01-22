using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc_api.Base;
using mvc_api.Models.Response;
using mvc_api.Util.Logger;

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
            System.Diagnostics.Debug.WriteLine("Hoge/List ====> AnotherRole"+ _configuration["MyTest:Value"]);
            return Ok();
        }

        [HttpGet("NoRole")]
        public IActionResult NoRole()
        {
            System.Diagnostics.Debug.WriteLine("Hoge/List ====> NoRole");
            return Ok();
        }

        [HttpGet("AdminRole")]
        [Authorize(Roles = "admin")]
        public IActionResult AdminOnly()
        {
            System.Diagnostics.Debug.WriteLine("Hoge/List ====> AdminOnly");
            return Ok();
        }

        [HttpGet("CheckParams")]
        public IActionResult CheckParams(string name, string city)
        {
            System.Diagnostics.Debug.WriteLine($"====> {name} - {city}");
            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpGet("CheckAdminPolicy")]
        public IActionResult CheckAdminPolicy()
        {
            System.Diagnostics.Debug.WriteLine("====> CheckAdminPolicy");
            return Ok();
        }


        [HttpGet("Person")]
        public IActionResult Person()
        {
            return this.ToResult<Person>(new Person { fullName = "sato", Old = 20 });
        }

    }
}
