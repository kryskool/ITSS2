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
using System.Text.RegularExpressions;
using System.Text.Json;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json
{
    public class JsonMessageParser:MessageParser
    {
        private const String GroupName = "MessageName";

        public JsonMessageParser( Type serializationProvider )
        :
            base( new JsonDataContractResolver( serializationProvider ) )
        {
        }

        public JsonMessageParser( ISerializationProvider serializationProvider )
        :
            base( new JsonDataContractResolver( serializationProvider ) )
        {
        }

        private MessageSerializationException GetDeserializationException( String messageEnvelope, Exception? innerException )
        {
            String message = $"Deserialization of message (truncated) '{ messageEnvelope.Truncate() }' failed.";

            if( innerException is not null )
            {
                return new MessageSerializationException( message, innerException );
            }

            return new MessageSerializationException( message );
        }

        public override IMessageEnvelope Deserialize( String messageEnvelope )
        {
            String messageName = this.GetMessageName( messageEnvelope );

            Type dataContractType = this.DataContractResolver.ResolveContract( messageName );

            try
            {
                Object? deserializedObject = JsonSerializer.Deserialize( messageEnvelope, dataContractType , JsonSerializationSettings.DeserializerOptions );

                IDataContract<IMessageEnvelope>? envelopeDataContract = ( IDataContract<IMessageEnvelope>? )( deserializedObject );

                if( envelopeDataContract is null )
                {
                    throw Assert.Exception( this.GetDeserializationException( messageEnvelope, null ) );    
                }

                return envelopeDataContract.GetDataObject();
            }catch( Exception ex )
            {
                throw Assert.Exception( this.GetDeserializationException( messageEnvelope, ex ) );
            }
        }

        public override String Serialize( IMessageEnvelope messageEnvelope )
        {
            String result = String.Empty;

            Type dataContractType = this.DataContractResolver.ResolveContract( messageEnvelope.Message.GetName() );

            try
            {
                result = JsonSerializer.Serialize( messageEnvelope, dataContractType, JsonSerializationSettings.SerializerOptions );
            }catch( Exception ex )
            {
                throw Assert.Exception( new MessageSerializationException( $"Serialization of message '{ messageEnvelope } ({ messageEnvelope.Timestamp })' failed.", ex ) );
            }

            return result;
        }        

        public override String GetMessageName( String message )
        {
            String result = String.Empty;

            if( String.IsNullOrEmpty( message ) == false )
            {
                Match match = Regex.Match( message, $@"^\s*\{{\s*\""(?<{ JsonMessageParser.GroupName }>[a-zA-Z]+)\""\:", RegexOptions.IgnoreCase );

                if( match.Success == true )
                {
                    Group messageNameGroup = match.Groups[ JsonMessageParser.GroupName ];

                    if( messageNameGroup.Success == true )
                    {
                        result = messageNameGroup.Value;
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