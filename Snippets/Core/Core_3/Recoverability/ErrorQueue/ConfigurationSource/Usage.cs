namespace Core3.Recoverability.ErrorQueue.ConfigurationSource
{
    using NServiceBus;

    class Usage
    {
        Usage(Configure configure)
        {
            #region UseCustomConfigurationSourceForErrorQueueConfig

            configure.CustomConfigurationSource(new ConfigurationSource());

            #endregion
        }
    }
}