namespace Core7
{
    using System;
    using System.Threading.Tasks;
    using Autofac;
    using NServiceBus;

    class Hosting
    {
        async Task SendOnly()
        {
            #region Hosting-SendOnly

            var endpointConfiguration = new EndpointConfiguration("EndpointName");
            endpointConfiguration.SendOnly();
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            #endregion
        }

        async Task Startup()
        {
            #region Hosting-Startup
            var endpointConfiguration = new EndpointConfiguration("EndpointName");
            // Apply configuration
            var startableEndpoint = await Endpoint.Create(endpointConfiguration)
                .ConfigureAwait(false);
            var endpointInstance = await startableEndpoint.Start()
                .ConfigureAwait(false);

            // Shortcut
            var endpointInstance2 = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            #endregion
        }

        async Task Shutdown(IEndpointInstance endpointInstance)
        {
            #region Hosting-Shutdown
            await endpointInstance.Stop().ConfigureAwait(false);
            #endregion
        }

        #region Hosting-Static
        public static class EndpointInstance
        {
            public static IEndpointInstance Endpoint { get; private set; }
            public static void SetInstance(IEndpointInstance endpoint)
            {
                if (Endpoint != null)
                {
                    throw new Exception("Endpoint already set.");
                }
                Endpoint = endpoint;
            }
        }
        #endregion

        async Task InjectEndpoint()
        {
            #region Hosting-Inject

            var containerBuilder = new ContainerBuilder();
            var endpointConfiguration = new EndpointConfiguration("EndpointName");
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            containerBuilder.Register(_ => endpointInstance).InstancePerDependency();

            #endregion

        }

    }
}
