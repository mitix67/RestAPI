using Microsoft.AspNetCore.Mvc;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherForecastServicee _service;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastServicee service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromQuery]int take, [FromQuery]int tempMax, [FromQuery]int tempMin)
        {
            var result = _service.Get().Where(a => a.TemperatureC <= tempMax && a.TemperatureC >= tempMin).Take(take);
            return result;
        }

        [HttpPost("generate")]
        public ActionResult<string> Post([FromQuery] int numberOfResults, [FromBody]TemperatureRequest temperatureRequest)
        {
            //HttpContext.Response.StatusCode = 401;

            //return StatusCode(401, $"Hello {name}");

            if (numberOfResults <= 0 || temperatureRequest.TempMax <= temperatureRequest.TempMin)
                return BadRequest("Invalid parameters");
            var result =  _service.Post(numberOfResults, temperatureRequest.TempMin, temperatureRequest.TempMax);

            return Ok(result);
        }
    }
}
