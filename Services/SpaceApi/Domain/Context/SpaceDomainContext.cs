using Microsoft.EntityFrameworkCore;
using SpaceApi.Domain.Models;

namespace SpaceApi.Domain.Context
{
    public class SpaceDomainContext : DbContext
    {
        public SpaceDomainContext(DbContextOptions<SpaceDomainContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Space> Spaces { get; set; }

        public DbSet<RentAbleChair> RentAblesChairs { get; set; }

        public DbSet<RentAbleRoom> RentAbleRooms { get; set; }

        public DbSet<RentAbleConferenceRoom> RentAbleConferenceRooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //UserConfiguration.ModelBuilder(modelBuilder);
        }
    }
}