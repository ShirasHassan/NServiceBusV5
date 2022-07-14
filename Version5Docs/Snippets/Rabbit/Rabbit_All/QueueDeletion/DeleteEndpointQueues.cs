﻿namespace Rabbit_All.QueueDeletion
{
    using System;

    public class DeleteEndpointQueues
    {

        DeleteEndpointQueues()
        {
            #region rabbit-delete-queues-endpoint-usage

            DeleteQueuesForEndpoint(
                uri: "amqp://guest:guest@localhost:5672",
                endpointName: "myendpoint");

            #endregion
        }

        #region rabbit-delete-queues-for-endpoint

        public static void DeleteQueuesForEndpoint(string uri, string endpointName)
        {
            // main queue
            QueueDeletionUtils.DeleteQueue(uri, endpointName);

            // callback queue
            QueueDeletionUtils.DeleteQueue(uri, $"{endpointName}.{Environment.MachineName}");

            // timeout queue - not used if using native timeouts
            QueueDeletionUtils.DeleteQueue(uri, $"{endpointName}.Timeouts");

            // timeout dispatcher queue - not used if using native timeouts
            QueueDeletionUtils.DeleteQueue(uri, $"{endpointName}.TimeoutsDispatcher");

            // retries queue
            // TODO: Only required in Versions 3 and below
            QueueDeletionUtils.DeleteQueue(uri, $"{endpointName}.Retries");
        }

        #endregion
    }
}
