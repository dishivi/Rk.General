using Base.Webapi.Attributes;
using Base.Webapi.Auth;
using Base.Webapi.Interface;
using Core.ServeHttp.Interface;
using Core.ServeHttp.Service;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IAuthenticationProcess, AuthenticationProcess>();
builder.Services.AddSingleton<AuthItAttribute>();
builder.Services.AddSingleton<IDefaultHttpService, DefaultHttpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();

