using AutoMapper;
using EventBusRabbitMQ;
using JwtAuthorization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SpaceApi.Application.RabbitMQ;
using SpaceApi.Domain.Context;
using SpaceApi.Domain.Repositories;
using SpaceApi.InfraStructures.Mapper;
using SwaggerOption;
using System;

namespace SpaceApi
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
            // Add Hangfire services.
            //services.AddHangfire(configuration => configuration
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UsePostgreSqlStorage(Configuration.GetConnectionString("BoxSpaceDB")));

            //// Add the processing server as IHostedService
            //services.AddHangfireServer();

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

            services.AddDbContext<SpaceDomainContext>(opt =>
                           opt.UseNpgsql(Configuration.GetConnectionString("BoxSpaceDB")));
            services.AddScoped<ISpaceUnitOfWork, SpaceUnitOfWork>();
            services.AddSwaggerGen(options =>
            {
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
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
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = false;
                mc.AddProfile(new SpaceAutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                return new RabbitMQConnection(new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"],
                    //Port = Convert.ToInt32(Configuration["EventBus:Port"]),
                    UserName = Configuration["EventBus:UserName"],
                    Password = Configuration["EventBus:Password"]
                });
            });

            services.AddSingleton<EventBusRabbitMQConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseCors("allowAny");
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpaceApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting(); 

            app.UseAuth();

            app.UseAuthorization();

            //app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHangfireDashboard();
            });
            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SpaceDomainContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}