using AutoMapper;
using MediatR;
using News.Api.DTOs;
using NewsApi.Domain.Repositories;
using NewsApi.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsApi.Application.Queries
{
    public class GetAllEventsForLanding
    {
        public class Query : IRequest<List<EventsForLanding>>
        {
            public Query()
            {
            }
        }

        public class Handler : IRequestHandler<Query, List<EventsForLanding>>
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

            public async Task<List<EventsForLanding>> Handle(Query request, CancellationToken cancellationToken)
            {
                var events = await _eventRepository.FindAllAsync(x => x.ShowOnLanding == true && x.StartDate <= DateTime.UtcNow && x.EndDate >= DateTime.UtcNow);

                return _mapper.Map<List<EventsForLanding>>(events);
            }
        }
    }
}