using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace mvc_api.Base
{
    public class ApiControllerBase : ControllerBase
    {
        protected string ActionInfo()
        {
            var controllerName = this.ControllerContext.ActionDescriptor.ControllerName;
            var actionName = this.ControllerContext.ActionDescriptor.ActionName;
            var userName = this.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return $"Controller={controllerName},Action={actionName},User={userName},";
        }

        protected IActionResult ToResult<T>(T res)
        {
            return new JsonResult(
                res,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
# if DEBUG
                    WriteIndented = true,
# endif
                });

        }


    }
}
