using Base.Webapi.Attributes;
using Core.Authentication.Interfaces;
using Core.Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Rk.Web.Api.Models;

namespace Rk.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        public IAuthenticationWith Auth { get; }

        public LoginController(IAuthenticationWith auth)
        {
            Auth = auth;
        }

        [HttpGet(nameof(Login))]
        [GenerateToken]
        public IActionResult Login()
        {
            BaseRequest identity = new();

            //Authenticate user here

            return new OkObjectResult(
                new RequestAuthentication
                {
                    ExpiryDate = DateTime.Now.AddSeconds(10),
                    Claims = new List<AuthenticationClaim>
                    {
                        new AuthenticationClaim
                        {
                            ClaimType = nameof(identity.Identity_Id),
                            ClaimValue = Guid.NewGuid().ToString(),
                        },
                        new AuthenticationClaim
                        {
                            ClaimType =  nameof(identity.Identity_Name),
                            ClaimValue = "rk"
                        },new AuthenticationClaim
                        {
                            ClaimType = nameof(identity.CompanyId),
                            ClaimValue = Guid.NewGuid().ToString(),
                        },
                    }
                });
        }

        [HttpPost(nameof(LoginByGoogle))]
        public async Task<IActionResult> LoginByGoogle([FromBody] string tokenId)
        {
            var claims = await Auth.ValidateGoogleToken(tokenId);

            //if success, db call and get the required claims and pass to generate token

            BaseRequest identity = new();

            return new OkObjectResult(
                Auth.GenerateToken(new RequestAuthentication
                {
                    ExpiryDate = DateTime.Now.AddSeconds(10),
                    Claims = new List<AuthenticationClaim>
                    {
                        new AuthenticationClaim
                        {
                            ClaimType = nameof(identity.Identity_Id),
                            ClaimValue = Guid.NewGuid().ToString(),
                        },
                        new AuthenticationClaim
                        {
                            ClaimType =  nameof(identity.Identity_Name),
                            ClaimValue = "rk"
                        },new AuthenticationClaim
                        {
                            ClaimType = nameof(identity.CompanyId),
                            ClaimValue = Guid.NewGuid().ToString(),
                        },
                    }
                }));
        }
    }
}
