using System;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Serialization.Xml
{
    internal class MessageSerializer:IMessageSerializer
    {
        public MessageSerializer( ITypeMappings typeMappings )
        {
            typeMappings.ThrowIfNull();

            this.TypeMappings = typeMappings;
        }

        ~MessageSerializer()
        {
            this.Dispose( false );
        }

        public ITypeMappings TypeMappings
        {
            get;
        }

        public IMessageStreamReader GetStreamReader( IInteractionLog interactionLog )
        {
            return new MessageStreamReader( interactionLog, this.TypeMappings );
        }

        public IMessageStreamWriter GetStreamWriter( IInteractionLog interactionLog )
        {
            return new MessageStreamWriter( interactionLog, this.TypeMappings );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}