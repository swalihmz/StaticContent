using EventGridTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventGridTest.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly EventService _eventService;

        public EventController(EventService eventService, ILogger<EventController> logger)
        {
            _logger = logger;
            _eventService = eventService;
        }

        [HttpPost("topic/{topic}/eventType/{eventType}")]
        public IActionResult Post(string topic, string eventType, [FromQuery]string subject, [FromBody]object eventData)
        {
            _eventService.DeliverEvent(topic, eventType, subject, eventData);

            return Ok();
        }
    }
}