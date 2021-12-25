using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Events
{
    public class UploadTeamLogoEvent
    {
        public Guid TeamId { get; set; }

        public byte[] ImageBytes { get; set; }

        public string FileExtention { get; set; }
    }
}