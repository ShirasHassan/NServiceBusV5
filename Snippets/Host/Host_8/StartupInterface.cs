using System.Threading.Tasks;
using NServiceBus;

#pragma warning disable 618
#region HostStartAndStop
public class Bootstrapper :
    IWantToRunWhenEndpointStartsAndStops
{
    public Task Start(IMessageSession session)
    {
        // Do startup actions here.
        // Either mark Start method as async or do the following
        return Task.CompletedTask;
    }

    public Task Stop(IMessageSession session)
    {
        // Do cleanup actions here.
        // Either mark Stop method as async or do the following
        return Task.CompletedTask;
    }
}
#endregion
#pragma warning restore 618