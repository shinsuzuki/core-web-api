using Microsoft.AspNetCore.Http.Extensions;
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
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerName = controllerActionDescriptor!.ControllerName;
            var actionName = controllerActionDescriptor!.ActionName;
            var userName = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            _logger.LogDebug($"{controllerName},{actionName},{userName},OnActionExecuting");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerName = controllerActionDescriptor!.ControllerName;
            var actionName = controllerActionDescriptor!.ActionName;
            var userName = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            _logger.LogDebug($"{controllerName},{actionName},{userName},OnActionExecuted");
        }

    }
}
