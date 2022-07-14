﻿namespace Core3.Recoverability
{
    using System;
    using NServiceBus;
    using NServiceBus.Faults;
    using NServiceBus.Unicast.Transport;

    #region CustomFaultManager

    public class CustomFaultManager :
        IManageMessageFailures
    {
        public void SerializationFailedForMessage(TransportMessage message, Exception e)
        {
            // implement steps for this message when the failure is due to deserialization
        }

        public void ProcessingAlwaysFailsForMessage(TransportMessage message, Exception e)
        {
            // implement steps for this message after it fails all Immediate Retry attempts
        }

        public void Init(Address address)
        {
            // implement initializations for the custom fault manager.
        }
    }

    #endregion

    class RegisterFaultManager
    {

        RegisterFaultManager()
        {
            #region RegisterFaultManager

            var components = Configure.Instance.Configurer;
            components.ConfigureComponent<CustomFaultManager>(DependencyLifecycle.InstancePerCall);

            #endregion
        }
    }

}