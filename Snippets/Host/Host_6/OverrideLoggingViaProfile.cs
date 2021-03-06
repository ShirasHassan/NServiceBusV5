using NServiceBus;
using NServiceBus.Logging;

class OverrideLoggingViaProfile
{
    #region LoggingConfigWithProfile

    public class YourProfileLoggingHandler :
        NServiceBus.Hosting.Profiles.IConfigureLoggingForProfile<YourProfile>
    {
        public void Configure(IConfigureThisEndpoint specifier)
        {
            // setup logging infrastructure
            LogManager.Use<Log4NetFactory>();
        }

    }

    #endregion

    class Log4NetFactory :
        LoggingFactoryDefinition
    {
        protected override ILoggerFactory GetLoggingFactory()
        {
            throw new System.NotImplementedException();
        }
    }

    class YourProfile :
        IProfile
    {
    }
}