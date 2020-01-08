using System;

namespace Reth.Protocols.Serialization
{
    public interface ITypeMappings
    {
        Type EnvelopeType{ get; }
        Type EnvelopeDataContractType{ get; }

        ITypeMapping GetTypeMapping( String typeName );
    }
}
