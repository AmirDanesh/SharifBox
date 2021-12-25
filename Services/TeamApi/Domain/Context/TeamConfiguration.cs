using Microsoft.EntityFrameworkCore;
using TeamApi.Domain.Models;

namespace TeamApi.Domain.Context
{
    public class TeamConfiguration
    {
        public static void ModelBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamUser>().HasKey(e => new { e.TeamId, e.UserId });
        }
    }
}