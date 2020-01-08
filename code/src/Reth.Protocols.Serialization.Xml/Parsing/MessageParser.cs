using System;
using System.Collections.Generic;
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
            String result = null;

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

        public IMessage Parse( String message )
        {
            IMessage result = null;

            String messageName = MessageParser.GetMessageName( message );

            if( !( messageName is null ) )
            {
                try
                {
                    ITypeMapping typeMapping = this.TypeMappings.GetTypeMapping( messageName );
                    
                    if( !( typeMapping is null ) )
                    {
                        dynamic envelope = null;

                        try
                        {
                            using( StringReader input = new StringReader( message ) )
                            {
                                using( XmlReader reader = XmlReader.Create( input, XmlSerializationSettings.ReaderSettings ) )
                                {
                                    Type envelopeDataContractType = this.TypeMappings.EnvelopeDataContractType.MakeGenericType( typeMapping.InstanceType, this.TypeMappings.GetType() );

                                    DataContractSerializer serializer = MessageParser.GetSerializer( envelopeDataContractType );

                                    envelope = serializer.ReadObject( reader );
                                }
                            }
                        }catch( Exception ex )
                        {
                            ExecutionLogProvider.LogError( ex );
                            ExecutionLogProvider.LogError( $"Deserialization of message failed: '{ message }'" );

                            UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.InvalidFormat,
                                                                                    MessageDirection.Incoming,
                                                                                    new RawMessage( message ),
                                                                                    ex  ) );
                        }

                        if( !( envelope is null ) )
                        {
                            result = envelope.DataObject.Message;
                        }
                    }else
                    {
                        ExecutionLogProvider.LogError( $"Type mapping not found: { message }." );
                    }
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                    ExecutionLogProvider.LogError( $"Determination of type mapping failed: { message }." );

                    UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.UnknownError,
                                                                            MessageDirection.Incoming,
                                                                            new RawMessage( message ),
                                                                            ex  ) );
                }
            }else
            {
                ExecutionLogProvider.LogError( $"Determination of message type failed: { message }." );

                UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.InvalidFormat,
                                                                        MessageDirection.Incoming,
                                                                        new RawMessage( message )   ) );
            }

            return result;
        }

        public String Parse( IMessage message )
        {
            String result = String.Empty;
            
            if( !( message is null ) )
            {
                Type messageType = message.GetType();

                if( messageType == typeof( RawMessage ) )
                {
                    RawMessage rawMessage = ( RawMessage )message;

                    result = rawMessage.Value;
                }else
                {
                    try
                    {
                        Type envelopeType = this.TypeMappings.EnvelopeType.MakeGenericType( messageType );
                        Type envelopeDataContractType = this.TypeMappings.EnvelopeDataContractType.MakeGenericType( messageType, this.TypeMappings.GetType() );

                        Object envelope = Activator.CreateInstance( envelopeType, message );
                        Object dataObject = Activator.CreateInstance( envelopeDataContractType, envelope );

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
                            ExecutionLogProvider.LogError( ex );
                            ExecutionLogProvider.LogError( $"Serializing of message failed: { message }." );

                            UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.InvalidFormat,
                                                                                    MessageDirection.Outgoing,
                                                                                    message,
                                                                                    ex  ) );
                        }

                        result = buffer.ToString();
                    }catch( Exception ex )
                    {
                        ExecutionLogProvider.LogError( ex );
                        ExecutionLogProvider.LogError( $"Message envelope cannot be created: { message }." );

                        UnhandledMessageHandler.Invoke( new UnhandledMessage(   UnhandledReason.UnknownError,
                                                                                MessageDirection.Outgoing,
                                                                                message,
                                                                                ex  ) );
                    }
                }
            }

            return result;
        }
    }
}