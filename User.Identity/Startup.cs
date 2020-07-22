using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using User.Identity.Services;
using User.Identity.Authentication;
using IdentityServer4.Services;
using User.Identity.Infrastructure;
using BuildingBlocks.Resilience.Http;
using zipkin4net;
using zipkin4net.Middleware;
using zipkin4net.Transport.Http;
using zipkin4net.Tracers.Zipkin;

namespace User.Identity
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
            services.AddIdentityServer()
                .AddExtensionGrantValidator<Authentication.SmsAuthCodeValidator>()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources());

            services.AddTransient<IProfileService, ProfileService>();
            services.AddSingleton<HttpClient>();
            services.AddScoped<IAuthCodeService, TestAuthCodeService>()
                    .AddScoped<IUserService, UserService>();
            services.AddTransient<IResilientHttpClientFactory, ResilientHttpClientFactory>();
            services.AddSingleton<IHttpClient, ResilientHttpClient>(sp => sp.GetService<IResilientHttpClientFactory>().CreateResilientHttpClient());
            services.AddMvc();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
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
                var httpSender = new HttpZipkinSender("http://127.0.0.1:9411", "application/json");

                var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer(), new Statistics());//注册zipkin
                var consoleTracer = new zipkin4net.Tracers.ConsoleTracer();//控制台输出


                TraceManager.RegisterTracer(tracer);//注册
                TraceManager.RegisterTracer(consoleTracer);//控制台输入日志
                TraceManager.Start(logger);//放到内存中的数据

            });

            lifetime.ApplicationStopped.Register(() => TraceManager.Stop());

            app.UseTracing("identity_api");//这边的名字可自定义
        }
        #endregion
    }
}
