using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using mvc_api.Util.Logger;
using System.Security.Claims;

namespace mvc_api.Filter
{
    public class GlobalActionFilter : IActionFilter
    {
        private readonly ILoggerManager _logger;


        public GlobalActionFilter(ILoggerManager logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogDebug(this.AccessInfo(context) + "OnActionExecuting");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogDebug(this.AccessInfo(context) + "OnActionExecuted");
        }

        private string AccessInfo(FilterContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerName = controllerActionDescriptor!.ControllerName;
            var actionName = controllerActionDescriptor!.ActionName;
            var userName = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return $"Controller={controllerName},Action={actionName},User={userName},";
        }
    }
}
