using Base.Webapi.Interface;
using Core.Authentication.Interfaces;
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
            var token = context.HttpContext.Request.Headers.Authorization.ToString();

            if (!string.IsNullOrEmpty(token))
            {
                if (token.StartsWith("Bearer "))
                {
                    token = token.Replace("Bearer ", String.Empty);
                }

                var response = _auth.ValidateToken(token);
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
}
