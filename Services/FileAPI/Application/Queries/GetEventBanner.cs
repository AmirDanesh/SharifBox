using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace FileAPI.Application.Queries
{
    public class GetEventBanner
    {
        public class Query : IRequest<string>
        {
            public Query(Guid eventId)
            {
                EventId = eventId;
            }

            public Guid EventId { get; }
        }

        public class Handler : IRequestHandler<Query, string>
        {
            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                var file = Startup.Configuration["Assets:eventBanner"];

                if (Directory.Exists(Startup.Configuration["DefaultRoutes:eventBanner"]))
                {
                    var files = Directory.GetFiles(Startup.Configuration["DefaultRoutes:eventBanner"], request.EventId + ".*");
                    if (files.Length > 0 && files[0].Length > 0)
                        file = files[0];
                }

                return Path.Combine(Directory.GetCurrentDirectory(), file);
            }
        }
    }
}