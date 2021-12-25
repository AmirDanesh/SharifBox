using FileAPI.Application.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileAPI.Extentions
{
    public static class ApplicationBuilderExtention
    {
        public static UploadTeamLogoEventConsumer TeamLogoEventListener { get; set; }

        public static UploadUserProfileEventConsumer UserProfileEventListener { get; set; }

        public static UploadEventBannerEventConsumer EventBannerEventListener { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            UserProfileEventListener = app.ApplicationServices.GetService<UploadUserProfileEventConsumer>();
            TeamLogoEventListener = app.ApplicationServices.GetService<UploadTeamLogoEventConsumer>();
            EventBannerEventListener = app.ApplicationServices.GetService<UploadEventBannerEventConsumer>();

            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            UserProfileEventListener.ConsumeUploadProfilePictureEvent();
            TeamLogoEventListener.ConsumeUploadTeamLogoEvent();
            EventBannerEventListener.ConsumeUploadEventBannerEvent();
        }

        private static void OnStopping()
        {
            UserProfileEventListener.Disconnect();
            TeamLogoEventListener.Disconnect();
            EventBannerEventListener.Disconnect();
        }
    }
}