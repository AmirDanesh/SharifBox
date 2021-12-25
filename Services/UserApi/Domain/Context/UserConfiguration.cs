using Microsoft.EntityFrameworkCore;
using UserApi.Domain.Models.User;

namespace UserApi.Domain.Context
{
    public class UserConfiguration
    {
        public static void ModelBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSkill>().HasKey(e => new { e.SkillId, e.UserId });
            modelBuilder.Entity<Domain.Models.User.User>().HasIndex(x => x.IdentityUserId);
        }
    }
}