using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EventFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost("notify")]
        public async Task<IActionResult> Post()
        {
            Log.Information("Hello");
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                Log.Information(body);
            }
            return Ok();
        }
    }
}
