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
using Hangfire;
using Hangfire.MemoryStorage;
using GameDealsNotification.Configurations;
using GameDealsNotification.Services.Interfaces;
using GameDealsNotification.Services;

namespace GameDealsNotification
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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Add Swagger Service
            services.AddSwaggerGen();

            //Add Hangfire Service
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage()
            );
            services.AddHangfireServer();

            //Add Database Connection Configurations
            services.Configure<DbConnectionConfigModel>(Configuration.GetSection("DbConnection"));

            //Add other settings
            services.Configure<Settings>(Configuration.GetSection("Settings"));

            //Add MainService
            services.AddSingleton<IMainService, MainService>();

            //Add DBContext
            services.AddTransient<IDBContext, DBContext>();

            //Add httprequest service
            services.AddTransient<IHttpRequest, HttpRequest>();

            //Add email service
            services.AddTransient<IEmailService, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Configure Swagger Service
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); c.RoutePrefix = string.Empty; });

            //Add recurring hangfire jobs
            app.UseHangfireDashboard();
            recurringJobManager.AddOrUpdate("1",() => serviceProvider.GetService<IMainService>().ScanningDealsAndSendingNotiAsync(), Cron.Minutely());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
