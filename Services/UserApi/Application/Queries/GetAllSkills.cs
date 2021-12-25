using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserApi.Domain.Repositories;
using UserApi.DTOs;

namespace UserApi.Application.Queries
{
    public class GetAllSkills
    {
        public class Query : IRequest<List<SkillDropDownDTO>>
        {
        }

        public class QueryHandler : IRequestHandler<Query, List<SkillDropDownDTO>>
        {
            private readonly IMapper _mapper;
            private readonly IUserUnitOfWork _unitOfWork;
            private readonly ISkillRepository _skillRepository;

            public QueryHandler(IMapper mapper, IUserUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _skillRepository = unitOfWork.SkillRepository;
            }

            public async Task<List<SkillDropDownDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var skills = await _skillRepository.GetAllAsync();

                return _mapper.Map<List<SkillDropDownDTO>>(skills);
            }
        }
    }
}