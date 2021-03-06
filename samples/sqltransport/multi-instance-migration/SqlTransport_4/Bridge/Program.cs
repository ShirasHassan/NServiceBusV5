using System;

namespace Bridge
{
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Bridge;
    using SqlDialect = NServiceBus.Bridge.SqlDialect;

    class Program
    {
        const string SenderConnectionString = @"Data Source=.\SqlExpress;Database=NsbSamplesSqlMultiInstanceSender4;Integrated Security=True;Max Pool Size=100";
        const string ReceiverConnectionString = @"Data Source=.\SqlExpress;Database=NsbSamplesSqlMultiInstanceReceiver4;Integrated Security=True;Max Pool Size=100";
        const string BridgeConnectionString = @"Data Source=.\SqlExpress;Database=NsbSamplesSqlMultiInstanceBridge4;Integrated Security=True;Max Pool Size=100";

        static async Task Main()
        {
            Console.Title = "Samples.SqlServer.MultiInstanceBridge";

            #region BridgeConfiguration

            var bridgeConfig =
                Bridge.Between<SqlServerTransport>("Bridge-Sender",
                        t => t.ConnectionString(SenderConnectionString))
                    .And<SqlServerTransport>("Bridge-Receiver",
                        t => t.ConnectionString(ReceiverConnectionString));

            bridgeConfig.AutoCreateQueues();
            var storage = new SqlSubscriptionStorage(
                connectionBuilder: () => new SqlConnection(BridgeConnectionString), 
                tablePrefix: "", 
                sqlDialect: new SqlDialect.MsSqlServer(), 
                cacheFor: null);

            //Ensures all required schema objects are created
            await storage.Install().ConfigureAwait(false);

            bridgeConfig.UseSubscriptionPersistence(storage);

            #endregion

            SqlHelper.EnsureDatabaseExists(SenderConnectionString);
            SqlHelper.EnsureDatabaseExists(ReceiverConnectionString);
            SqlHelper.EnsureDatabaseExists(BridgeConnectionString);

            var bridge = bridgeConfig.Create();

            await bridge.Start().ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await bridge.Stop().ConfigureAwait(false);
        }
    }
}