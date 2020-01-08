using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Protocols.Transfer;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server
{
    public interface IStorageSystemServer:IMessageServer
    {
        SubscriberInfo SubscriberInfo{ get; }
        
        StorageSystemCollection StorageSystems{ get; }
    }
}
