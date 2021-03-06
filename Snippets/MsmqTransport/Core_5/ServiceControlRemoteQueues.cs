
using NServiceBus;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;



#region ErrorQueueRemoteMachineConfigurationProvider

class ProvideConfiguration : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
{
    public MessageForwardingInCaseOfFaultConfig GetConfiguration()
    {
        return new MessageForwardingInCaseOfFaultConfig
        {
            ErrorQueue = "error@machinename"
        };
    }
}

#endregion

#region ErrorQueueRemoteMachineConfigurationSource
public class ConfigurationSource : IConfigurationSource
{
    public T GetConfiguration<T>() where T : class, new()
    {
        if (typeof(T) == typeof(MessageForwardingInCaseOfFaultConfig))
        {
            var config = new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = "error@machinename"
            };

            return config as T;
        }

        // Respect app.config for other sections not defined in this method
        return System.Configuration.ConfigurationManager.GetSection(typeof(T).Name) as T;
    }
}

#endregion

class CustomConfigurationSource
{
    CustomConfigurationSource(BusConfiguration busConfiguration)
    {
        #region UseCustomConfigurationSourceForErrorQueueRemoateMachineConfig

        busConfiguration.CustomConfigurationSource(new ConfigurationSource());

        #endregion
    }
}