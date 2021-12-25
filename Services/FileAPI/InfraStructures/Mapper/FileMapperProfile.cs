using AutoMapper;
using EventBusRabbitMQ.Events;
using FileAPI.Application.Commands;

namespace FileAPI.InfraStructures.Mapper
{
    public class FileMapperProfile : Profile
    {
        public FileMapperProfile()
        {
            CreateMap<UploadUserProfilePictureEvent, SaveUserProfilePicture.Command>();
            CreateMap<UploadTeamLogoEvent, SaveTeamLogo.Command>();
            CreateMap<UploadEventBannerEvent, SaveEventBanner.Command>();
        }
    }
}