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

    }
}
