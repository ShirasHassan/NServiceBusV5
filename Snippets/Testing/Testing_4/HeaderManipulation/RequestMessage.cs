﻿namespace Testing_4.HeaderManipulation
{
    using NServiceBus;

    class RequestMessage :
        IMessage
    {
        public string String { get; set; }
    }
}