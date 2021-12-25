using SharifBox.Repository;
using TeamApi.Domain.Context;
using TeamApi.Domain.Models;

namespace TeamApi.Domain.Repositories
{
    public interface ITeamRepository : IGenericRepository<TeamDomainContext, Team>
    {
    }

    public class TeamRepository : GenericRepository<TeamDomainContext, Team>, ITeamRepository
    {
        private readonly TeamDomainContext _context;

        public TeamRepository(TeamDomainContext context)
            : base(context)
        {
            _context = context;
        }
    }
}