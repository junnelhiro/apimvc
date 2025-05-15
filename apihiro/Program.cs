using apihiro.Db;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen();

var configuration = new MapperConfiguration(cfg => 
{
    cfg.AddProfile<MapperProfile>(); 
});

#if DEBUG
//configuration.AssertConfigurationIsValid();
#endif

// use DI (http://docs.automapper.org/en/latest/Dependency-injection.html) or create the mapper yourself
var mapper = configuration.CreateMapper();


var constr = builder.Configuration.GetConnectionString("DefaultConnection");
// Add logging services
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Enables console logging


builder.Services.AddSqlServer<WebContext>(constr, options =>
{
    //optionsBuilder.EnableSensitiveDataLogging();
    //options.LogTo(Console.WriteLine, LogLevel.Information);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
