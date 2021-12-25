using AutoMapper;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using MediatR;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeamApi.Application.Refit;
using TeamApi.Domain.Models;
using TeamApi.Domain.Repositories;
using TeamApi.DTOs;

namespace TeamApi.Application.Commands
{
    public class AddTeam
    {
        public class Command : IRequest
        {
            public Command(string name, Guid managerUserId, IEnumerable<Guid> users, string activityField, string description, string teamLogo)
            {
                Name = name;
                ManagerUserId = managerUserId;
                Users = users;
                ActivityField = activityField;
                Description = description;
                TeamLogo = teamLogo;
            }

            [Required]
            public string Name { get; }

            [Required]
            public Guid ManagerUserId { get; }

            public IEnumerable<Guid> Users { get; }

            public string ActivityField { get; }

            public string Description { get; }

            public string TeamLogo { get; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IMapper _mapper;
            private readonly ITeamUnitOfWork _unitOfWork;
            private readonly ITeamRepository _teamRepository;
            private readonly ITeamUserRepository _teamUserRepository;
            private readonly EventBusRabbitMQProducer _eventBus;

            public Handler(IMapper mapper, ITeamUnitOfWork unitOfWork, EventBusRabbitMQProducer eventBus)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _teamRepository = unitOfWork.TeamRepository;
                _teamUserRepository = unitOfWork.TeamUserRepository;
                _eventBus = eventBus;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var team = await _teamRepository.AddAsync(_mapper.Map<Team>(request));

                if(request.Users.Count() > 0 && request.Users != null)
                {
                    foreach (var item in request.Users)
                    {
                        await _teamUserRepository.AddAsync(new TeamUser()
                        {
                            TeamId = team.Id,
                            UserId = item
                        });
                    }
                }

                //Add ManagerUserId to TeamUsers
                if (!request.Users.Contains(request.ManagerUserId))
                {
                    await _teamUserRepository.AddAsync(new TeamUser()
                    {
                        TeamId = team.Id,
                        UserId = team.ManagerUserId
                    });
                }

                await _unitOfWork.CommitAsync();

                #region Add Team Logo

                if (request.TeamLogo != null && request.TeamLogo.StartsWith("data:image"))
                {
                    var fileExtention = request.TeamLogo.Split(",")[0].Split("/")[1].Split(";")[0];
                    byte[] imageBytes = Convert.FromBase64String(request.TeamLogo.Split(",")[1]);

                    _eventBus.PublishTeamLogo(EventBusConstants.UploadTeamLogoQueue, new UploadTeamLogoEvent()
                    {
                        TeamId = team.Id,
                        ImageBytes = imageBytes,
                        FileExtention = fileExtention
                    });
                }

                #endregion Add Team Logo

                return Unit.Value;
            }
        }
    }
}