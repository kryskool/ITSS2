namespace Reth.Protocols.Serialization
{
    public interface IProtocolProvider
    {
        ITypeMappings TypeMappings{ get; }

        IMessageSerializer MessageSerializer{ get; }
    }
}
