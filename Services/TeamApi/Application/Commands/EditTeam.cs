using AutoMapper;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeamApi.Application.Queries;
using TeamApi.Domain.Models;
using TeamApi.Domain.Repositories;
using TeamApi.DTOs;

namespace TeamApi.Application.Commands
{
    public class EditTeam
    {
        public class Command : IRequest
        {
            public Command(Guid id, string name, Guid managerUserId, IEnumerable<Guid> users, string activityField, string description, string teamLogo)
            {
                Id = id;
                Name = name;
                ManagerUserId = managerUserId;
                Users = users;
                ActivityField = activityField;
                Description = description;
                TeamLogo = teamLogo;
            }

            [Required]
            public Guid Id { get; set; }

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
            private readonly IMediator _mediator;
            private readonly ITeamRepository _teamRepository;
            private readonly ITeamUserRepository _teamUserRepository;
            private readonly EventBusRabbitMQProducer _eventBus;

            public Handler(IMapper mapper, ITeamUnitOfWork unitOfWork, IMediator mediator, EventBusRabbitMQProducer eventBus)
            {
                _unitOfWork = unitOfWork;
                _mediator = mediator;
                _mapper = mapper;
                _teamRepository = unitOfWork.TeamRepository;
                _teamUserRepository = unitOfWork.TeamUserRepository;
                _eventBus = eventBus;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentTeam = await _teamRepository.GetAllIncluding(x => x.TeamDetails).FirstOrDefaultAsync(x => x.Id == request.Id);
                if (currentTeam == null)
                    throw new KeyNotFoundException("تیم یافت نشد");
                var newTeam = _mapper.Map(request, currentTeam);
                newTeam = await _teamRepository.UpdateAsync(newTeam, request.Id);

                //Change TeamManagerUser in TeamUser Table
                if (currentTeam.ManagerUserId != newTeam.ManagerUserId)
                {
                    var preManager = await _teamUserRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == currentTeam.ManagerUserId);
                    _teamUserRepository.Delete(preManager);
                    await _teamUserRepository.AddAsync(new TeamUser()
                    {
                        TeamId = newTeam.Id,
                        UserId = newTeam.ManagerUserId
                    });
                }


                var teamUsers = await _teamUserRepository.FindAllAsync(x => x.TeamId == request.Id);

                var teamUsersToDelete = teamUsers.Where(x => !request.Users.Contains(x.UserId))
                                                 .ToList();

                var teamUsersToAdd = request.Users.Where(x => !teamUsers.Any(s => s.UserId == x))
                                                    .Select(s => new TeamUser() { TeamId = request.Id, UserId = s })
                                                    .ToList();

                teamUsersToDelete.ForEach(item => _teamUserRepository.Delete(item));

                teamUsersToAdd.ForEach(async item => await _teamUserRepository.AddAsync(item));

                await _unitOfWork.CommitAsync();

                if (request.TeamLogo != null && request.TeamLogo.StartsWith("data:image"))
                {
                    var fileExtention = request.TeamLogo.Split(",")[0].Split("/")[1].Split(";")[0];
                    byte[] imageBytes = Convert.FromBase64String(request.TeamLogo.Split(",")[1]);

                    _eventBus.PublishTeamLogo(EventBusConstants.UploadTeamLogoQueue, new UploadTeamLogoEvent()
                    {
                        TeamId = request.Id,
                        ImageBytes = imageBytes,
                        FileExtention = fileExtention
                    });
                }

                return Unit.Value;
            }
        }
    }
}