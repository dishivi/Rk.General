using Base.Webapi.Interface;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Base.Webapi.Attributes
{
    public class AuthItAttribute : ActionFilterAttribute
    {
        private readonly IAuthenticationProcess authProcess;

        public AuthItAttribute(IAuthenticationProcess authProcess)
        {
            this.authProcess = authProcess;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            authProcess.DoingAuth(context);

            base.OnActionExecuting(context);
        }
    }
}
