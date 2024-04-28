using Microsoft.AspNetCore.Mvc;
using Shoporium.Business.Services;
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
        private readonly IAWSService _azureService;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(
            IConfiguration configuration,
            IAWSService azureService,
            ShoporiumContext context,
            ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _context = context;
            _azureService = azureService;
            _configuration = configuration;
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFiles()
        {
            var fromFiles = Request.Form.Files;

            if (fromFiles == null || fromFiles.Count == 0)
                return BadRequest("No files uploaded.");

            var containerName = _configuration["AWSBucketName"];

            foreach (var file in fromFiles)
            {
                if (file.Length == 0)
                {
                    // Optionally, handle the case where a file has no content
                    continue;
                }

                await _azureService.UploadBlobAsync(containerName!, file.FileName, file);
            }

            return Ok("Files uploaded successfully.");
        }
    }
}