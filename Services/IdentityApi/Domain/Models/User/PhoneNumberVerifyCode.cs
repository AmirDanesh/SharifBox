using System;

namespace IdentityApi.Domain.Models.User
{
    public class PhoneNumberVerifyCode
    {
        public PhoneNumberVerifyCode(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            VerifyCode = 111111; //new Random().Next(100000, 1000000);
            ExpireDate = DateTime.UtcNow.AddMinutes(5);
        }

        public Guid Id { get; set; }

        public int VerifyCode { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}