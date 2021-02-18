// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml
{
    public class XmlMessageParser:MessageParser
    {
        public XmlMessageParser( Type serializationProvider )
        :
            base( new XmlDataContractResolver( serializationProvider ) )
        {
        }

        public XmlMessageParser( ISerializationProvider serializationProvider )
        :
            base( new XmlDataContractResolver( serializationProvider ) )
        {
        }

        private XmlSerializerNamespaces Namespaces
        {
            get;
        } = new XmlSerializerNamespaces( new[]{ XmlQualifiedName.Empty } );

        public override IMessageEnvelope DeserializeMessageEnvelope( String messageEnvelope )
        {
            String messageName = this.GetMessageName( messageEnvelope );

            Type dataContractType = this.DataContractResolver.ResolveContract( messageName ).MessageEnvelopeDataContractType;

            try
            {
                using( StringReader input = new StringReader( messageEnvelope ) )
                {
                    using( XmlReader reader = XmlReader.Create( input, XmlSerializationSettings.ReaderSettings ) )
                    {
                        XmlSerializer serializer = SerializationManager.GetSerializer( dataContractType );

                        IDataContract<IMessageEnvelope> envelopeDataContract = ( IDataContract<IMessageEnvelope> )( serializer.Deserialize( reader ) );

                        return envelopeDataContract.GetDataObject();
                    }
                }
            }catch( Exception ex )
            {
                throw Assert.Exception( new MessageSerializationException( $"Deserialization of message envelope (truncated) '{ messageEnvelope.Truncate() }' failed.", ex ) );
            }
        }

        public override IMessage DeserializeMessage( String message )
        {
            String messageName = this.GetMessageName( message );

            Type dataContractType = this.DataContractResolver.ResolveContract( messageName ).MessageDataContractType;

            try
            {
                using( StringReader input = new StringReader( message ) )
                {
                    using( XmlReader reader = XmlReader.Create( input, XmlSerializationSettings.ReaderSettings ) )
                    {
                        XmlSerializer serializer = SerializationManager.GetSerializer( dataContractType );

                        IDataContract<IMessage> dataContract = ( IDataContract<IMessage> )( serializer.Deserialize( reader ) );

                        return dataContract.GetDataObject();
                    }
                }
            }catch( Exception ex )
            {
                throw Assert.Exception( new MessageSerializationException( $"Deserialization of message (truncated) '{ message.Truncate() }' failed.", ex ) );
            }
        }

        public override String SerializeMessageEnvelope( IMessageEnvelope messageEnvelope )
        {
            StringBuilder result = new StringBuilder();

            Type dataContractType = this.DataContractResolver.ResolveContract( messageEnvelope.Message.Name ).MessageEnvelopeDataContractType;

            using( XmlWriter writer = XmlWriter.Create( result, XmlSerializationSettings.WriterSettings ) )
            {
                try
                {
                    XmlSerializer serializer = SerializationManager.GetSerializer( dataContractType );
                    
                    Object dataContract = Activator.CreateInstance( dataContractType, messageEnvelope );

                    serializer.Serialize( writer, dataContract, this.Namespaces );
                }catch( Exception ex )
                {
                    throw Assert.Exception( new MessageSerializationException( $"Serialization of message envelope '{ messageEnvelope } ({ messageEnvelope.Timestamp })' failed.", ex ) );
                }
            }

            return result.ToString();
        }

        public override String SerializeMessage( IMessage message )
        {
            StringBuilder result = new StringBuilder();

            String messageName = message.Name;

            Type dataContractType = this.DataContractResolver.ResolveContract( messageName ).MessageDataContractType;

            using( XmlWriter writer = XmlWriter.Create( result, XmlSerializationSettings.WriterSettings ) )
            {
                try
                {
                    XmlSerializer serializer = SerializationManager.GetSerializer( dataContractType, new XmlRootAttribute( messageName ) );
                    
                    Object dataContract = Activator.CreateInstance( dataContractType, message );

                    serializer.Serialize( writer, dataContract, this.Namespaces );
                }catch( Exception ex )
                {
                    throw Assert.Exception( new MessageSerializationException( $"Serialization of message '{ message }' failed.", ex ) );
                }
            }

            return result.ToString();
        }

        private String GetMessageName( String message )
        {
            String result = String.Empty;

            if( String.IsNullOrEmpty( message ) == false )
            {
                int indexOfNameStart = ( message.IndexOf( "<", 1,StringComparison.InvariantCultureIgnoreCase ) + 1 );

                if( indexOfNameStart > 0 )
                {
                    int indexOfNameEnd = message.IndexOf( " ", indexOfNameStart, StringComparison.InvariantCultureIgnoreCase );

                    if( indexOfNameEnd >= 0 )
                    {
                        int lengthOfName = indexOfNameEnd - indexOfNameStart;

                        result = message.Substring( indexOfNameStart, lengthOfName );
                    }
                }
            }

            bool messageTypeFound = !( String.IsNullOrEmpty( result ) );

            if( messageTypeFound == false )
            {
                throw Assert.Exception( new MessageNotSupportedException( $"Determination of message name failed." ) );
            }

            return result;
        }
    }
}
