using Base.Webapi.Attributes;
using Core.Authentication.Interfaces;
using Core.ExceptionHandler.ExceptionHandler;
using General.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Rk.General.Utility.Common;
using Rk.Infrastructure.Contracts;
using Rk.Infrastructure.Repositories;
using Rk.Web.Api.Application.Request;
using Rk.Web.Api.Models;

namespace Rk.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ITimesheetUserRepository _timesheetUserRepository;
        public IAuthenticationWith Auth { get; }

        public LoginController(IAuthenticationWith auth, ITimesheetUserRepository timesheetUserRepository)
        {
            Auth = auth;
            _timesheetUserRepository = timesheetUserRepository;
        }

        [HttpGet(nameof(Login))]
        //[GenerateToken]
        public async Task<IActionResult> Login([FromBody] RequestLogin request)
        {
            BaseRequest identity = new();

            var detail = await _timesheetUserRepository.ValidateUserAuthAsync(request.Username, request.Password, request.TenantId.ToGuid() ?? Guid.Empty);
            if (detail is null) throw AuthenticationExceptionHandler.RaiseIncorrectUserNameOrPasswordException();

            //Authenticate user here

            var claims = new RequestAuthentication
            {
                ExpiryDate = DateTime.Now.AddSeconds(10),
                Claims = new List<AuthenticationClaim>
                    {
                        new AuthenticationClaim
                        {
                            ClaimType = nameof(identity.Identity_Id),
                            ClaimValue = detail.Id.ToString(),
                        },
                        new AuthenticationClaim
                        {
                            ClaimType =  nameof(identity.Identity_Name),
                            ClaimValue = detail.Name
                        },new AuthenticationClaim
                        {
                            ClaimType = nameof(identity.Tenant_Id),
                            ClaimValue = detail.TenantId.ToString(),
                        },
                    }
            };

            string token = Auth.GenerateToken(claims);

            Response.Headers.Append("jwt-token", token);

            return new OkResult();
        }

        [HttpPost(nameof(LoginByGoogle))]
        public async Task<IActionResult> LoginByGoogle([FromBody] string token)
        {
            var claims = await Auth.ValidateGoogleToken(token);

            //if success, db call and get the required claims and pass to generate jwt token

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

        [HttpPost(nameof(LoginByFacebook))]
        public async Task<IActionResult> LoginByFacebook([FromBody] string token)
        {
            var claims = await Auth.ValidateFacebookToken(token);

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
