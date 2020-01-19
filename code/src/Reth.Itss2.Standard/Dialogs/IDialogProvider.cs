using Reth.Itss2.Standard.Dialogs.General.Unprocessed;

using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs
{
    public interface IDialogProvider:IMessageTransceiver
    {
        IUnprocessedDialog Unprocessed{ get; }

        IDialogName[] GetSupportedDialogs();
    }
}