namespace Core3.DelayedDelivery
{
    using System;
    using NServiceBus;

    class DeferForTimeSpan
    {
        DeferForTimeSpan(
            Configure configuration,
            IBus bus)
        {
            #region configure-persistence-timeout

            configuration.UseInMemoryTimeoutPersister();

            #endregion

            #region configure-timeout-manager

            configuration.RunTimeoutManager();

            #endregion

            #region delayed-delivery-timespan

            bus.Defer(TimeSpan.FromMinutes(30), new MessageToBeSentLater());

            #endregion
        }

        class MessageToBeSentLater
        {
        }
    }
}
