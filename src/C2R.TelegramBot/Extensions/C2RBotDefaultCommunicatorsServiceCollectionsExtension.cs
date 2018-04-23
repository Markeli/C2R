using System;
using C2R.TelegramBot.Services.Commands;
using C2R.TelegramBot.Services.Commands.AnotherReviewer;
using C2R.TelegramBot.Services.Commands.Random;
using C2R.TelegramBot.Services.Commands.Register;
using C2R.TelegramBot.Services.Commands.Reviewer;
using C2R.TelegramBot.Services.Commands.Start;
using C2R.TelegramBot.Services.Commands.StartReminder;
using C2R.TelegramBot.Services.Commands.StopReminders;
using C2R.TelegramBot.Services.Commands.Unregister.Communicators;
using C2R.TelegramBot.Services.Communicators.Default;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace C2R.TelegramBot.Extensions
{
    public static class C2RBotDefaultCommunicatorsServiceCollectionsExtension
    {
        public static void AddC2RBotDefaultCommunicators([NotNull] this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            
            services.AddTransient<IAnotherReviewerCommandCommunicator, DefaultAnotherReviewerCommandCommunicator>();
            services.AddTransient<IRandomCommandCommunciator, DefaultRandomCommandCommunicator>();
            services.AddTransient<IRegisterCommandCommunicator, DefaultRegisterCommandCommunicator>();
            services.AddTransient<IReviewerCommandCommunicator, DefaultReviewerCommandCommunicator>();
            services.AddTransient<IStartCommandCommunicator, DefaultStartCommandCommunicator>();
            services.AddTransient<IStartReminderCommandCommunicator, DefaultStartReminderCommandCommunicator>();
            services.AddTransient<IStopReminderCommandCommunicator, DefaultStopReminderCommandCommunicator>();
            services.AddTransient<IUnregisterCommandCommunicator, DefaultUnregisterCommandCommunicator>();
        }
    }
}