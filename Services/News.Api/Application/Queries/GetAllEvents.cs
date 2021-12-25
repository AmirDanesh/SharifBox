using AutoMapper;
using MediatR;
using NewsApi.Domain.Repositories;
using NewsApi.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsApi.Application.Queries
{
    public class GetAllEvents
    {
        public class Query : IRequest<List<EventDTO>>
        {
            public Query()
            {
            }
        }

        public class Handler : IRequestHandler<Query, List<EventDTO>>
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

            public async Task<List<EventDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var events = await _eventRepository.GetAllAsync();

                return _mapper.Map<List<EventDTO>>(events);
            }
        }
    }
}