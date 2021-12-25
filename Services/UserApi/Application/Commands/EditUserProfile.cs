using AutoMapper;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Queries;
using UserApi.Domain.Models.User;
using UserApi.Domain.Repositories;
using UserApi.DTOs;

namespace UserApi.Application.Commands
{
    public class EditUserProfile
    {
        public class Command : IRequest<UserProfileDTO>
        {
            public Command(Guid identityUserId, string firstName, string lastName, string address, string nationalCode, List<Guid> skillIds, string imageProfile)
            {
                IdentityUserId = identityUserId;
                FirstName = firstName;
                LastName = lastName;
                Address = address;
                NationalCode = nationalCode;
                SkillIds = skillIds;
                ImageProfile = imageProfile;
            }

            [Required]
            public Guid IdentityUserId { get; }

            [Required]
            [RegularExpression(@"^[a-zA-Zآ-ی ء چ]+$")]
            public string FirstName { get; }

            [Required]
            [RegularExpression(@"^[a-zA-Zآ-ی ء چ]+$")]
            public string LastName { get; }

            [Required]
            public string Address { get; }

            [Required]
            [RegularExpression(@"^[0-9]*$")]
            public string NationalCode { get; }

            //TODO remove setter and test that
            public List<Guid> SkillIds { get; set; }

            public string ImageProfile { get; }
        }

        public class Handler : IRequestHandler<Command, UserProfileDTO>
        {
            private readonly IMapper _mapper;
            private readonly IUserUnitOfWork _unitOfWork;
            private readonly IMediator _mediator;
            private readonly IUserRepository _userRepository;
            private readonly ISkillRepository _skillRepository;
            private readonly IUserSkillRepository _userSkillRepository;
            private readonly EventBusRabbitMQProducer _eventBus;

            public Handler(IMapper mapper, IUserUnitOfWork unitOfWork, IMediator mediator, EventBusRabbitMQProducer eventBus)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _mediator = mediator;
                _userRepository = unitOfWork.UserRepository;
                _skillRepository = unitOfWork.SkillRepository;
                _userSkillRepository = unitOfWork.UserSkillRepository;
                _eventBus = eventBus;
            }

            public async Task<UserProfileDTO> Handle(Command request, CancellationToken cancellationToken)
            {

                var currentUser = await _userRepository.FindAsync(x => x.IdentityUserId == request.IdentityUserId);

                if (currentUser == null)
                    throw new KeyNotFoundException("کاربر یافت نشد");

                var newUser = _mapper.Map(request, currentUser);

                newUser = await _userRepository.UpdateAsync(newUser, newUser.Id);

                #region Update User Skills

                var userSkills = await _userSkillRepository.FindAllAsync(x => x.UserId == newUser.Id);

                var skillsToDelete = userSkills.Where(x => !request.SkillIds.Contains(x.SkillId)).ToList();
                var skillsToAdd = request.SkillIds.Where(x => !userSkills.Any(s => s.SkillId == x))
                    .Select(s => new UserSkill() { UserId = newUser.Id, SkillId = _skillRepository.FindAsync(z => z.Id == s).GetAwaiter().GetResult().Id }).ToList();

                skillsToDelete.ForEach(x => _userSkillRepository.Delete(x));
                skillsToAdd.ForEach(async x => await _userSkillRepository.AddAsync(x));

                #endregion Update User Skills

                await _unitOfWork.CommitAsync();

                #region Update Profile Picture

                if (request.ImageProfile != null && request.ImageProfile.StartsWith("data:image"))
                {
                    var fileExtention = request.ImageProfile.Split(",")[0].Split("/")[1].Split(";")[0];
                    byte[] imageBytes = Convert.FromBase64String(request.ImageProfile.Split(",")[1]);

                    _eventBus.PublishUploadProfilePicture(EventBusConstants.UploadProfilePictureQueue,
                        new UploadUserProfilePictureEvent()
                        {
                            IdentityUserId = request.IdentityUserId,
                            FileExtention = fileExtention,
                            ImageBytes = imageBytes
                        });
                }

                #endregion Update Profile Picture

                return _mapper.Map<UserProfileDTO>(newUser);
            }
        }
    }
}