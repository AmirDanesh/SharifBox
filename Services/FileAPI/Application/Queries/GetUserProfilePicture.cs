using AutoMapper.Configuration;
using FileAPI.Application.Helper;
using FileAPI.Domain.Models;
using FileAPI.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileAPI.Application.Queries
{
    public class GetUserProfilePicture
    {
        public class Query : IRequest<string>
        {
            public Query(Guid identityUserId)
            {
                IdentityUserId = identityUserId;
            }

            public Guid IdentityUserId { get; }
        }

        public class Handler : IRequestHandler<Query, string>
        {
            private readonly IFileUnitOfWork _unitOfWork;
            private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
            private readonly IProfilePictureRepository _profilePictureRepository;
            public Handler(IFileUnitOfWork unitOfWork, Microsoft.Extensions.Configuration.IConfiguration configuration)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
                _profilePictureRepository = unitOfWork.ProfilePictureRepository;
            }
            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                var fileInfo =  (await _profilePictureRepository.GetAllIncluding(x => x.FileInformation)
                    .FirstOrDefaultAsync(x => x.UserId == request.IdentityUserId))?.FileInformation;

                if(fileInfo != null)
                {
                    return FileHelper.ProfilePictureAddress(fileInfo.Id, fileInfo.Extention);
                }
                else
                {
                    return Path.Combine(Directory.GetCurrentDirectory(),Startup.Configuration["Assets:profilePicture"]);
                }
            }
        }
    }
}