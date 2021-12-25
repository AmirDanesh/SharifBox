using FileAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI.Domain.Models
{
    public class FileInformation
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extention { get; set; }
        public double Size { get; set; }
        public FileCategory FileCategory { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public Guid UploaderUserId { get; set; }
    }
}
