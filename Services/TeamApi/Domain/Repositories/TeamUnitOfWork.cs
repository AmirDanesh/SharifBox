using SharifBox.Repository;
using TeamApi.Domain.Context;

namespace TeamApi.Domain.Repositories
{
    public interface ITeamUnitOfWork : IUnitOfWork
    {
        public ITeamRepository TeamRepository { get; }

        public ITeamUserRepository TeamUserRepository { get; }
    }

    public class TeamUnitOfWork : UnitOfWork<TeamDomainContext>, ITeamUnitOfWork
    {
        public TeamUnitOfWork(TeamDomainContext context) : base(context)
        {
        }

        private ITeamRepository _teamRepository;
        private ITeamUserRepository _teamUserRepository;

        public ITeamRepository TeamRepository => _teamRepository ??= new TeamRepository(Context);

        public ITeamUserRepository TeamUserRepository => _teamUserRepository ??= new TeamUserRepository(Context);
    }
}