using AutoMapper;
using ErrorDescriber.Identity;
using IdentityApi.Domain.Context;
using IdentityApi.Domain.Models.User;
using IdentityApi.Domain.Repositories;
using IdentityApi.InfraStructures.Mapper;
using JwtAuthorization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using RabbitMQ.Client;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using SwaggerOption;

namespace IdentityApi
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
                builder.WithOrigins("http://127.0.0.1:4300", "http://localhost:4300", "http://demo.borhansoft.ir:4300")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));
            services.AddIdentity<Domain.Models.User.IdentityUser, Role>(opt =>
            {
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = false;

                //opt.SignIn.RequireConfirmedEmail = true;
                //opt.SignIn.RequireConfirmedPhoneNumber = true;
            })
                .AddEntityFrameworkStores<IdentityDomainContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<Domain.Models.User.IdentityUser, Role, IdentityDomainContext, Guid>>()
                .AddRoleStore<RoleStore<Role, IdentityDomainContext, Guid>>()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            services.AddMediatR(typeof(Startup).Assembly);

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddAuth(jwtSettings);

            services.AddDbContext<IdentityDomainContext>(opt =>
                           opt.UseNpgsql(Configuration.GetConnectionString("BoxIdentityDB")));

            services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

            var userMappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = false;
                mc.AddProfile(new IdentityMapperProfile());
            });
            IMapper userMapper = userMappingConfig.CreateMapper();
            services.AddSingleton(userMapper);

            services.AddDistributedMemoryCache();

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
            }
                app.UseCors("allowAny");

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
                using (var context = serviceScope.ServiceProvider.GetService<IdentityDomainContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}