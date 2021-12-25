using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeamApi.Application.Refit;
using TeamApi.Domain.Repositories;
using TeamApi.DTOs;

namespace TeamApi.Application.Queries
{
    public class GetTeamById
    {
        public class Query : IRequest<TeamDTO>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }
        }

        public class Handler : IRequestHandler<Query, TeamDTO>
        {
            private readonly IMapper _mapper;
            private readonly ITeamUnitOfWork _unitOfWork;
            private readonly ITeamRepository _teamRepository;

            public Handler(IMapper mapper, ITeamUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _teamRepository = unitOfWork.TeamRepository;
            }

            public async Task<TeamDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var team = await _teamRepository
                .GetAllIncluding(x => x.TeamUsers, s => s.TeamDetails)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

                var dto = _mapper.Map<TeamDTO>(team);
                dto.Users = new List<KeyValuePair<Guid, string>>();
                var refit = RestService.For<IUserAPI>(Startup.Configuration["refitLinks:userService"]);
                var managerName = await refit.GetUserName(dto.ManagerUser.Key);
                var usersName = await refit.GetUsersName(team.TeamUsers.Select(x => x.UserId).ToList());

                dto.ManagerUser = new KeyValuePair<Guid, string>(dto.ManagerUser.Key, managerName.FirstName + " " + managerName.LastName);

                foreach (var item in usersName)
                {
                    dto.Users.Add(new KeyValuePair<Guid, string>(item.Id, item.Name));
                }

                return dto;
            }
        }
    }
}