using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using mvc_api.Util.Logger;

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
            _logger.LogDebug($"{context.HttpContext.Request.Method},{context.HttpContext.Request.Path},OnActionExecuting");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogDebug($"{context.HttpContext.Request.Method},{context.HttpContext.Request.Path},OnActionExecuted");
        }

    }
}
