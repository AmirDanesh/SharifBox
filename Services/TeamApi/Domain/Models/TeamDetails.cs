using System;

namespace TeamApi.Domain.Models
{
    public class TeamDetails
    {
        public Guid Id { get; set; }

        public string ActivityField { get; set; }

        public string Description { get; set; }
    }
}