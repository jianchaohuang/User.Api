using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Contact.API.Data;
using Contact.API.IntegrationEvent.EventHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Contact.API
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
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            AppSettings setting = new AppSettings();
            Configuration.Bind("AppSettings", setting);
            services.AddTransient<IUserProfileChangedEventHandler, UserProfileChangedEventHandler>();

            services.AddDbContext<CapContext>(options => 
                options.UseMySql("Server=127.0.0.1;Database=beta_contact;UserId=root;Password=123456;"));

            services.AddCap(x =>
            {
                x.UseEntityFramework<CapContext>();
                //x.UseMySql("server=localhost;port=3306;database=beta_contact;userid=test;password=123456;");
                x.UseRabbitMQ(rb =>
                {
                    //rabbitmq服务器地址
                    rb.HostName = "localhost";

                    rb.UserName = "guest";
                    rb.Password = "guest";

                    //指定Topic exchange名称，不指定的话会用默认的
                    rb.ExchangeName = "cap.text.exchange";
                });
                x.UseDashboard();
                x.UseDiscovery(d =>
                {
                    d.DiscoveryServerHostName = "localhost";
                    d.DiscoveryServerPort = 8500;
                    d.CurrentNodeHostName = "localhost";
                    d.CurrentNodePort = 5801;
                    d.NodeId = 2;
                    d.NodeName = "CAP No.2 Node";
                });
                //设置处理成功的数据在数据库中保存的时间（秒），为保证系统新能，数据会定期清理。
                x.SucceedMessageExpiredAfter = 24 * 3600;

                //设置失败重试次数
                x.FailedRetryCount = 5;
            });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.RequireHttpsMetadata = false;
                    option.Audience = "contact_api";
                    option.Authority = "http://localhost:49655";
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<ContactContext>();
            
            services.AddTransient<IContactRespository, ContactRespository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCap();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
