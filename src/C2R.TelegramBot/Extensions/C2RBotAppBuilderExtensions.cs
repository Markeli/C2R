using System;
using System.Linq;
using C2R.Core;
using C2R.Core.Contracts;
using C2R.TelegramBot.Services.Communicators;
using C2R.TelegramBot.Services.Communicators.Default;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace C2R.TelegramBot.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Use after all C2RBot configurations
    /// </remarks>
    public static class C2RBotAppBuilderExtensions
    {
        public static void UseC2RBot([NotNull] this IApplicationBuilder appBuilder)
        {
            if (appBuilder == null) throw new ArgumentNullException(nameof(appBuilder));

            var communicatorsFactory = appBuilder.ApplicationServices.GetService<ICommunicatorFactory>();
            
            var defaultCodeReviewerProviderStrategy = CodeReviewerProviderStrategiesIds.RoundRobin;
            var defaultCommunicationMode =
                communicatorsFactory?.DefaultCommunicationMode ?? CommunictionModeIds.Standart;
            
            var configService = appBuilder.ApplicationServices.GetService<ITeamConfigService>();
            
            configService.SetDefaultCommunicationMode(defaultCommunicationMode);
            configService.SetDefaultProviderStrategy(defaultCodeReviewerProviderStrategy);

            var codeReviewerProvider = appBuilder.ApplicationServices.GetService<ICodeReviewerProvider>();
            var strategies = appBuilder.ApplicationServices.GetServices<ICodeReviewerProviderStrategy>();
            if (strategies == null || !strategies.Any()) throw new InvalidOperationException($"No code reviewer providing strategies registered");

            foreach (var providerStrategy in strategies)
            {
                codeReviewerProvider.RegisterStrategy(providerStrategy);
            }

        }
    }
}