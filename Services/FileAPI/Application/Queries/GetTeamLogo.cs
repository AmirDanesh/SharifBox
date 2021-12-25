using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileAPI.Application.Queries
{
    public class GetTeamLogo
    {
        public class Query : IRequest<string>
        {
            public Query(Guid teamId)
            {
                TeamId = teamId;
            }

            public Guid TeamId { get; }
        }

        public class Handler : IRequestHandler<Query, string>
        {
            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                var path = Startup.Configuration["DefaultRoutes:teamLogo"];
                var file = Startup.Configuration["Assets:teamLogo"];

                if (Directory.Exists(path))
                {
                    var files = Directory.GetFiles(path, request.TeamId + ".*");
                    if (files.Length > 0 && files[0].Length > 0)
                        file = Directory.GetFiles(path, request.TeamId + ".*")[0];
                }

                return Path.Combine(Directory.GetCurrentDirectory(), file);
            }
        }
    }
}