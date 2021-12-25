using System;

namespace IdentityApi.DTOs
{
    public class PhonverifyDto
    {
        public Guid UserId { get; set; }

        public int VerifyCode { get; set; }
    }
}