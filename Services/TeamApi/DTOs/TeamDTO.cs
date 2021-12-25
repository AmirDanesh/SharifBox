using System;
using System.Collections.Generic;

namespace TeamApi.DTOs
{
    public class TeamDTO
    {
        public TeamDTO(Guid id, string name, KeyValuePair<Guid, string> managerUserName, string activityField, string description)
        {
            Id = id;
            Name = name;
            ManagerUser = managerUserName;
            ActivityField = activityField;
            Description = description;
        }

        public Guid Id { get; }

        public string Name { get; }

        public KeyValuePair<Guid, string> ManagerUser { get; set; }

        public List<KeyValuePair<Guid, string>> Users { get; set; }

        public string ActivityField { get; }

        public string Description { get; }
    }
}