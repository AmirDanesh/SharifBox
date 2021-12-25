using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileAPI.Application.Commands
{
    public class SaveTeamLogo
    {
        public class Command : IRequest
        {
            public Command(Guid teamId, byte[] imageBytes, string fileExtention)
            {
                TeamId = teamId;
                ImageBytes = imageBytes;
                FileExtention = fileExtention;
            }

            public Guid TeamId { get; }

            public byte[] ImageBytes { get; }

            public string FileExtention { get; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var savePath = Startup.Configuration["DefaultRoutes:teamLogo"];
                var directory = Path.GetDirectoryName(savePath);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var path = Path.Combine(savePath, string.Concat(request.TeamId, ".", request.FileExtention));

                var preFile = Directory.GetFiles(savePath, request.TeamId + ".*");
                if (preFile.Length > 0)
                {
                    File.Delete(preFile[0]);
                }
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await fs.WriteAsync(request.ImageBytes);
                }

                return Unit.Value;
            }
        }
    }
}