using Core.Authentication.Interfaces;
using Core.ExceptionHandler;
using Core.ExceptionHandler.ExceptionHandler;
using Core.ServeHttp.Interface;
using General.Models.Application.Response;
using General.Models.Auth;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;

namespace Core.Auth.Jwt
{
    public class AuthenticationWith : IAuthenticationWith
    {
        private readonly IOptions<List<LoginProviderConfiguration>> _loginProviders;
        private readonly IOptions<JwtConfigurationModel> _jwtConfiguration;
        private readonly IDefaultHttpService _httpService;

        public AuthenticationWith(IOptions<List<LoginProviderConfiguration>> loginProviders,
            IOptions<JwtConfigurationModel> jwtConfiguration,
            IDefaultHttpService httpService)
        {
            _loginProviders = loginProviders;
            _jwtConfiguration = jwtConfiguration;
            _httpService = httpService;
        }

        public string GenerateToken(RequestAuthentication claims)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfiguration.Value.Key));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue))),
                Expires = claims.ExpiryDate,
                Issuer = _jwtConfiguration.Value.Issuer,
                Audience = _jwtConfiguration.Value.Issuer,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ResponseAuthentication ValidateToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfiguration.Value.Key));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var response = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwtConfiguration.Value.Issuer,
                    ValidAudience = _jwtConfiguration.Value.Issuer,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);

                if (response is null)
                    throw AuthenticationExceptionHandler.RaiseUnauthorizedUserException();

                string[] systemClaims = new string[]
                {
                    "nbf", "exp", "iat", "iss", "aud"
                };

                return new ResponseAuthentication
                {
                    Claims = response.Claims.Where(x => !systemClaims.Contains(x.Type)).Select(x => new AuthenticationClaim
                    {
                        ClaimType = x.Type,
                        ClaimValue = x.Value
                    }).ToList()
                };
            }
            catch (SecurityTokenExpiredException ex) { throw AuthenticationExceptionHandler.RaiseTokenExpiredException(); }
            catch (CustomExceptionHandler ex) { throw; }
            catch (Exception ex) { throw AuthenticationExceptionHandler.RaiseInvalidAPIKeyException(); }
        }

        public async Task<ResponseAuthentication> ValidateGoogleToken(string token)
        {
            var config = _loginProviders.Value.SingleOrDefault(x => x.Name == "Google");

            if (config == null)
                return null;

            GoogleJsonWebSignature.ValidationSettings settings =
                new GoogleJsonWebSignature.ValidationSettings();

            settings.Audience =
                new List<string>() { config.ClientId };

            GoogleJsonWebSignature.Payload payload = await
                GoogleJsonWebSignature.ValidateAsync(token, settings);

            return new ResponseAuthentication
            {
                Claims = new List<AuthenticationClaim>
                {
                    new AuthenticationClaim { ClaimType = "Email", ClaimValue = payload.Email },
                    new AuthenticationClaim { ClaimType = "UserId", ClaimValue = payload.JwtId }
                }
            };
        }

        public async Task<ResponseAuthentication> ValidateFacebookToken(string token)
        {
            var config = _loginProviders.Value.SingleOrDefault(x => x.Name == "Facebook");

            if (config == null)
                return null;

            var debugApiResponse = await _httpService.GetAsync("Facebook", new General.Models.Http.BaseHttpRequest
            {
                Url = $"debug_token?input_token={token}&access_token={config.ClientId}|{config.ClientSecret}"
            });

            if (debugApiResponse.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            var debugApiContent = await debugApiResponse.Content.ReadFromJsonAsync<ResponseFacebookLogin>();

            if (!debugApiContent.Data.Is_Valid)
                return null;

            var responseUserDetail = await _httpService.GetAsync("Facebook", new General.Models.Http.BaseHttpRequest
            {
                Url = $"fields=first_name,last_name,picture,email&access_token={token}"
            });

            if (responseUserDetail.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            var userDetail = await responseUserDetail.Content.ReadFromJsonAsync<ResponseGetFacebookUserDetail>();

            return new ResponseAuthentication
            {
                Claims = new List<AuthenticationClaim>
                {
                    new AuthenticationClaim { ClaimType = "Email", ClaimValue = userDetail.Email },
                    new AuthenticationClaim { ClaimType = "UserId", ClaimValue = userDetail.Id }
                }
            };
        }
    }
}