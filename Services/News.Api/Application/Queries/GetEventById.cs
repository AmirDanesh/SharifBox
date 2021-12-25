using AutoMapper;
using MediatR;
using NewsApi.Domain.Repositories;
using NewsApi.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewsApi.Application.Queries
{
    public class GetEventById
    {
        public class Query : IRequest<EventDTO>
        {
            public Query(Guid eId)
            {
                EventId = eId;
            }

            public Guid EventId { get; }
        }

        public class Handler : IRequestHandler<Query, EventDTO>
        {
            private readonly IMapper _mapper;
            private readonly IEventUnitOfWork _unitOfWork;
            private readonly IEventRepository _eventRepository;

            public Handler(IMapper mapper, IEventUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _eventRepository = unitOfWork.EventRepository;
            }

            public async Task<EventDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var ev = await _eventRepository.FindAsync(x => x.Id == request.EventId);

                return _mapper.Map<EventDTO>(ev);
            }
        }
    }
}