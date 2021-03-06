using System;
using System.Collections.Generic;
using NServiceBus.Transport;
using NServiceBus.Transport.RabbitMQ;
using RabbitMQ.Client;

class MyRoutingTopology :
    IRoutingTopology
{
    public MyRoutingTopology(bool createDurableExchangesAndQueues)
    {
    }

    public void SetupSubscription(IModel channel, Type type, string subscriberName)
    {
    }

    public void TeardownSubscription(IModel channel, Type type, string subscriberName)
    {
    }

    public void Publish(IModel channel, Type type, OutgoingMessage message, IBasicProperties properties)
    {
    }

    public void Send(IModel channel, string address, OutgoingMessage message, IBasicProperties properties)
    {
    }

    public void RawSendInCaseOfFailure(IModel channel, string address, byte[] body, IBasicProperties properties)
    {
    }

    public void Initialize(IModel channel, IEnumerable<string> receivingAddresses, IEnumerable<string> sendingAddresses)
    {
    }

    public void BindToDelayInfrastructure(IModel channel, string address, string deliveryExchange, string routingKey)
    {
    }
}