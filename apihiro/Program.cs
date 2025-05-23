using apihiro.Business;
using apihiro.Controllers;
using apihiro.Db;
using apihiro.Models.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.Configure<RouteOptions>(options =>
        {
            options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            options.LowercaseUrls = true;

        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddSwaggerGen();

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperProfile>();
        });
        var mapper = configuration.CreateMapper();
        builder.Services.AddSingleton(mapper);

#if DEBUG
        //configuration.AssertConfigurationIsValid();
#endif

        // use DI (http://docs.automapper.org/en/latest/Dependency-injection.html) or create the mapper yourself


        // Add logging services
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole(); // Enables console logging
        builder.Services.AddDbContext<WebContext>((p, options) =>
        {
            //options.EnableSensitiveDataLogging();
            //options.LogTo(Console.WriteLine, LogLevel.Information);
            var constr = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(constr);
            //options.UseSqlServer(constr, sqlOptions =>
            //{
            //    sqlOptions.MigrationsAssembly("apihiromigration");
            //});
        });

        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                        //("https://localhost:7282") // Allowed origins
                              .AllowAnyMethod() // Allow all HTTP methods
                              .AllowAnyHeader(); // Allow all headers
                    });
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCors(MyAllowSpecificOrigins); // Apply the policy
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller:slugify}/{action=Index}/{id?}"
        );
        app.MapControllers();
        app.Run();
    }
}