using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.HttpJob;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HangfireService
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
            services.AddControllers();
            services.AddHangfire(ConfigurationHangfire);
        }
        private void ConfigurationHangfire(IGlobalConfiguration globalConfiguration)
        {
            globalConfiguration.UseStorage(
                    new MySqlStorage(
                        "Server=localhost;Port=3306;Database=HangfireDb;Uid=root;Pwd=123456;charset=utf8;SslMode=none;Allow User Variables=True",
                        new MySqlStorageOptions
                        {
                            TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                            QueuePollInterval = TimeSpan.FromSeconds(15),
                            JobExpirationCheckInterval = TimeSpan.FromHours(1),
                            CountersAggregateInterval = TimeSpan.FromMinutes(5),
                            PrepareSchemaIfNecessary = false,
                            DashboardJobListLimit = 50000,
                            TransactionTimeout = TimeSpan.FromMinutes(1),
                        })).UseConsole().UseHangfireHttpJob();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                {
                    RequireSsl = false,
                    SslRedirect = false,
                    LoginCaseSensitive = true,
                    Users = new []
                    {
                        new BasicAuthAuthorizationUser
                        {
                            Login = "admin",
                            PasswordClear =  "test"
                        }
                    }
                }) }
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
