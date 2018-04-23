using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace C2R.TelegramBot.Services.Communications
{
    public class CommunicatorFactory : ICommunicatorFactory
    {
        [NotNull]
        private readonly IServiceProvider _serviceProvider;
        [NotNull]
        private readonly Dictionary<Type, List<Tuple<Guid, Type>>> _storage;

        public CommunicatorFactory([NotNull] IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _storage = new Dictionary<Type, List<Tuple<Guid, Type>>>();
        }

        public void Register<TI, TT>(Guid communicationMode) 
            where TI : class, ICommunicator 
            where TT : class, ICommunicator
        {
            if (!_storage.ContainsKey(typeof(TI)))
            {
                _storage[typeof(TI)] = new List<Tuple<Guid, Type>>(1);
            }
            _storage[typeof(TI)].Add(new Tuple<Guid, Type>(communicationMode, typeof(TT)));
        }

        public T Create<T>(Guid communicationMode) 
            where T : class, ICommunicator
        {
            if (!_storage.ContainsKey(typeof(T))) throw new InvalidOperationException($"Communicator with type {typeof(T)} not registed. Use {nameof(Register)}");

            var communicator = _storage[typeof(T)].FirstOrDefault(x => x.Item1 == communicationMode)?.Item2;
            communicator = communicator ?? _storage[typeof(T)].FirstOrDefault()?.Item2;

            return _serviceProvider.GetService(communicator) as T;
        }
    }
}