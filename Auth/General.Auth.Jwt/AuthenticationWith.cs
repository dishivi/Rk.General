using Core.Authentication.Interfaces;
using Core.Authentication.Models;
using Core.ExceptionHandler.ExceptionHandler;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Auth.Jwt
{
    public class AuthenticationWith : IAuthenticationWith
    {
        public readonly IConfiguration Configuration;

        public AuthenticationWith(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateToken(RequestAuthentication claims)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue))),
                Expires = claims.ExpiryDate,
                Issuer = Configuration["Jwt:Issuer"],
                Audience = Configuration["Jwt:Issuer"],
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ResponseAuthentication ValidateToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]));

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var response = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);

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
            catch (SecurityTokenExpiredException ex)
            {
                throw AuthenticationExceptionHandler.RaiseTokenExpiredException();
            }
            catch (Exception ex)
            {
                throw AuthenticationExceptionHandler.RaiseInvalidAPIKeyException();
            }
        }

        public async Task<ResponseAuthentication> ValidateGoogleToken(string token)
        {
            GoogleJsonWebSignature.ValidationSettings settings =
                new GoogleJsonWebSignature.ValidationSettings();

            settings.Audience =
                new List<string>() { Configuration["Google:ClientId"] };

            GoogleJsonWebSignature.Payload payload = await
                GoogleJsonWebSignature.ValidateAsync(token, settings);

            return new ResponseAuthentication
            {
                Claims = new List<AuthenticationClaim>
                {
                    new AuthenticationClaim { ClaimType = "Email", ClaimValue = payload.Email },
                    new AuthenticationClaim { ClaimType = "JwtId", ClaimValue = payload.JwtId }
                }
            };
        }
    }
}