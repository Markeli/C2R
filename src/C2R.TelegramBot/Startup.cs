using System;
using System.Collections.Generic;
using System.Linq;
using C2R.TelegramBot.Extensions;
using C2R.TelegramBot.Services;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Commands;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Dashboard.Dark;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace C2R.TelegramBot
{
    public class Startup
    {
        
        private readonly IConfigurationRoot _configuration;
        
        public Startup(IHostingEnvironment env)
        {
            _configuration = BuildConfiguration(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .Build();
        }
        
        public static IConfigurationBuilder BuildConfiguration(string contentRoot) =>
            new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("botSettings.json", optional:true);
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(b => b.SetMinimumLevel(LogLevel.Trace));
            services.AddEntityFrameworkInMemoryDatabase();
            services.AddMvc();
            services.AddC2RBot(_configuration);
            services.AddC2RBotStandartCommunicators();

            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
                config.UseDarkDashboard();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            
            app.UseMvc();
            app.UseC2RBotStandartCommunicators(true);
            app.UseC2RBot();
        }
    }
}