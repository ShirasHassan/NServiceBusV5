namespace Core5.Headers
{
    using System;
    using NServiceBus;
    using NServiceBus.Pipeline;
    using NServiceBus.Pipeline.Contexts;

    #region header-incoming-behavior

    public class IncomingBehavior :
        IBehavior<IncomingContext>
    {
        public void Invoke(IncomingContext context, Action next)
        {
            var headers = context.PhysicalMessage.Headers;
            var nsbVersion = headers[Headers.NServiceBusVersion];
            var customHeader = headers["MyCustomHeader"];
            next();
        }
    }

    #endregion
}