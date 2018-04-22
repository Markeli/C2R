using System;
using JetBrains.Annotations;

namespace C2R.TelegramBot.Services.Communications
{
    public interface ICommunicatorFactory
    {
        void Register<TI, TT>(Guid communicationMode) 
            where TI: class, ICommunicator
            where TT: class, ICommunicator;
        
        [NotNull]
        T Create<T>(Guid communicationMode) 
            where T: class, ICommunicator;
    }
}