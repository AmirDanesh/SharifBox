using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ApiGateway
{
    public class Startup
    {
        private readonly string myAllowedOrigins = "_mySpecifiedOriginsOnly";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot();

            //ICollection<string> hosts = new Collection<string>() {"http://"};
            //Configuration.GetSection("AllowedHosts").Bind(hosts);

            services.AddCors(o => o.AddPolicy("allowAny", builder =>
            {
                builder.WithOrigins("http://127.0.0.1:4300", "http://localhost:4300","http://demo.borhansoft.ir:4300", "https://localhost:4300")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ICollection<string> hosts = new Collection<string>();
            Configuration.GetSection("AllowedHosts").Bind(hosts);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("allowAny");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot();
        }
    }
}