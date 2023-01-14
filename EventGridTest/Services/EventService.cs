using Azure.Messaging.EventGrid;

namespace EventGridTest.Services
{
    public class EventService
    {
        private readonly IConfiguration _configuration;
        private readonly EventGridPublisherClient _eventGridPublisherClient;
        public EventService(IConfiguration configuration, EventGridPublisherClient eventGridPublisherClient)
        {
            _configuration = configuration;
            _eventGridPublisherClient = eventGridPublisherClient;
        }

        public async Task<Azure.Response> DeliverEvent(string topic, string eventType, string subject, object data)
        {
            var egEvent = new EventGridEvent(subject, eventType, "1.0", data)
            { 
                Topic = topic, 
                Id = "Id-2", 
                EventTime = DateTimeOffset.UtcNow 
            };

            return await _eventGridPublisherClient.SendEventAsync(egEvent);            
        }
    }
}
