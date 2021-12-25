using System;

namespace News.Api.DTOs
{
    public class EventsForLanding
    {
        public EventsForLanding(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Guid Id { get; }
        public string Title { get; }
    }
}