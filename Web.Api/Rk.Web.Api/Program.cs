using Base.Webapi.Application.Request.Validator;
using Core.Auth.Jwt;
using Core.Authentication.Interfaces;
using General.Auth.Jwt;
using General.Models.Auth;
using General.Models.Database;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rk.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var dbProvider = builder.Configuration.GetSection("DatabaseProvider");
if (dbProvider?.Value is not null && dbProvider["DatabaseProvider:Name"] == "SQL_Server")
{
    builder.Services.AddDbContext<TimesheetContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseProvider:TenantConnectionString")));
}

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));

builder.Services.RegisterAuthServices(builder.Configuration);

builder.Services.AddSingleton<IAuthenticationWith, AuthenticationWith>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
