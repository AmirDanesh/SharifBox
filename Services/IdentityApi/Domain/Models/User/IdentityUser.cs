using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace IdentityApi.Domain.Models.User
{
    public class IdentityUser : IdentityUser<Guid>
    {
        public List<PhoneNumberVerifyCode> VerifyCodes { get; set; }
    }
}