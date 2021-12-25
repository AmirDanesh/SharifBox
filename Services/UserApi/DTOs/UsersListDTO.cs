using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.DTOs
{
    public class UsersListDTO
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JoinDate { get; set; }
    }
}
