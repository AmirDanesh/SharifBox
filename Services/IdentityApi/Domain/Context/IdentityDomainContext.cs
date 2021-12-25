using IdentityApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace IdentityApi.Domain.Context
{
    public class IdentityDomainContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public IdentityDomainContext(DbContextOptions<IdentityDomainContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Models.User.IdentityUser> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<PhoneNumberVerifyCode> PhoneNumberVerifyCodes { get; set; }
    }
}