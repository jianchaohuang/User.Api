using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using User.Api.data;
using User.Api.Filters;
using User.Api.IntegrationEvent.EventHandling;
using zipkin4net;
using zipkin4net.Middleware;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

namespace User.Api
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
            services.AddDbContext<UserContext>(options=>
            {
                options.UseMySQL(Configuration.GetConnectionString("MysqlUser"));
            });
            services.AddTransient<IUserProfileChangedEventHandler, UserProfileChangedEventHandler>();
            services.AddCap(x =>
            {
                x.UseEntityFramework<UserContext>();
                //x.UseMySql("server=localhost;port=3306;database=beta_user;userid=test;password=123;");
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
                    d.CurrentNodePort = 5800;
                    d.NodeId = 1;
                    d.NodeName = "CAP No.1 Node";
                });
                //设置处理成功的数据在数据库中保存的时间（秒），为保证系统新能，数据会定期清理。
                x.SucceedMessageExpiredAfter = 24 * 3600;

                //设置失败重试次数
                x.FailedRetryCount = 5;
            });
            services.AddMvc(options=>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCap();
            RegisterZipkinTrace(app, loggerFactory, lifetime);
            app.UseMvc();
            //InitUseDataBase(app);
        }
        public void InitUseDataBase(IApplicationBuilder app)
        {
            using (var scope=app.ApplicationServices.CreateScope())
            {
                var userContext = scope.ServiceProvider.GetRequiredService<UserContext>();
                userContext.Database.Migrate();
                if(!userContext.Users.Any())
                {
                    userContext.Users.Add(new model.AppUser { Name = "chaor" });
                    userContext.SaveChanges();
                }
            }
        }
        #region 注册zipkin跟踪
        /// <summary>
        /// 注册zipkinTrace
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="lifetime"></param>
        public void RegisterZipkinTrace(IApplicationBuilder app, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            lifetime.ApplicationStarted.Register(() =>
            {
                TraceManager.SamplingRate = 1.0f;//记录数据密度，1.0代表全部记录
                var logger = new TracingLogger(loggerFactory, "zipkin4net");//内存数据
                var httpSender = new HttpZipkinSender("http://192.168.1.100:9411", "application/json");

                var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer(), new Statistics());//注册zipkin
                var consoleTracer = new zipkin4net.Tracers.ConsoleTracer();//控制台输出


                TraceManager.RegisterTracer(tracer);//注册
                TraceManager.RegisterTracer(consoleTracer);//控制台输入日志
                TraceManager.Start(logger);//放到内存中的数据

            });

            lifetime.ApplicationStopped.Register(() => TraceManager.Stop());

            app.UseTracing("user_api");//这边的名字可自定义
        }
        #endregion
    }
}
