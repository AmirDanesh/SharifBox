using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileAPI.Application.Commands
{
    public class SaveEventBanner
    {
        public class Command : IRequest
        {
            public Command(Guid eventId, byte[] imageBytes, string fileExtention)
            {
                EventId = eventId;
                ImageBytes = imageBytes;
                FileExtention = fileExtention;
            }

            public Guid EventId { get; }

            public byte[] ImageBytes { get; }

            public string FileExtention { get; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var savePath = Startup.Configuration["DefaultRoutes:eventBanner"];
                var directory = Path.GetDirectoryName(savePath);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var path = Path.Combine(savePath, string.Concat(request.EventId, ".", request.FileExtention));

                var preFile = Directory.GetFiles(savePath, request.EventId + ".*");
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