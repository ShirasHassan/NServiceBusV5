﻿namespace Core4.NonDurable.ExpressMessages
{
    using NServiceBus;

    #region ExpressMessageAttribute

    [Express]
    public class MessageWithExpress :
        IMessage
    {
    }

    #endregion
}