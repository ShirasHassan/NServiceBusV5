namespace Core5
{
    using System;
    using NServiceBus;

    class TimeToWaitBeforeTriggeringCriticalErrorOnTimeoutOutages
    {
        TimeToWaitBeforeTriggeringCriticalErrorOnTimeoutOutages(BusConfiguration busConfiguration)
        {
            #region TimeToWaitBeforeTriggeringCriticalErrorOnTimeoutOutages

            busConfiguration.TimeToWaitBeforeTriggeringCriticalErrorOnTimeoutOutages(
                timeToWait: TimeSpan.FromMinutes(5));

            #endregion
        }

    }
}