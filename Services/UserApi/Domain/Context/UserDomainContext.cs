using Microsoft.EntityFrameworkCore;
using UserApi.Domain.Models.User;

namespace UserApi.Domain.Context
{
    public class UserDomainContext : DbContext
    {
        public UserDomainContext(DbContextOptions<UserDomainContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<UserSkill> UserSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserConfiguration.ModelBuilder(modelBuilder);
        }
    }
}