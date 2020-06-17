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
using MediatR;
using Project.Infrastruture;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Project.API.Applications.Services;
using Project.API.Applications.Queries;
using Project.Domain.AggregatesModel;
using Project.Infrastruture.Repositories;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Project.API
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
            var migrationAssembly = typeof(ProjectContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<ProjectContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("MysqlProject")
                    ,sql=>sql.MigrationsAssembly(migrationAssembly));
            });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option=>
                {
                    option.RequireHttpsMetadata = false;
                    option.Audience = "project_api";
                    option.Authority = "http://localhost:49655";
                });
            services.AddScoped<IRecommendService,TestRecommendService>()
                .AddScoped<IProjectQueries,ProjectQueries>()
                .AddScoped<IProjectRepository, ProjectReposity>(sp=> {
                    var context = sp.GetService<ProjectContext>();
                    return new ProjectReposity(context);
                });
            services.AddMediatR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
