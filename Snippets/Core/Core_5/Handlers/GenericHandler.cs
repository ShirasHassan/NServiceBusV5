﻿namespace Core5.Handlers
{
    using Common;
    using NServiceBus;
    using NServiceBus.Logging;

    #region GenericMessageHandler

    public class GenericHandler :
        IHandleMessages<object>
    {
        static ILog log = LogManager.GetLogger<GenericHandler>();

        public void Handle(object message)
        {
            log.Info($"Received a message of type {message.GetType().Name}.");
            SomeLibrary.SomeMethod(message);
        }
    }

    #endregion
}
