using Reth.Protocols.Dialogs;

namespace Reth.Protocols.Serialization.Xml
{
    public interface IMessageEnvelopeDataContract
    {
        IMessageEnvelope DataObject{ get; }
    }
}
