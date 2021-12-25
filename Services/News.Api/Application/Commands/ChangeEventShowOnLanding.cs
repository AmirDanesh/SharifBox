using MediatR;
using NewsApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsApi.Application.Commands
{
    public class ChangeEventShowOnLanding
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid eventId, bool showOnLanding)
            {
                EventId = eventId;
                ShowOnLanding = showOnLanding;
            }

            public Guid EventId { get; }

            public bool ShowOnLanding { get; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IEventUnitOfWork _unitOfWork;
            private readonly IEventRepository _eventRepository;

            public Handler(IEventUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _eventRepository = unitOfWork.EventRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var ev = await _eventRepository.GetAsync(request.EventId);
                ev.ShowOnLanding = request.ShowOnLanding;
                ev = await _eventRepository.UpdateAsync(ev, request.EventId);
                await _unitOfWork.CommitAsync();
                return Unit.Value;
            }
        }
    }
}