using Reth.Itss2.Standard.Dialogs.General.Hello;

namespace Reth.Itss2.Standard.Workflows
{
    public interface ISubscriberNode
    {
        Subscriber LocalSubscriber{ get; }
        Subscriber RemoteSubscriber{ get; }
    }
}
