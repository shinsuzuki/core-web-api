using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using mvc_api.Models.Response;
using mvc_api.Util.Logger;
using System.Net;
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

            if (!context.ModelState.IsValid)
            {
                // 自動モデルバインダーフィルターを無効、自分でレスポンスを作成
                this.SetBadRequestResponse(context);
            }
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogDebug(this.AccessInfo(context) + "OnActionExecuted");
        }

        private void SetBadRequestResponse(ActionExecutingContext context)
        {
            var errorResponse = new ErrorResponse();

            context.ModelState.ToList().ForEach(argument =>
            {
                if (argument.Value?.ValidationState == ModelValidationState.Invalid)
                {
                    argument.Value?.Errors.ToList().ForEach(error =>
                    {
                        errorResponse.AddErrorList(HttpStatusCode.BadRequest, "400100", error.ErrorMessage);
                    });
                }
            });

            context.Result = new BadRequestObjectResult(errorResponse);
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
