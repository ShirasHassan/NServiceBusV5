using System;
using NServiceBus;
using NServiceBus.Transport.SQLServer;

class DelayedDelivery
{
    void Configure(EndpointConfiguration endpointConfiguration)
    {
        #region EnableNativeDelayedDelivery 3.0

        var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
        var delayedDeliverySettings = transport.UseNativeDelayedDelivery();

        #endregion

        #region DelayedDeliveryTableSuffix

        delayedDeliverySettings.TableSuffix("Delayed");

        #endregion

        #region DelayedDeliveryProcessingInterval

        delayedDeliverySettings.ProcessingInterval(TimeSpan.FromSeconds(1));

        #endregion

        #region DelayedDeliveryBatchSize

        delayedDeliverySettings.BatchSize(100);

        #endregion

        #region DelayedDeliveryDisableTM

        delayedDeliverySettings.DisableTimeoutManagerCompatibility();

        #endregion
    }
}