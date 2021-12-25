using FileAPI.Domain.Context;
using SharifBox.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI.Domain.Repositories
{
    public interface IFileUnitOfWork : IUnitOfWork
    {
        public IFileInformationRepository FileInformationRepository { get; }
        public IProfilePictureRepository ProfilePictureRepository { get; }
    }

    public class FileUnitOfWork : UnitOfWork<FileDomainContext>, IFileUnitOfWork
    {
        public FileUnitOfWork(FileDomainContext context) : base (context)
        {

        }

        private IFileInformationRepository _fileInformationRepository;
        private IProfilePictureRepository _profilePictureRepository;

        public IFileInformationRepository FileInformationRepository => _fileInformationRepository ??= new FileInformationRepository(Context);
        public IProfilePictureRepository ProfilePictureRepository => _profilePictureRepository ??= new ProfilePictureRepository(Context);
    }
}
