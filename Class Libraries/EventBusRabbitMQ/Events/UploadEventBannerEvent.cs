using System;

namespace EventBusRabbitMQ.Events
{
    public class UploadEventBannerEvent
    {
        public Guid EventId { get; set; }

        public byte[] ImageBytes { get; set; }

        public string FileExtention { get; set; }
    }
}