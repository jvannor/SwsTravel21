using Microsoft.AspNetCore.Mvc;

namespace ChannelAdapter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        // Methods
        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Life")]
        public ActionResult<string> GetLife()
        {
            var message = $"Application alive at {DateTimeOffset.Now}.";
            _logger.LogInformation(message);
            return message;
        }

        [HttpGet("Ready")]
        public ActionResult<string> GetReady()
        {
            var message = $"Application ready at {DateTimeOffset.Now}.";
            _logger.LogInformation(message);
            return message;
        }

        [HttpGet("Start")]
        public ActionResult<string> GetStart()
        {
            var message = $"Application started at {DateTimeOffset.Now}.";
            _logger.LogInformation(message);
            return message;
        }

        // Fields
        private readonly ILogger<HealthController> _logger;
    }
}
