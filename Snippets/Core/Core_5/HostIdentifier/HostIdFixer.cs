﻿#pragma warning disable 618

namespace Core5.HostIdentifier
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;
    using NServiceBus;
    using NServiceBus.Config;
    using NServiceBus.Hosting;
    using NServiceBus.Settings;
    using NServiceBus.Unicast;

    #region HostIdFixer

    public class HostIdFixer :
        IWantToRunWhenConfigurationIsComplete
    {

        public HostIdFixer(UnicastBus bus, ReadOnlySettings settings)
        {
            var hostId = CreateGuid(Environment.MachineName, settings.EndpointName());
            var location = Assembly.GetExecutingAssembly().Location;
            var properties = new Dictionary<string, string>
            {
                {"Location", location}
            };
            bus.HostInformation = new HostInformation(
                hostId: hostId,
                displayName: Environment.MachineName,
                properties: properties);
        }

        static Guid CreateGuid(params string[] data)
        {
            using (var provider = new MD5CryptoServiceProvider())
            {
                var inputBytes = Encoding.Default.GetBytes(string.Concat(data));
                var hashBytes = provider.ComputeHash(inputBytes);
                return new Guid(hashBytes);
            }
        }

        public void Run(Configure config)
        {
        }
    }

    #endregion

}