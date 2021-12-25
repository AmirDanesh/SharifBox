using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using UserApi.Domain.Models.User;
using UserApi.Domain.Repositories;
using UserApi.DTOs;

namespace UserApi.Application.Commands
{
    public class AddSkill
    {
        public class Command : IRequest<SkillDropDownDTO>
        {
            public Command(string name)
            {
                Name = name;
            }

            [Required]
            public string Name { get; }
        }

        public class Handler : IRequestHandler<Command, SkillDropDownDTO>
        {
            private readonly IMapper _mapper;
            private readonly IUserUnitOfWork _unitOfWork;
            private readonly IMediator _mediator;
            private readonly ISkillRepository _skillRepository;

            public Handler(IMapper mapper, IUserUnitOfWork unitOfWork, IMediator mediator)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _mediator = mediator;
                _skillRepository = unitOfWork.SkillRepository;
            }

            public async Task<SkillDropDownDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var skill = await _skillRepository.AddAsync(new Skill() { Name = request.Name });
                await _unitOfWork.CommitAsync();
                return _mapper.Map<SkillDropDownDTO>(skill);
            }
        }
    }
}