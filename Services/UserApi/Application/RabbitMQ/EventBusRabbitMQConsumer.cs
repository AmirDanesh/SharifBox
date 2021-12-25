using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Application.Commands;
using UserApi.Domain.Context;
using UserApi.Domain.Models.User;
using UserApi.Domain.Repositories;

namespace UserApi.Application.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _connection = connection;
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.IdentityUserCreatedQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.IdentityUserCreatedQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.IdentityUserCreatedQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var identityUserCreatedEvent = JsonConvert.DeserializeObject<IdentityUserCreatedEvent>(message);

                var command = _mapper.Map<CreateUser.Command>(identityUserCreatedEvent);

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