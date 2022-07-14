﻿namespace Wcf_2.Cancellation
{
    using System;
    using NServiceBus;

    class Usage
    {
        class MyService :
            WcfService<Request, Response>
        {
        }

        void Simple(EndpointConfiguration endpointConfiguration)
        {
            #region WcfCancelRequest

            var wcfSettings = endpointConfiguration.Wcf();
            wcfSettings.CancelAfter(
                provider: serviceType =>
                {
                    return serviceType == typeof(MyService) ?
                        TimeSpan.FromSeconds(5) :
                        TimeSpan.FromSeconds(60);
                });

            #endregion
        }
    }
}