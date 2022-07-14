﻿namespace SagaFinder
{
    using System.Threading;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Extensibility;
    using NServiceBus.Persistence;
    using NServiceBus.Sagas;

    #region SagaFinder-sqlServer

    class SqlServerSagaFinder :
        ISagaFinder<MySagaData, MyMessage>
    {
        public Task<MySagaData> FindBy(MyMessage message, ISynchronizedStorageSession session, IReadOnlyContextBag context, CancellationToken cancellationToken = default)
        {
            return session.GetSagaData<MySagaData>(
                context: context,
                whereClause: "JSON_VALUE(Data,'$.PropertyPathInJson') = @propertyValue",
                appendParameters: (builder, append) =>
                {
                    var parameter = builder();
                    parameter.ParameterName = "propertyValue";
                    parameter.Value = message.PropertyValue;
                    append(parameter);
                });
        }
    }

    #endregion
}