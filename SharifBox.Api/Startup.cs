using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace SharifBox.Api
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
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddCors(o => o.AddPolicy("allowAny", builder =>
            {
                builder.WithOrigins("http://127.0.0.1:4200", "http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddMediatR(typeof(Startup).Assembly);

            //services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            //var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            //services.AddAuth(jwtSettings);

            #region User Domain

            //services.AddDbContext<UserDomainContext>(opt =>
            //               opt.UseMySql(Configuration.GetConnectionString("BoxUserDB"), mySqlOptionsAction: sqlOptions =>
            //               {
            //                   sqlOptions.EnableRetryOnFailure(
            //                       maxRetryCount: 10,
            //                       maxRetryDelay: TimeSpan.FromSeconds(30),
            //                       errorNumbersToAdd: null
            //                       );
            //               }));
            //services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

            #endregion User Domain

            // Auto Mapper
            //var userMappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AllowNullCollections = false;
            //    mc.AddProfile(new UserMapperProfile());
            //});
            //IMapper userMapper = userMappingConfig.CreateMapper();
            //services.AddSingleton(userMapper);

            services.AddDistributedMemoryCache();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseCors("allowAny");

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            //app.UseAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                //using (var context = serviceScope.ServiceProvider.GetService<UserDomainContext>())
                //{
                //    context.Database.Migrate();
                //}
            }
        }
    }
}