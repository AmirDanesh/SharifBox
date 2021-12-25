using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityApi.Domain.Models.User
{
    public class Role : IdentityRole<Guid>
    {
        public Role(string name) : base(name)
        {

        }
    }
}