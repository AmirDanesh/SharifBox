using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Shared
{
    public static class UserExtentions
    {
        public static Guid GetId(this ClaimsPrincipal user)
        {
            string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return new Guid(userId);
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
