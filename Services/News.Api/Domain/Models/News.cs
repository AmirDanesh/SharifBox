using System;

namespace NewsApi.Domain.Models
{
    public class News
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public Nullable<DateTime> StartDate { get; set; }

        public Nullable<DateTime> EndDate { get; set; }

        public bool ShowOnLanding { get; set; }
    }
}