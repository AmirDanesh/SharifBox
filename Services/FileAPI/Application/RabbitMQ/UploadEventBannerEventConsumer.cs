using System.Text;
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

namespace FileAPI.Application.RabbitMQ
{
    public class UploadEventBannerEventConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UploadEventBannerEventConsumer(IRabbitMQConnection connection, IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _connection = connection;
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ConsumeUploadEventBannerEvent()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.UploadEventBannerQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.UploadEventBannerQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.UploadEventBannerQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var data = JsonConvert.DeserializeObject<UploadEventBannerEvent>(message);

                var command = _mapper.Map<SaveEventBanner.Command>(data);

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