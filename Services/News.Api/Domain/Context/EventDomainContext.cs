using Microsoft.EntityFrameworkCore;
using NewsApi.Domain.Models;

namespace NewsApi.Domain.Context
{
    public class EventDomainContext : DbContext
    {
        public EventDomainContext(DbContextOptions<EventDomainContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Models.News> News { get; set; }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //UserConfiguration.ModelBuilder(modelBuilder);
        }
    }
}