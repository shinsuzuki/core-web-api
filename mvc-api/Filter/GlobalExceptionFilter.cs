using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection.Metadata;
using System.Security.Claims;
using NLog;
using mvc_api.Util.Logger;

namespace mvc_api.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        ILoggerManager _logger; 

        public GlobalExceptionFilter(ILoggerManager logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            try
            {
                var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var name = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                _logger.LogError(
                    $"Controller:{controllerActionDescriptor!.ControllerName} " +
                    $"Action:{controllerActionDescriptor.ActionName} " +
                    $"User:{(name ?? "No User")} " +
                    "予期せぬ例外が発生しました。" + Environment.NewLine +
                    "************************************************" + Environment.NewLine +
                    $"{context.Exception}" + Environment.NewLine +
                    "************************************************"
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError("\r\n" + "ログ出力時にエラーが発生しました。" + ex);
            }
        }
    }
}
