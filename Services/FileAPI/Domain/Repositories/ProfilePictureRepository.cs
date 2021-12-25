using FileAPI.Domain.Context;
using FileAPI.Domain.Models;
using SharifBox.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI.Domain.Repositories
{
    public interface IProfilePictureRepository : IGenericRepository<FileDomainContext, ProfilePictures>
    {

    }
    public class ProfilePictureRepository : GenericRepository<FileDomainContext, ProfilePictures>, IProfilePictureRepository
    {
        public ProfilePictureRepository(FileDomainContext dbContext) : base(dbContext)
        {
        }
    }
}
