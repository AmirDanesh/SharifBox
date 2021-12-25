using SharifBox.Repository;
using UserApi.Domain.Context;

namespace UserApi.Domain.Repositories
{
    public interface IUserUnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        public IUserSkillRepository UserSkillRepository { get; }

        public ISkillRepository SkillRepository { get; }
    }

    public class UserUnitOfWork : UnitOfWork<UserDomainContext>, IUserUnitOfWork
    {
        public UserUnitOfWork(UserDomainContext context) : base(context)
        {
        }

        private IUserRepository _userRepository;
        private IUserSkillRepository _userSkillRepository;
        private ISkillRepository _SkillRepository;

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(Context);

        public IUserSkillRepository UserSkillRepository => _userSkillRepository ??= new UserSkillRepository(Context);

        public ISkillRepository SkillRepository => _SkillRepository ??= new SkillRepository(Context);
    }
}