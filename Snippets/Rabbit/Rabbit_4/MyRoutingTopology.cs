using System;
using NServiceBus.Transport;
using NServiceBus.Transport.RabbitMQ;
using RabbitMQ.Client;

class MyRoutingTopology :
    IRoutingTopology
{
    public MyRoutingTopology(bool createDurableExchangesAndQueues)
    {
    }

    public MyRoutingTopology()
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

    public void Initialize(IModel channel, string main)
    {
    }
}