using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C2R.TelegramBot.Services.Commands;
using C2RTelegramBot.Services;
using C2RTelegramBot.Services.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace C2RTelegramBot
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
                .AddJsonFile("botSettings.json");
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IUpdateService, UpdateService>();
            services.AddSingleton<IUpdateProcessor, StartCommandProccessor>();
            services.AddTransient<ICollection<IUpdateProcessor>>(c => c.GetServices<IUpdateProcessor>()?.ToList() ?? new List<IUpdateProcessor>(0));
            services.AddSingleton<IBotService, PushingBotService>();

            services.Configure<BotConfiguration>(_configuration.GetSection("BotConfiguration"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}