using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace SharifBox.Api.Models.Refit
{
    public interface IRefitRequests
    {
        [Get("/api/Events/landing")]
        Task<List<EventsDTO>> GetEvents();

        [Get("/api/Teams/")]
        Task<List<TeamsDTO>> GetTeams();
    }

    public class EventsDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
    }

    public class TeamsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}