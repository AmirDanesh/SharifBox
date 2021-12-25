using System;
using System.Threading.Tasks;
using Refit;

namespace IdentityApi.Application.Refit
{
    public interface IUserAPI
    {
        [Get("/api/users/byId/{id}")]
        Task<UserDTO> GetUser(Guid id);
    }

    public class UserDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}