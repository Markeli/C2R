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
    public static class C2RBotStandartCommunicatorsServiceCollectionsExtension
    {
        public static void AddC2RBotStandartCommunicators([NotNull] this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            
            services.AddTransient<IAnotherReviewerCommandCommunicator, StandartAnotherReviewerCommandCommunicator>();
            services.AddTransient<StandartAnotherReviewerCommandCommunicator, StandartAnotherReviewerCommandCommunicator>();
            services.AddTransient<IRandomCommandCommunciator, StandartRandomCommandCommunicator>();
            services.AddTransient<StandartRandomCommandCommunicator, StandartRandomCommandCommunicator>();
            services.AddTransient<IRegisterCommandCommunicator, StandartRegisterCommandCommunicator>();
            services.AddTransient<StandartRegisterCommandCommunicator, StandartRegisterCommandCommunicator>();
            services.AddTransient<IReviewerCommandCommunicator, StandartReviewerCommandCommunicator>();
            services.AddTransient<StandartReviewerCommandCommunicator, StandartReviewerCommandCommunicator>();
            services.AddTransient<IStartCommandCommunicator, StandartStartCommandCommunicator>();
            services.AddTransient<StandartStartCommandCommunicator, StandartStartCommandCommunicator>();
            services.AddTransient<IStartReminderCommandCommunicator, StandartStartReminderCommandCommunicator>();
            services.AddTransient<StandartStartReminderCommandCommunicator, StandartStartReminderCommandCommunicator>();
            services.AddTransient<IStopReminderCommandCommunicator, StandartStopReminderCommandCommunicator>();
            services.AddTransient<StandartStopReminderCommandCommunicator, StandartStopReminderCommandCommunicator>();
            services.AddTransient<IUnregisterCommandCommunicator, StandartUnregisterCommandCommunicator>();
            services.AddTransient<StandartUnregisterCommandCommunicator, StandartUnregisterCommandCommunicator>();
        }
    }
}