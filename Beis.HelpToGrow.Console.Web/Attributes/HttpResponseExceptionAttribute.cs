namespace Beis.HelpToGrow.Console.Web.Attributes
{
    using Beis.HelpToGrow.Console.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class HttpResponseExceptionAttribute : ActionFilterAttribute
    {
        public HttpResponseExceptionAttribute()
        {
            this.Order = int.MaxValue - 10;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.Status
                };

                context.ExceptionHandled = true;
            }
        }
    }
}