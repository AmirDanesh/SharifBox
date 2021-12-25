using Microsoft.EntityFrameworkCore;
using TeamApi.Domain.Models;

namespace TeamApi.Domain.Context
{
    public class TeamDomainContext : DbContext
    {
        public TeamDomainContext(DbContextOptions<TeamDomainContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamUser> TeamUsers { get; set; }

        public DbSet<TeamDetails> TeamDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            TeamConfiguration.ModelBuilder(modelBuilder);
        }
    }
}