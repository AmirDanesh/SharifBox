using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI.Domain.Models
{
    public class ProfilePictures
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FileInformationId { get; set; }
        public FileInformation FileInformation { get; set; }
    }
}
