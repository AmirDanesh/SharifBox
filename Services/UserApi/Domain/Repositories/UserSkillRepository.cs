using SharifBox.Repository;
using UserApi.Domain.Context;
using UserApi.Domain.Models.User;

namespace UserApi.Domain.Repositories
{
    public interface IUserSkillRepository : IGenericRepository<UserDomainContext, UserSkill>
    {
    }

    public class UserSkillRepository : GenericRepository<UserDomainContext, UserSkill>, IUserSkillRepository
    {
        private readonly UserDomainContext _context;

        public UserSkillRepository(UserDomainContext context)
            : base(context)
        {
            _context = context;
        }
    }
}