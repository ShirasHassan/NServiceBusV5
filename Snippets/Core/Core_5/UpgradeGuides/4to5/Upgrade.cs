namespace Core5.UpgradeGuides._4to5
{
    using System;
    using System.Security.Principal;
    using System.Threading;
    using NServiceBus;
    using NServiceBus.Logging;
    using NServiceBus.MessageMutator;
    using NServiceBus.Persistence;
    using NServiceBus.Transports;
    using Raven.Client.Document;

    public class Upgrade
    {
        #region 4to5RemovePrincipalHack

        public class PrincipalMutator :
            IMutateIncomingTransportMessages
        {
            public void MutateIncoming(TransportMessage message)
            {
                var headers = message.Headers;
                var windowsIdentityName = headers[Headers.WindowsIdentityName];
                var identity = new GenericIdentity(windowsIdentityName);
                var principal = new GenericPrincipal(identity, new string[0]);
                Thread.CurrentPrincipal = principal;
            }
        }

        #endregion

        void MessageConventions(BusConfiguration busConfiguration)
        {
            #region 4to5MessageConventions

            var conventions = busConfiguration.Conventions();
            conventions.DefiningCommandsAs(
                type =>
                {
                    return type.Namespace != null &&
                           type.Namespace == "MyNamespace" &&
                           type.Namespace.EndsWith("Commands");
                });
            conventions.DefiningEventsAs(
                type =>
                {
                    return type.Namespace != null &&
                           type.Namespace == "MyNamespace" &&
                           type.Namespace.EndsWith("Events");
                });
            conventions.DefiningMessagesAs(
                type =>
                {
                    return type.Namespace != null &&
                           type.Namespace == "Messages";
                });
            conventions.DefiningDataBusPropertiesAs(
                property =>
                {
                    return property.Name.EndsWith("DataBus");
                });
            conventions.DefiningExpressMessagesAs(
                type =>
                {
                    return type.Name.EndsWith("Express");
                });
            conventions.DefiningTimeToBeReceivedAs(
                type =>
                {
                    if (type.Name.EndsWith("Expires"))
                    {
                        return TimeSpan.FromSeconds(30);
                    }
                    return TimeSpan.MaxValue;
                });

            #endregion
        }

        void CustomConfigOverrides(BusConfiguration busConfiguration)
        {
            #region 4to5CustomConfigOverrides

            busConfiguration.AssembliesToScan(AllAssemblies.Except("NotThis.dll"));
            var conventions = busConfiguration.Conventions();
            conventions.DefiningEventsAs(
                type =>
                {
                    return type.Name.EndsWith("Event");
                });
            busConfiguration.EndpointName("MyEndpointName");

            #endregion
        }

        void UseTransport(BusConfiguration busConfiguration)
        {
            #region 4to5UseTransport

            // Choose one of the following

            busConfiguration.UseTransport<MsmqTransport>();

            busConfiguration.UseTransport<RabbitMQTransport>();

            busConfiguration.UseTransport<SqlServerTransport>();

            busConfiguration.UseTransport<AzureStorageQueueTransport>();

            busConfiguration.UseTransport<AzureServiceBusTransport>();

            #endregion
        }

        class AzureStorageQueueTransport :
            TransportDefinition
        {
        }

        class AzureServiceBusTransport :
            TransportDefinition
        {
        }

        class RabbitMQTransport :
            TransportDefinition
        {
        }

        class SqlServerTransport :
            TransportDefinition
        {
        }

        void InterfaceMessageCreation(IBus Bus, IMessageCreator messageCreator)
        {
            #region 4to5InterfaceMessageCreation

            Bus.Publish<MyInterfaceMessage>(o =>
            {
                o.OrderNumber = 1234;
            });

            #endregion

            #region 4to5ReflectionInterfaceMessageCreation

            // This type would be derived from some other runtime information
            var messageType = typeof(MyInterfaceMessage);

            var instance = messageCreator.CreateInstance(messageType);

            // use reflection to set properties on the constructed instance

            Bus.Publish(instance);

            #endregion
        }

        public interface MyInterfaceMessage
        {
            int OrderNumber { get; set; }
        }

        void CustomRavenConfig()
        {
            #region 4to5CustomRavenConfig

            var documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "MyDatabase",
            };

            documentStore.Initialize();

            var busConfiguration = new BusConfiguration();

            var persistence = busConfiguration.UsePersistence<RavenDBPersistence>();
            persistence.SetDefaultDocumentStore(documentStore);

            #endregion
        }

        void StartupAction()
        {
            #region 4to5StartupAction

            var startableBus = Bus.Create(new BusConfiguration());
            MyCustomAction();
            startableBus.Start();

            #endregion
        }

        public void MyCustomAction()
        {

        }

        void Installers(BusConfiguration busConfiguration)
        {
            #region 4to5Installers

            busConfiguration.EnableInstallers();
            // this will run the installers
            Bus.Create(busConfiguration);

            #endregion
        }

        void AllThePersistence(BusConfiguration busConfiguration)
        {
#pragma warning disable 618

            #region 4to5ConfigurePersistence

            // Configure to use InMemory for all persistence types
            busConfiguration.UsePersistence<InMemoryPersistence>();

            // Configure to use InMemory for specific persistence types
            busConfiguration.UsePersistence<InMemoryPersistence>()
                .For(
                    Storage.Sagas,
                    Storage.Subscriptions);

            // Configure to use NHibernate for all persistence types
            busConfiguration.UsePersistence<NHibernatePersistence>();

            // Configure to use NHibernate for specific persistence types
            busConfiguration.UsePersistence<NHibernatePersistence>()
                .For(
                    Storage.Sagas,
                    Storage.Subscriptions);

            // Configure to use RavenDB for all persistence types
            busConfiguration.UsePersistence<RavenDBPersistence>();

            // Configure to use RavenDB for specific persistence types
            busConfiguration.UsePersistence<RavenDBPersistence>()
                .For(
                    Storage.Sagas,
                    Storage.Subscriptions);

            #endregion

#pragma warning restore 618
        }

        #region 4to5BusExtensionMethodForHandler

        public class MyHandler :
            IHandleMessages<MyMessage>
        {
            IBus bus;

            public MyHandler(IBus bus)
            {
                this.bus = bus;
            }

            public void Handle(MyMessage message)
            {
                var otherMessage = new OtherMessage();
                bus.Reply(otherMessage);
            }
        }

        #endregion

        public class MyMessage
        {
        }

        public class OtherMessage
        {
        }

        void RunCustomAction()
        {
            #region 4to5RunCustomAction

            var startableBus = Bus.Create(new BusConfiguration());
            MyCustomAction();
            startableBus.Start();

            #endregion
        }

        void DefineCriticalErrorAction(BusConfiguration busConfiguration, ILog log)
        {
            #region 4to5DefineCriticalErrorAction

            // Configuring how NServiceBus handles critical errors
            busConfiguration.DefineCriticalErrorAction(
                (message, exception) =>
                {
                    var output = $"Critical exception: '{message}'";
                    log.Error(output, exception);
                    // Perhaps end the process??
                });

            #endregion
        }

        void PurgeOnStartup(BusConfiguration busConfiguration)
        {
            #region 4to5PurgeOnStartup

            busConfiguration.PurgeOnStartup(true);

            #endregion
        }

        void License(BusConfiguration busConfiguration)
        {
            #region 4to5License

            busConfiguration.LicensePath("PathToLicense");
            // or
            busConfiguration.License("YourCustomLicenseText");

            #endregion
        }

        void TransactionConfig(BusConfiguration busConfiguration)
        {
            #region 4to5TransactionConfig

            // Enable
            busConfiguration.Transactions().Enable();

            // Disable
            busConfiguration.Transactions().Disable();

            #endregion
        }

        void StaticConfigureEndpoint(BusConfiguration busConfiguration)
        {
            #region 4to5StaticConfigureEndpoint

            // SendOnly
            Bus.CreateSendOnly(busConfiguration);

            // AsVolatile
            var transactions = busConfiguration.Transactions();
            transactions.Disable();
            busConfiguration.DisableDurableMessages();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            // DisableDurableMessages
            busConfiguration.DisableDurableMessages();

            // EnableDurableMessages
            busConfiguration.EnableDurableMessages();

            #endregion
        }

        void PerformanceMonitoring(BusConfiguration busConfiguration)
        {
            #region 4to5PerformanceMonitoring

            busConfiguration.EnableSLAPerformanceCounter();
            // or
            busConfiguration.EnableSLAPerformanceCounter(TimeSpan.FromMinutes(3));

            #endregion
        }

        void DoNotCreateQueues(BusConfiguration busConfiguration)
        {
            #region 4to5DoNotCreateQueues

            busConfiguration.DoNotCreateQueues();

            #endregion
        }

        void EndpointName(BusConfiguration busConfiguration)
        {
            #region 4to5EndpointName

            busConfiguration.EndpointName("MyEndpoint");

            #endregion
        }

        void SendOnly(BusConfiguration busConfiguration)
        {
            #region 4to5SendOnly

            var sendOnlyBus = Bus.CreateSendOnly(busConfiguration);

            #endregion
        }
    }
}