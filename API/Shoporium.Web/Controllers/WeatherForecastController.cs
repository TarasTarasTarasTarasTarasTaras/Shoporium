using Microsoft.AspNetCore.Mvc;
using Shoporium.Data._EntityFramework;

namespace Shoporium.Web.Controllers
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
        private readonly ShoporiumContext _context;

        public WeatherForecastController(ShoporiumContext context, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _context = context;
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

        [HttpGet("test")]
        public ActionResult Test()
        {
            // run it
            _context.ProductCategories.First(i => i.Id == 847).Name = "В'язка тварин";
            _context.SaveChanges();

            return Ok(_context.ProductCategories.Skip(10).Take(50).ToList());
        }
    }
}