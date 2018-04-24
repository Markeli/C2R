using System;
using System.Collections.Generic;
using System.Linq;
using C2R.Core;
using C2R.Core.Contracts;
using C2R.Core.Data;
using C2R.Core.Data.Abstract;
using C2R.Core.ReviewerProviderStrategies;
using C2R.Core.Teams;
using C2R.TelegramBot.Services;
using C2R.TelegramBot.Services.Bots;
using C2R.TelegramBot.Services.Commands;
using C2R.TelegramBot.Services.Commands.AnotherReviewer;
using C2R.TelegramBot.Services.Commands.Random;
using C2R.TelegramBot.Services.Commands.Register;
using C2R.TelegramBot.Services.Commands.Reviewer;
using C2R.TelegramBot.Services.Commands.Start;
using C2R.TelegramBot.Services.Commands.StartReminder;
using C2R.TelegramBot.Services.Commands.StopReminders;
using C2R.TelegramBot.Services.Commands.Unregister;
using C2R.TelegramBot.Services.Communicators;
using C2R.TelegramBot.Services.Scheduler;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace C2R.TelegramBot.Extensions
{
    public static class C2RBotServiceCollectionsExtension
    {
        public static void AddC2RBot([NotNull] this IServiceCollection services, [NotNull] IConfigurationRoot configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            services.AddSingleton<IUpdateService, UpdateService>();
            services.AddSingleton<IUpdateProcessor, AnotherReviewerCommandProcessor>();
            services.AddSingleton<IUpdateProcessor, RandomCommandProcessor>();
            services.AddSingleton<IUpdateProcessor, RegisterCommandProcessor>();
            services.AddSingleton<IUpdateProcessor, ReviewerCommandProcessor>();
            services.AddSingleton<IUpdateProcessor, StartCommandProccessor>();
            services.AddSingleton<IUpdateProcessor, StartReminderCommandProcessor>();
            services.AddSingleton<IUpdateProcessor, StopReminderCommandProcessor>();
            services.AddSingleton<IUpdateProcessor, UnregisterCommandProcessor>();
            services.AddTransient<ICollection<IUpdateProcessor>>(c => c.GetServices<IUpdateProcessor>()?.ToList() ?? new List<IUpdateProcessor>(0));
            services.AddSingleton<IBotService, BotService>();
            services.Configure<BotConfiguration>(configuration.GetSection("BotConfiguration"));
            services.AddSingleton<ICommunicatorFactory, CommunicatorFactory>();

            services.AddSingleton<IReminderScheduler, ReminderScheduler>();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<C2RDataContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase(nameof(C2RDataContext));
            var options = dbContextOptionsBuilder.Options;

            services.AddSingleton(options);
            services.AddSingleton<IC2RDataContextFactory, C2RDataContextFactory>();
            
            services.AddTransient<ITeamService, TeamService>();
            services.AddSingleton<ITeamConfigService, TeamConfigService>();
            services.AddTransient<ICodeReviewHistoryService, CodeReviewHistoryService>();

            services.AddSingleton<ICodeReviewerProvider, CodeReviewerProvider>();
            
            services.AddTransient<ICodeReviewerProviderStrategy, RandomCodeReviwerProviderStrategy>();
            services.AddTransient<IRandomCodeReviewerProviderStrategy, RandomCodeReviwerProviderStrategy>();
            services.AddTransient<ICodeReviewerProviderStrategy, RoundRobinCodeReviewerProviderStrategy>();
        }
    }
    
}