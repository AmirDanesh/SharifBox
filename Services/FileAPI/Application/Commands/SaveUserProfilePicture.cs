using FileAPI.Application.Helper;
using FileAPI.Domain.Context;
using FileAPI.Domain.Models;
using FileAPI.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileAPI.Application.Commands
{
    public class SaveUserProfilePicture
    {
        public class Command : IRequest
        {
            public Command(Guid identityUserId, string fileExtention, byte[] imageBytes)
            {
                IdentityUserId = identityUserId;
                FileExtention = fileExtention;
                ImageBytes = imageBytes;
            }

            public Guid IdentityUserId { get; }

            public string FileExtention { get; }

            public byte[] ImageBytes { get; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IFileUnitOfWork _unitOfWork;
            private readonly IFileInformationRepository _fileInformationRepository;
            private readonly IProfilePictureRepository _profilePictureRepository;
            public Handler(IFileUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _fileInformationRepository = unitOfWork.FileInformationRepository;
                _profilePictureRepository = unitOfWork.ProfilePictureRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var fileInformation = await _fileInformationRepository.AddAsync(new FileInformation()
                {
                    Size = request.ImageBytes.Length,
                    UploaderUserId = request.IdentityUserId,
                    Extention = request.FileExtention,
                    FileCategory = Enums.FileCategory.profilePicture
                });

                await _profilePictureRepository.AddAsync(new ProfilePictures()
                {
                    FileInformationId = fileInformation.Id,
                    UserId = request.IdentityUserId
                });

                var savePath = FileHelper.ProfilePictureAddress(fileInformation.Id, request.FileExtention);
                var directory = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var preFileInfo = 
                    await _profilePictureRepository.GetAllIncluding(x => x.FileInformation).Where(x => x.UserId == request.IdentityUserId)
                    .FirstOrDefaultAsync();

                if(preFileInfo != null)
                {
                    File.Delete(directory + $"\\{preFileInfo.FileInformationId}.{preFileInfo.FileInformation.Extention}");
                    _fileInformationRepository.Delete(preFileInfo.FileInformation);
                }
                
                using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    await fs.WriteAsync(request.ImageBytes);
                }

                await _unitOfWork.CommitAsync();
                return Unit.Value;
            }
        }
    }
}