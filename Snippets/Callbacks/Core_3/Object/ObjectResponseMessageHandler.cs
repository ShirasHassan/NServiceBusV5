﻿namespace Core3.Object
{
    using NServiceBus;

    #region FakeObjectCallbackHandler

    public class ObjectResponseMessageHandler :
        IHandleMessages<ResponseMessage>
    {
        public void Handle(ResponseMessage message)
        {
        }
    }

    #endregion
}