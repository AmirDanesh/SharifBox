using SharifBox.Repository;
using UserApi.Domain.Context;
using UserApi.Domain.Models.User;

namespace UserApi.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<UserDomainContext, User>
    {
    }

    public class UserRepository : GenericRepository<UserDomainContext, User>, IUserRepository
    {
        private readonly UserDomainContext _context;

        public UserRepository(UserDomainContext context)
            : base(context)
        {
            _context = context;
        }
    }
}