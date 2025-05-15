using apihiro.Db;
using apihiro.Models.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apihiro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WebContext context;
        private readonly IMapper map;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            WebContext context,
            IMapper map)
        {
            _logger = logger;
            this.context = context;
            this.map = map;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost()]
        public ActionResult<MyModel> PostModel([FromBody] MyModel model)
        {
            var emp = map.Map<Employee>(model);
            context.Add(emp);
            context.SaveChanges();

            var configuration = new MapperConfiguration(cfg =>
                     cfg.CreateProjection<Employee, MyModel>());
            var data = context.Employees.ToList();
            return Ok(data);
        }
    }

    public class MyModel
    {
        public string Name { get; set; }
        public string Address  { get; set; }
    }
}
