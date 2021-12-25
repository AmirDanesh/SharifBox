using FileAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI.Domain.Context
{
    public class FileDomainContext : DbContext
    {
        public FileDomainContext(DbContextOptions<FileDomainContext> options) : base(options)
        {
        }

        public DbSet<FileInformation> FileInformation { get; set; }
        public DbSet<ProfilePictures> ProfilePictures { get; set; }
    }
}
