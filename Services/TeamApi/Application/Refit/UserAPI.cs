using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamApi.Application.Refit
{
    public interface IUserAPI
    {
        [Get("/api/users/byId/{id}")]
        Task<UserDTO> GetUserName(Guid id);

        [Get("/api/users")]
        Task<List<UserNameDto>> GetUsersName([Body] List<Guid> ids);
    }

    public class UserDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class UserNameDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}