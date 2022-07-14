﻿namespace Core3.Handlers
{
    using Common;
    using log4net;
    using NServiceBus;

    #region GenericMessageHandler
    public class GenericHandler :
        IHandleMessages<object>
    {
        static ILog log = LogManager.GetLogger(typeof(GenericHandler));

        public void Handle(object message)
        {
            log.Info($"Received a message of type {message.GetType().Name}.");
            SomeLibrary.SomeMethod(message);
        }
    }

    #endregion
}