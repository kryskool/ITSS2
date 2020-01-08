using Reth.Itss2.Standard.Dialogs.General.Hello;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows
{
    public abstract class SubscriberNodeFactory
    {
        public SubscriberNodeFactory( SubscriberInfo subscriberInfo )
        {
            subscriberInfo.ThrowIfNull();

            this.SubscriberInfo = subscriberInfo;
        }

        public SubscriberInfo SubscriberInfo
        {
            get;
        }
    }
}
