using IdentityApi.Domain.Context;
using SharifBox.Repository;

namespace IdentityApi.Domain.Repositories
{
    public interface IIdentityUnitOfWork : IUnitOfWork
    {
        public GenericRepository<IdentityDomainContext, Domain.Models.User.IdentityUser> UserRepository { get; }
    }

    public class IdentityUnitOfWork : UnitOfWork<IdentityDomainContext>, IIdentityUnitOfWork
    {
        public IdentityUnitOfWork(IdentityDomainContext context) : base(context)
        {
        }

        private GenericRepository<IdentityDomainContext, Domain.Models.User.IdentityUser> _userRepository;

        public GenericRepository<IdentityDomainContext, Domain.Models.User.IdentityUser> UserRepository => _userRepository ??= new GenericRepository<IdentityDomainContext, Domain.Models.User.IdentityUser>(Context);
    }
}