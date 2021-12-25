using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using FileAPI.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace FileAPI.Application.RabbitMQ
{
    public class UploadUserProfileEventConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UploadUserProfileEventConsumer(IRabbitMQConnection connection, IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _connection = connection;
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ConsumeUploadProfilePictureEvent()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.UploadProfilePictureQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.UploadProfilePictureQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.UploadProfilePictureQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var data = JsonConvert.DeserializeObject<UploadUserProfilePictureEvent>(message);

                var command = _mapper.Map<SaveUserProfilePicture.Command>(data);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    await mediator.Send(command);
                }
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}