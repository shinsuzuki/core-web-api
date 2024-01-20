using Microsoft.AspNetCore.Mvc.Filters;

namespace mvc_api.Filter
{
    public class GlobalActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            System.Diagnostics.Debug.WriteLine("====> OnActionExecuting");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            System.Diagnostics.Debug.WriteLine("====> OnActionExecuted");
        }

    }
}
