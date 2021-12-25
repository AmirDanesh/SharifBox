using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Events
{
    public class UploadUserProfilePictureEvent
    {
        public Guid IdentityUserId { get; set; }

        public byte[] ImageBytes { get; set; }

        public string FileExtention { get; set; }
    }
}