using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Serialization.Xml.Parsing
{
    public class MessageParser
    {
        private static Object SyncRoot
        {
            get;
        } = new Object();

        private static Dictionary<Type, DataContractSerializer> Serializers
        {
            get;
        } = new Dictionary<Type, DataContractSerializer>();

        public static String GetMessageName( String message )
        {
            message.ThrowIfNull();

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

            Debug.Assert( messageTypeFound == true, $"{ messageTypeFound } == true" );

            if( messageTypeFound == false )
            {
                throw new MessageTypeException( $"Determination of message type failed." );
            }

            return result;
        }

        private static DataContractSerializer GetSerializer( Type envelopeDataContractType )
        {
            DataContractSerializer result = null;

            lock( MessageParser.SyncRoot )
            {
                if( MessageParser.Serializers.TryGetValue( envelopeDataContractType, out result ) == false )
                {
                    result = new DataContractSerializer( envelopeDataContractType );

                    MessageParser.Serializers.Add( envelopeDataContractType, result );
                }
            }

            return result;
        }

        public MessageParser( ITypeMappings typeMappings )
        {
            typeMappings.ThrowIfNull();

            this.TypeMappings = typeMappings;
        }

        public ITypeMappings TypeMappings
        {
            get;
        }

        public bool TryParse( String message, out IMessage result )
        {
            result = null;
            
            bool success = false;

            try
            {
                String messageName = MessageParser.GetMessageName( message );

                ITypeMapping typeMapping = this.TypeMappings.GetTypeMapping( messageName );
                Type envelopeDataContractType = this.TypeMappings.EnvelopeDataContractType.MakeGenericType( typeMapping.InstanceType, this.TypeMappings.GetType() );

                using( StringReader input = new StringReader( message ) )
                {
                    using( XmlReader reader = XmlReader.Create( input, XmlSerializationSettings.ReaderSettings ) )
                    {
                        DataContractSerializer serializer = MessageParser.GetSerializer( envelopeDataContractType );

                        IMessageEnvelopeDataContract envelope = ( IMessageEnvelopeDataContract )( serializer.ReadObject( reader ) );

                        result = envelope.DataObject.Message;

                        success = true;
                    }
                }
            }catch
            {
            }

            return success;
        }

        public IMessage Parse( String message )
        {
            message.ThrowIfNull();

            IMessage result = null;

            String messageName = String.Empty;

            Type envelopeDataContractType = null;

            try
            {
                messageName = MessageParser.GetMessageName( message );

                ITypeMapping typeMapping = this.TypeMappings.GetTypeMapping( messageName );
                    
                envelopeDataContractType = this.TypeMappings.EnvelopeDataContractType.MakeGenericType( typeMapping.InstanceType, this.TypeMappings.GetType() );
            }catch( Exception ex )
            {
                throw new MessageTypeException( "Unknown message type.", ex );
            }

            try
            {
                using( StringReader input = new StringReader( message ) )
                {
                    using( XmlReader reader = XmlReader.Create( input, XmlSerializationSettings.ReaderSettings ) )
                    {
                        DataContractSerializer serializer = MessageParser.GetSerializer( envelopeDataContractType );

                        IMessageEnvelopeDataContract envelope = ( IMessageEnvelopeDataContract )( serializer.ReadObject( reader ) );

                        result = envelope.DataObject.Message;
                    }
                }
            }catch( Exception ex )
            {
                throw new MessageSerializationException( $"Deserialization failed: '{ messageName }'", ex );
            }

            return result;
        }

        public bool TryParse( IMessage message, out String result )
        {
            result = null;

            bool success = false;

            if( !( message is null ) )
            {
                try
                {
                    Type messageType = message.GetType();

                    if( messageType == typeof( RawMessage ) )
                    {
                        RawMessage rawMessage = ( RawMessage )message;

                        result = rawMessage.Value;
                    }else
                    {
                        Type envelopeDataContractType = this.TypeMappings.EnvelopeDataContractType.MakeGenericType( messageType, this.TypeMappings.GetType() );
                    
                        Object envelope = Activator.CreateInstance( this.TypeMappings.EnvelopeType,
                                                                    message );

                        Object dataObject = Activator.CreateInstance( envelopeDataContractType, envelope );
                        
                        StringBuilder buffer = new StringBuilder();

                        using( XmlWriter writer = XmlWriter.Create( buffer, XmlSerializationSettings.WriterSettings ) )
                        {
                            DataContractSerializer serializer = MessageParser.GetSerializer( envelopeDataContractType );

                            serializer.WriteObject( writer, dataObject );
                        }

                        result = buffer.ToString();
                    }
                }catch
                {
                }
            }

            return success;
        }

        public String Parse( IMessage message )
        {
            message.ThrowIfNull();

            String result = String.Empty;
            
            Type messageType = message.GetType();

            if( messageType == typeof( RawMessage ) )
            {
                RawMessage rawMessage = ( RawMessage )message;

                result = rawMessage.Value;
            }else
            {
                Type envelopeDataContractType = null;
                Object dataObject = null;

                try
                {
                    envelopeDataContractType = this.TypeMappings.EnvelopeDataContractType.MakeGenericType( messageType, this.TypeMappings.GetType() );
                    
                    Object envelope = Activator.CreateInstance( this.TypeMappings.EnvelopeType,
                                                                message );

                    dataObject = Activator.CreateInstance( envelopeDataContractType, envelope );
                }catch( Exception ex )
                {
                    throw new MessageTypeException( $"Message envelope cannot be created: '{ message.GetType().Name }'", ex );
                }

                StringBuilder buffer = new StringBuilder();

                try
                {
                    using( XmlWriter writer = XmlWriter.Create( buffer, XmlSerializationSettings.WriterSettings ) )
                    {
                        DataContractSerializer serializer = MessageParser.GetSerializer( envelopeDataContractType );

                        serializer.WriteObject( writer, dataObject );
                    }
                }catch( Exception ex )
                {
                    throw new MessageSerializationException( $"Serialization failed: '{ message.GetType().Name }'.", ex );
                }

                result = buffer.ToString();
            }

            return result;
        }
    }
}