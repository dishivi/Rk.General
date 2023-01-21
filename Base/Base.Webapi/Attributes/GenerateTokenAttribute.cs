using Core.Authentication.Interfaces;
using Core.Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Base.Webapi.Attributes
{
    public class GenerateTokenAttribute : ActionFilterAttribute
    {
        public IAuthenticationWith Auth;

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (Auth == null)
            {
                Auth = (IAuthenticationWith)context.HttpContext.RequestServices.GetService(typeof(IAuthenticationWith));
            }

            var response = context.Result as OkObjectResult;
            if (response != null && response.Value != null)
            {
                var claims = (RequestAuthentication)response.Value;

                var token = Auth.GenerateToken(claims);

                context.HttpContext.Response.Headers.Add("jwt-token", token);

                context.HttpContext.Response.Body = new MemoryStream();
            }

            base.OnResultExecuting(context);
        }
    }
}
