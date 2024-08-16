using Base.Webapi.Interface;
using Core.Authentication.Interfaces;
using Core.ExceptionHandler.ExceptionHandler;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Base.Webapi.Auth
{
    public class AuthenticationProcess : IAuthenticationProcess
    {
        private readonly IAuthenticationWith _auth;

        public AuthenticationProcess(IAuthenticationWith auth)
        {
            _auth = auth;
        }

        public void DoingAuth(ActionExecutingContext context)
        {
            string? token = context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(token) || !token.StartsWith("Bearer "))
                throw AuthenticationExceptionHandler.RaiseUnauthorizedUserException();

            token = token.Replace("Bearer ", String.Empty);

            var response = _auth.ValidateToken(token);

            if(response is null)
                throw AuthenticationExceptionHandler.RaiseUnauthorizedUserException();

            if (response != null)
            {
                foreach (var claim in response.Claims)
                {
                    context.HttpContext.Request.Headers.Add(claim.ClaimType, claim.ClaimValue);
                }
            }
        }
    }
}
