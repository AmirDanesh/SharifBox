using SharifBox.Repository;
using UserApi.Domain.Context;
using UserApi.Domain.Models.User;

namespace UserApi.Domain.Repositories
{
    public interface ISkillRepository : IGenericRepository<UserDomainContext, Skill>
    {
    }

    public class SkillRepository : GenericRepository<UserDomainContext, Skill>, ISkillRepository
    {
        private readonly UserDomainContext _context;

        public SkillRepository(UserDomainContext context)
            : base(context)
        {
            _context = context;
        }
    }
}