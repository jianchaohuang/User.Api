using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Recommend.API.data;

namespace Recommend.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCap(x =>
            {
                //x.UseEntityFramework<RecommendContext>();
                x.UseMySql("server=localhost;port=3306;database=beta_recommend;userid=root;password=123456;");
                x.UseRabbitMQ(rb =>
                {
                    //rabbitmq服务器地址
                    rb.HostName = "localhost";

                    rb.UserName = "guest";
                    rb.Password = "guest";

                    //指定Topic exchange名称，不指定的话会用默认的
                    rb.ExchangeName = "cap.text.exchange";
                });
                //x.UseDashboard();
                //x.UseDiscovery(d =>
                //{
                //    d.DiscoveryServerHostName = "localhost";
                //    d.DiscoveryServerPort = 8500;
                //    d.CurrentNodeHostName = "localhost";
                //    d.CurrentNodePort = 5800;
                //    d.NodeId = 1;
                //    d.NodeName = "CAP No.1 Node";
                //});
                ////设置处理成功的数据在数据库中保存的时间（秒），为保证系统新能，数据会定期清理。
                //x.SucceedMessageExpiredAfter = 24 * 3600;

                ////设置失败重试次数
                //x.FailedRetryCount = 5;
            });
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
            app.UseMvc();
        }
    }
}
