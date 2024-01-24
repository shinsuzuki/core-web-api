using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using mvc_api.Models.Response;
using mvc_api.Util.Logger;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;

namespace mvc_api.Controllers
{
    // todo グローバルの例外はフィルターが使いやすい
    // https://www.herlitz.io/2019/05/05/global-exception-handling-asp.net-core/
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error(ILoggerManager logger)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var errorResponse = new ErrorResponse(HttpStatusCode.InternalServerError);

#if DEBUG
            errorResponse.AddError("500100", context?.Error?.Message ?? string.Empty);
#else
            errorResponse.AddError("500100", "Internal Server Error");
#endif

            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }

    }
}
