using NewsApi.Domain.Context;
using SharifBox.Repository;

namespace NewsApi.Domain.Repositories
{
    public interface INewsRepository : IGenericRepository<EventDomainContext, Models.News>
    {
    }

    public class NewsRepository : GenericRepository<EventDomainContext, Models.News>, INewsRepository
    {
        private readonly EventDomainContext _context;

        public NewsRepository(EventDomainContext context)
            : base(context)
        {
            _context = context;
        }
    }
}