using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;
using System;

#region saga

public class OrderSaga :
    Saga<OrderSagaData>,
    IAmStartedByMessages<StartOrder>,
    IHandleMessages<CompletePaymentTransaction>,
    IHandleMessages<CompleteOrder>
{
    static ILog log = LogManager.GetLogger<OrderSaga>();

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
    {
        mapper.ConfigureMapping<StartOrder>(_ => _.OrderId)
            .ToSaga(_ => _.OrderId);
        mapper.ConfigureMapping<CompleteOrder>(_ => _.OrderId)
            .ToSaga(_ => _.OrderId);
    }

    public void Handle(StartOrder message)
    {
        Data.OrderId = message.OrderId;
        Data.PaymentTransactionId = Guid.NewGuid().ToString();

        log.Info($"Saga with OrderId {Data.OrderId} received StartOrder with OrderId {message.OrderId}");
        var issuePaymentRequest = new IssuePaymentRequest
        {
            PaymentTransactionId = Data.PaymentTransactionId
        };
        Bus.SendLocal(issuePaymentRequest);
    }

    public void Handle(CompletePaymentTransaction message)
    {
        log.Info($"Transaction with Id {Data.PaymentTransactionId} completed for order id {Data.OrderId}");
        var completeOrder = new CompleteOrder
        {
            OrderId = Data.OrderId
        };
        Bus.SendLocal(completeOrder);
    }

    public void Handle(CompleteOrder message)
    {
        log.Info($"Saga with OrderId {Data.OrderId} received CompleteOrder with OrderId {message.OrderId}");
        MarkAsComplete();
    }
}

#endregion