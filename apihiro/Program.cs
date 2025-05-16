using apihiro.Db;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
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


        // Add logging services
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole(); // Enables console logging
        builder.Services.AddDbContext<WebContext>((p,options) =>
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
    }
}