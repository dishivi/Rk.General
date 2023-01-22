using General.Models.Auth;
using General.Models.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Polly;
using System.Text;

namespace General.Auth.Jwt
{
    public static class AuthenticationServices
    {
        public static void RegisterAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<List<LoginProviderConfiguration>>(configuration.GetSection("LoginProvider"));
            services.Configure<JwtConfigurationModel>(configuration.GetSection("Jwt"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            var clients = new List<HttpClientConfiguration>();
            configuration.GetSection("HttpClients").Bind(clients);

            foreach (var client in clients)
            {
                services.AddHttpClient(client.ClientName, httpClient =>
                {
                    httpClient.BaseAddress = new Uri(client.BaseAddress);

                    foreach (var header in client.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }).AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.WaitAndRetryAsync(
                        client.RetryCount, _ => TimeSpan.FromMilliseconds(client.RetryDurationInMiliSeconds)));
            }
        }
    }
}
