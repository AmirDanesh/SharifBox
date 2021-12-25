using SharifBox.Repository;
using TeamApi.Domain.Context;
using TeamApi.Domain.Models;

namespace TeamApi.Domain.Repositories
{
    public interface ITeamUserRepository : IGenericRepository<TeamDomainContext, TeamUser>
    {
    }

    public class TeamUserRepository : GenericRepository<TeamDomainContext, TeamUser>, ITeamUserRepository
    {
        private readonly TeamDomainContext _context;

        public TeamUserRepository(TeamDomainContext context)
            : base(context)
        {
            _context = context;
        }
    }
}