using System;
using C2R.TelegramBot.Services.Commands;
using C2R.TelegramBot.Services.Communications;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace C2R.TelegramBot.Extensions
{
    public static class C2RBotDefaultCommunicatorsAppBuilderExtensions
    {
        public static void UseC2RBotDefaultCommunicators([NotNull] this IApplicationBuilder appBuilder)
        {
            if (appBuilder == null) throw new ArgumentNullException(nameof(appBuilder));

            var communicationMode = DefaultCommunictionMode.Id;
            
            var communicatorsFactory = appBuilder.ApplicationServices.GetService<ICommunicatorFactory>();
            communicatorsFactory.SetDefaultCommunicationMode(communicationMode);
            
            communicatorsFactory.Register<IAnotherReviewerCommandCommunicator, DefaultAnotherReviewerCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IRandomCommandCommunciator, DefaultRandomCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IRegisterCommandCommunicator, DefaultRegisterCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IReviewerCommandCommunicator, DefaultReviewerCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IStartCommandCommunicator, DefaultStartCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IStartReminderCommandCommunicator, DefaultStartReminderCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IStopReminderCommandCommunicator, DefaultStopReminderCommandCommunicator>(communicationMode);
            communicatorsFactory.Register<IUnregisterCommandCommunicator, DefaultUnregisterCommandCommunicator>(communicationMode);
        }
    }
}