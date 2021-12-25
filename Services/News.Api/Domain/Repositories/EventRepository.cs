using NewsApi.Domain.Context;
using NewsApi.Domain.Models;
using SharifBox.Repository;

namespace NewsApi.Domain.Repositories
{
    public interface IEventRepository : IGenericRepository<EventDomainContext, Event>
    {
    }

    public class EventRepository : GenericRepository<EventDomainContext, Event>, IEventRepository
    {
        private readonly EventDomainContext _context;

        public EventRepository(EventDomainContext context)
            : base(context)
        {
            _context = context;
        }
    }
}