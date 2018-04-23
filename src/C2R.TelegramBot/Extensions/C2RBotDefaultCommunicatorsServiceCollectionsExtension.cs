using System;
using C2R.TelegramBot.Services.Commands;
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