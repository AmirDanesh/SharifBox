using NewsApi.Domain.Context;
using SharifBox.Repository;

namespace NewsApi.Domain.Repositories
{
    public interface IEventUnitOfWork : IUnitOfWork
    {
        public IEventRepository EventRepository { get; }

        public INewsRepository NewsRepository { get; }
    }

    public class EventUnitOfWork : UnitOfWork<EventDomainContext>, IEventUnitOfWork
    {
        public EventUnitOfWork(EventDomainContext context) : base(context)
        {
        }

        private IEventRepository _eventRepository;
        private INewsRepository _newsRepository;

        public IEventRepository EventRepository => _eventRepository ??= new EventRepository(Context);

        public INewsRepository NewsRepository => _newsRepository ??= new NewsRepository(Context);
    }
}