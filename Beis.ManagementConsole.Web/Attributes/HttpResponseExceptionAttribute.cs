using Beis.ManagementConsole.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Beis.ManagementConsole.Web.Attributes
{
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