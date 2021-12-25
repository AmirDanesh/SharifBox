using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using MediatR;
using JwtAuthorization;
using NewsApi.Domain.Context;
using Microsoft.EntityFrameworkCore;
using NewsApi.Domain.Repositories;
using System;
using AutoMapper;
using NewsApi.InfraStructures.Mapper;
using System.Reflection;
using System.IO;
using NewsApi;
using SwaggerOption;
using EventBusRabbitMQ;
using RabbitMQ.Client;
using EventBusRabbitMQ.Producer;

namespace News.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddCors(o => o.AddPolicy("allowAny", builder =>
            {
                builder.WithOrigins("http://127.0.0.1:4300", "http://localhost:4300")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddMediatR(typeof(Startup).Assembly);

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddAuth(jwtSettings);

            services.AddDbContext<EventDomainContext>(opt =>
                           opt.UseNpgsql(Configuration.GetConnectionString("BoxEventDB")));
            services.AddScoped<IEventUnitOfWork, EventUnitOfWork>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = false;
                mc.AddProfile(new EventMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type =>
                {
                    if (type.FullName.EndsWith("+Command") || type.FullName.EndsWith("+Query"))
                    {
                        var parentTypeName = type.FullName.Substring(type.FullName.LastIndexOf(".", StringComparison.Ordinal) + 1);
                        return parentTypeName.Replace("+Command", "Command").Replace("+Query", "Query");
                    }

                    return type.Name;
                });
                options.SchemaFilter<IgnoreReadOnlySchemaFilter>();
            });

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                return new RabbitMQConnection(new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"],
                    UserName = Configuration["EventBus:UserName"],
                    Password = Configuration["EventBus:Password"]
                });
            });

            services.AddSingleton<EventBusRabbitMQProducer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("allowAny");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuth();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<EventDomainContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}