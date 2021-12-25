using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeamApi.Application.Refit;
using TeamApi.Domain.Repositories;
using TeamApi.DTOs;

namespace TeamApi.Application.Queries
{
    public class GetAllTeams
    {
        public class Query : IRequest<List<TeamListDTO>>
        {
            public Query()
            {
            }
        }

        public class Handler : IRequestHandler<Query, List<TeamListDTO>>
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

            public async Task<List<TeamListDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var teams = await _teamRepository.GetAllIncluding(x => x.TeamUsers).ToListAsync();
                var refit = RestService.For<IUserAPI>(Startup.Configuration["refitLinks:userService"]);
                var managerNames = await refit.GetUsersName(teams.Select(x => x.ManagerUserId).ToList());

                var dto = new List<TeamListDTO>();

                foreach (var item in teams)
                {
                    dto.Add(new TeamListDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ManagerName = managerNames.FirstOrDefault(x => x.Id == item.ManagerUserId).Name,
                        NumberOfUsers = item.TeamUsers.Count()
                    });
                }

                return dto;
            }
        }
    }
}