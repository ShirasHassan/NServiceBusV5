using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Transport.SQLServer;
using ServiceControl.TransportAdapter;

class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.ServiceControl.SqlServerTransportAdapter.Adapter";
        #region AdapterTransport

        var transportAdapterConfig = new TransportAdapterConfig<SqlServerTransport, SqlServerTransport>("ServiceControl.SqlServer.Adapter");

        #endregion

#pragma warning disable 618

        #region EndpointSideConfig

        transportAdapterConfig.CustomizeEndpointTransport(
            customization: transport =>
        {
            transport.EnableLegacyMultiInstanceMode(Connections.GetConnection);

            SqlHelper.EnsureDatabaseExists(Connections.Adapter);
        });

        #endregion

#pragma warning restore 618

        #region SCSideConfig

        transportAdapterConfig.CustomizeServiceControlTransport(
            customization: transport =>
            {
                var connection = @"Data Source=.\SqlExpress;Initial Catalog=ServiceControl;Integrated Security=True;Max Pool Size=100;Min Pool Size=10";
                transport.ConnectionString(connection);
            });

        #endregion

        #region ControlQueueOverride

        transportAdapterConfig.ServiceControlSideControlQueue = "Particular.ServiceControl.SQL";

        #endregion

        var adapter = TransportAdapter.Create(transportAdapterConfig);

        await adapter.Start()
            .ConfigureAwait(false);

        Console.WriteLine("Press <enter> to shutdown adapter.");
        Console.ReadLine();

        await adapter.Stop()
            .ConfigureAwait(false);
    }
}