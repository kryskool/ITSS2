using System;

namespace Reth.Protocols.Serialization
{
    public interface ITypeMapping
    {
        Type InterfaceType{ get; }
        Type InstanceType{ get; }

        Type DataContractType{ get; }        
    }
}