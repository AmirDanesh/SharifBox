using System;

namespace NewsApi.DTOs
{
    public class EventDTO
    {
        public EventDTO(Guid id, string title, string content, string startDate, string endDate, bool showOnLanding)
        {
            Id = id;
            Title = title;
            Content = content;
            StartDate = startDate;
            EndDate = endDate;
            ShowOnLanding = showOnLanding;
        }

        public Guid Id { get; }

        public string Title { get; }

        public string Content { get; }

        public string StartDate { get; }

        public string EndDate { get; }

        public bool ShowOnLanding { get; }
    }
}