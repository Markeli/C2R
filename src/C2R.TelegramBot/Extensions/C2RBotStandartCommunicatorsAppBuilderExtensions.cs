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
using C2R.TelegramBot.Services.Communicators;
using C2R.TelegramBot.Services.Communicators.Default;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace C2R.TelegramBot.Extensions
{
    public static class C2RBotDefaultCommunicatorsAppBuilderExtensions
    {
        public static void UseC2RBotStandartCommunicators([NotNull] this IApplicationBuilder appBuilder, bool isDefault)
        {
            if (appBuilder == null) throw new ArgumentNullException(nameof(appBuilder));

            var communicationMode = CommunictionModeIds.Standart;
            
            var communicatorsFactory = appBuilder.ApplicationServices.GetService<ICommunicatorFactory>();
            if (isDefault)
            {
                communicatorsFactory.DefaultCommunicationMode = communicationMode;
            }
            
            communicatorsFactory.Register<IAnotherReviewerCommandCommunicator, StandartAnotherReviewerCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IRandomCommandCommunciator, StandartRandomCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IRegisterCommandCommunicator, StandartRegisterCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IReviewerCommandCommunicator, StandartReviewerCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IStartCommandCommunicator, StandartStartCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IStartReminderCommandCommunicator, StandartStartReminderCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IStopReminderCommandCommunicator, StandartStopReminderCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IUnregisterCommandCommunicator, StandartUnregisterCommandCommunicator>(communicationMode);
        }
    }
}