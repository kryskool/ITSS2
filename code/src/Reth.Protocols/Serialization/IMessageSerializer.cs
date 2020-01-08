using System;

using Reth.Protocols.Diagnostics;

namespace Reth.Protocols.Serialization
{
    public interface IMessageSerializer:IDisposable
    {
        ITypeMappings TypeMappings{ get; }

        IMessageStreamReader GetStreamReader( IInteractionLog interactionLog );
        IMessageStreamWriter GetStreamWriter( IInteractionLog interactionLog );
    }
}