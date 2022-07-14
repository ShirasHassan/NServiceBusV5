﻿using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

class Program
{
    static ILog log = LogManager.GetLogger<Program>();
    static async Task Main()
    {
        var configuration = new EndpointConfiguration("DataAnnotationsValidationSample");
        configuration.UsePersistence<LearningPersistence>();
        configuration.UseTransport<LearningTransport>();

        #region Enable
        configuration.UseDataAnnotationsValidation(outgoing: false);
        #endregion

        var endpoint = await Endpoint.Start(configuration).ConfigureAwait(false);

        await endpoint.SendLocal(new MyMessage {Content = "This property is expected to have a value."}).ConfigureAwait(false);
        await endpoint.SendLocal(new MyMessage()).ConfigureAwait(false);

        log.Info("Press any key to stop program");
        Console.Read();
        await endpoint.Stop().ConfigureAwait(false);
    }
}