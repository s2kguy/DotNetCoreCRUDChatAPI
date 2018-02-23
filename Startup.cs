using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
//using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Sockets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Extensions;
//using Microsoft.Extensions.DependencyModel;,
using chatAPI.Models;
using chatAPI.Hubs;

namespace chatAPI
{
    public class Startup
    {
        public static ConnectionManager _ConnectionManager;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
          
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // REGISTERING THE DB CONTEXT VIA DEPENDENCY INJECTION CONTAINER
            services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("Users"));
            services.AddSignalR();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
         //   _ConnectionManager = serviceProvider.GetService<ConnectionManager>();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("chat");
            }); 

            app.UseMvc();
        }
    }
}
