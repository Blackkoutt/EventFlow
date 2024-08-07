using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowAPI.Controllers
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
        private readonly IAdditionalServicesService _additionalServicesService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAdditionalServicesService additionalServicesService )
        {
            _additionalServicesService = additionalServicesService;
            _logger = logger;
        }

        [HttpGet(Name = "GetAdditionalServices")]
        public async Task <IEnumerable<IResponseDto>> Get()
        {
            return await _additionalServicesService.GetAllAsync();
           /* return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();*/
        }
    }
}
