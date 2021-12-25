using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Common
{
    public static class EventBusConstants
    {
        public const string IdentityUserCreatedQueue = "IdentityUserCreatedQueue";
        public const string UploadProfilePictureQueue = "UploadProfilePictureQueue";
        public const string UploadTeamLogoQueue = "UploadTeamLogoQueue";
        public const string UploadEventBannerQueue = "UploadEventBannerQueue";
    }
}