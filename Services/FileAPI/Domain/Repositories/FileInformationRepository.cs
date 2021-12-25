using FileAPI.Domain.Context;
using FileAPI.Domain.Models;
using SharifBox.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI.Domain.Repositories
{
    public interface IFileInformationRepository : IGenericRepository<FileDomainContext, FileInformation>
    {

    }

    public class FileInformationRepository : GenericRepository<FileDomainContext, FileInformation>, IFileInformationRepository
    {
        public FileInformationRepository(FileDomainContext dbContext) : base(dbContext)
        {
        }
    }
}
