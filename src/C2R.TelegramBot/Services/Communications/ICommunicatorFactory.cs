using System;
using JetBrains.Annotations;

namespace C2R.TelegramBot.Services.Communications
{
    public interface ICommunicatorFactory
    {
        [NotNull]
        T Create<T>(Guid communicationMode);
    }
}