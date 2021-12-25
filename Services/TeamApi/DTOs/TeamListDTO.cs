using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamApi.DTOs
{
    public class TeamListDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ManagerName { get; set; }

        public int NumberOfUsers { get; set; }
    }
}