namespace Core6.DataBus.Custom
{
    using NServiceBus;

    class Usage
    {
        Usage(EndpointConfiguration endpointConfiguration)
        {
            #region PluginCustomDataBus
            endpointConfiguration.UseDataBus(typeof(CustomDataBus));

            #endregion
        }
    }
}
