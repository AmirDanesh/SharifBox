using AutoMapper;
using EventBusRabbitMQ;
using FileAPI.Application.RabbitMQ;
using FileAPI.Domain.Context;
using FileAPI.Domain.Repositories;
using FileAPI.Extentions;
using FileAPI.InfraStructures.Mapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FileAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddCors(o => o.AddPolicy("allowAny", builder =>
            {
                builder.WithOrigins("http://127.0.0.1:4300", "http://localhost:4300", "http://127.0.0.1:5002", "http://localhost:5002")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddMediatR(typeof(Startup).Assembly);

            var fileMappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = false;
                mc.AddProfile(new FileMapperProfile());
            });
            IMapper fileMapper = fileMappingConfig.CreateMapper();
            services.AddSingleton(fileMapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FileAPI", Version = "v1" });
            });

            services.AddDbContext<FileDomainContext>(opt =>
                           opt.UseNpgsql(Configuration.GetConnectionString("BoxFileDB")));
            services.AddScoped<IFileUnitOfWork, FileUnitOfWork>();

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                return new RabbitMQConnection(new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"],
                    UserName = Configuration["EventBus:UserName"],
                    Password = Configuration["EventBus:Password"]
                });
            });

            services.AddSingleton<UploadUserProfileEventConsumer>();
            services.AddSingleton<UploadTeamLogoEventConsumer>();
            services.AddSingleton<UploadEventBannerEventConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseRabbitListener();

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
                using (var context = serviceScope.ServiceProvider.GetService<FileDomainContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}