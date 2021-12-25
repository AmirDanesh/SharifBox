using IdentityApi.Domain.Context;
using SharifBox.Repository;

namespace IdentityApi.Domain.Repositories
{
    public interface IIdentityUserRepository : IGenericRepository<IdentityDomainContext, Models.User.IdentityUser>
    {
    }

    public class IdentityUserRepository : GenericRepository<IdentityDomainContext, Models.User.IdentityUser>, IIdentityUserRepository
    {
        private readonly IdentityDomainContext _context;

        public IdentityUserRepository(IdentityDomainContext context)
            : base(context)
        {
            _context = context;
        }
    }
}