using apihiro.Db;
using apihiro.Models.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace apihiro.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("api/[controller]")]
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

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 20).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpPost()]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<MyModel>> PostModel([FromForm] MyModel model)
        {
            var emp = map.Map<Employee>(model);
            context.Add(emp);
            await context.SaveChangesAsync();
            var data = context.Employees
                .Find(emp.Id);
            if (model.UploadedFile != null && model.UploadedFile.Length != 0)
            {
                if (!Directory.Exists("Uploads"))
                {
                    Directory.CreateDirectory("Uploads");
                }
                var filePath = Path.Combine("Uploads", model.UploadedFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.UploadedFile.CopyToAsync(stream);
                }
            }
            return Ok(data);
        }

        [HttpGet("All")]
        public ActionResult<List<MyModel>> GetAll([FromQuery] Guid? Id)
        {
            if (Id != null)
            {
                var data1 = context.Employees
                    .Where(x => x.Id == Id)
               .OrderBy(x => x.Name)
               .ToList();
                return Ok(data1);

            }
            var emps = context.Employees.ToList();
            var data = context.Employees
                 .ProjectTo<MyModel>(map.ConfigurationProvider)
                 .ToList();
            return Ok(data);
        }
    }

    public class MyModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public IFormFile UploadedFile { get; set; }

    }
}
