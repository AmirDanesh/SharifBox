using AutoMapper;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using MediatR;
using NewsApi.Domain.Models;
using NewsApi.Domain.Repositories;
using NewsApi.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace NewsApi.Application.Commands
{
    public class AddEvent
    {
        public class Command : IRequest<EventDTO>
        {
            public Command(string title, string content, string startDate, string endDate, bool showOnLanding, string eventBanner)
            {
                Title = title;
                Content = content;
                StartDate = startDate;
                EndDate = endDate;
                ShowOnLanding = showOnLanding;
                EventBanner = eventBanner;
            }

            [Required]
            public string Title { get; }

            [Required]
            public string Content { get; }

            public string StartDate { get; }

            public string EndDate { get; }

            [Required]
            public bool ShowOnLanding { get; }

            public string EventBanner { get; }
        }

        public class Handler : IRequestHandler<Command, EventDTO>
        {
            private readonly IMapper _mapper;
            private readonly IEventUnitOfWork _unitOfWork;
            private readonly IEventRepository _eventRepository;
            private readonly EventBusRabbitMQProducer _eventBus;

            public Handler(IMapper mapper, IEventUnitOfWork unitOfWork, EventBusRabbitMQProducer eventBus)
            {
                _eventBus = eventBus;
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _eventRepository = unitOfWork.EventRepository;
            }

            public async Task<EventDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var addEvent = _mapper.Map<Event>(request);

                if(addEvent.StartDate > addEvent.EndDate)
                    throw new ArgumentException("تاریخ پایان باید بزرگتر از تاریخ شروع باشد");

                addEvent = await _eventRepository.AddAsync(addEvent);
                await _unitOfWork.CommitAsync();

                if (request.EventBanner != null && request.EventBanner.StartsWith("data:image"))
                {
                    var fileExtention = request.EventBanner.Split(",")[0].Split("/")[1].Split(";")[0];
                    byte[] imageBytes = Convert.FromBase64String(request.EventBanner.Split(",")[1]);

                    _eventBus.PublishEventBanner(EventBusConstants.UploadEventBannerQueue, new UploadEventBannerEvent()
                    {
                        EventId = addEvent.Id,
                        ImageBytes = imageBytes,
                        FileExtention = fileExtention
                    });
                }

                return _mapper.Map<EventDTO>(addEvent);
            }
        }
    }
}