﻿namespace Core3.UpgradeGuides._3to4.SimpleSaga
{
    using System;
    using NServiceBus.Saga;

    #region simple-saga-data
    public class OrderSagaData :
        IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
        [Unique]
        public string OrderId { get; set; }
    }
    #endregion
}
