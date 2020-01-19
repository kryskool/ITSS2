using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Serialization.Xml.Extensions.StringExtensions;

namespace Reth.Protocols.Serialization.Xml
{
    public class XmlDataContractSerializer<TTypeMappings>
        where TTypeMappings:ITypeMappings
    {        
        private static Lazy<TTypeMappings> typeMappings = new Lazy<TTypeMappings>(  () =>
                                                                                    {
                                                                                        TTypeMappings result = default( TTypeMappings );
                                                        
                                                                                        try
                                                                                        {
                                                                                            ExecutionLogProvider.LogInformation( $"Creating type mappings ({ typeof( TTypeMappings ) })." );

                                                                                            result = ( TTypeMappings )( Activator.CreateInstance( typeof( TTypeMappings ) ) );
                                                                                        }catch( Exception ex )
                                                                                        {
                                                                                            ExecutionLogProvider.LogError( ex );
                                                                                            ExecutionLogProvider.LogError( $"Failed to create type mappings ({ typeof( TTypeMappings ) })." );
                                                                                        }

                                                                                        return result;
                                                                                    },
                                                                                    LazyThreadSafetyMode.ExecutionAndPublication);

        public XmlDataContractSerializer()
        {
        }

        public TTypeMappings TypeMappings
        {
            get{ return XmlDataContractSerializer<TTypeMappings>.typeMappings.Value; }
        }
        
        public XmlSerializable<TMessage, TTypeMappings> CreateDataContract<TMessage>()
            where TMessage:class
        {
            ITypeMappings typeMappings = this.TypeMappings;

            XmlSerializable<TMessage, TTypeMappings> result = null;

            String typeName = typeof( TMessage ).Name;

            try
            {
                ITypeMapping typeMapping = typeMappings.GetTypeMapping( typeName );

                if( !( typeMapping is null ) )
                {
                    Object instance = Activator.CreateInstance( typeMapping.DataContractType );

                    result = ( XmlSerializable<TMessage, TTypeMappings> )instance;    
                }else
                {
                    ExecutionLogProvider.LogError( $"Type mapping not found ({ typeName })." );
                }
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
                ExecutionLogProvider.LogError( $"Failed to create type mapping ({ typeName })." );
            }

            return result;
        }

        public String WriteToString<TDataObject>( XmlSerializable<TDataObject, TTypeMappings> dataObject )
            where TDataObject:class
        {
            dataObject.ThrowIfNull();

            return this.WriteToString<TDataObject>( dataObject, XmlDataContractSerializer<TTypeMappings>.GetRoot<TDataObject>( dataObject.GetType() ) );
        }

        public String WriteToString<TDataObject>( XmlSerializable<TDataObject, TTypeMappings> dataObject, XmlRootAttribute root )
            where TDataObject:class
        {
            dataObject.ThrowIfNull();
            root.ThrowIfNull();

            StringBuilder result = new StringBuilder();

            using( XmlWriter writer = XmlWriter.Create( result, XmlSerializationSettings.WriterSettings ) )
            {
                Type dataContract = dataObject.GetType();

                DataContractSerializer serializer = new DataContractSerializer( dataObject.GetType(), root.ElementName, root.Namespace );
                
                serializer.WriteObject( writer, dataObject );
            }

            return result.ToString();
        }

        public XmlSerializable<TDataObject, TTypeMappings> ReadFromString<TDataObject>( Type dataContract, String xml )
            where TDataObject:class
        {
            return this.ReadFromString<TDataObject>( dataContract, xml, XmlDataContractSerializer<TTypeMappings>.GetRoot<TDataObject>( dataContract ) );
        }

        public XmlSerializable<TDataObject, TTypeMappings> ReadFromString<TDataObject>( Type dataContract, String xml, XmlRootAttribute root )
            where TDataObject:class
        {
            XmlSerializable<TDataObject, TTypeMappings> result = default( XmlSerializable<TDataObject, TTypeMappings> );

            using( StringReader input = new StringReader( xml ) )
            {
                using( XmlReader reader = XmlReader.Create( input, XmlSerializationSettings.ReaderSettings ) )
                {
                    DataContractSerializer serializer = new DataContractSerializer( dataContract, root.ElementName, root.Namespace );

                    result = ( XmlSerializable<TDataObject, TTypeMappings> )serializer.ReadObject( reader );
                }
            }

            return result;
        }

        private static XmlRootAttribute GetRoot<TDataObject>( Type dataContract )
        {
            dataContract.ThrowIfNull();

            XmlRootAttribute result = new XmlRootAttribute( typeof( TDataObject ).Name );

            Object[] attributes = dataContract.GetCustomAttributes( typeof( XmlRootAttribute ), false );

            if( attributes.Length != 0 )
            {
                result = ( XmlRootAttribute )( attributes[ 0 ] );
            }

            return result;
        }

        private void ThrowIfReadFailed( XmlReader reader )
        {
            reader.ThrowIfNull();

            bool read = reader.Read();

            Debug.Assert( read == true, $"{ nameof( read ) } == true )" );

            if( read == false )
            {
                throw new InvalidOperationException( "No element to read." );
            }
        }

        private void ThrowIfMoveToAttributeFailed( XmlReader reader, String attributeName )
        {
            reader.ThrowIfNull();

            bool hasAttribute = reader.MoveToAttribute( attributeName );

            Debug.Assert( hasAttribute == true, $"{ nameof( hasAttribute ) } == true" );

            if( hasAttribute == false )
            {
                throw new FormatException( $"Attribute '{ attributeName }' not found." );
            }
        }

        public TDataObject ReadMandatoryAttribute<TDataObject>( XmlReader reader, String name ) where TDataObject:class
        {
            name.ThrowIfNull();

            this.ThrowIfMoveToAttributeFailed( reader, name );

            XmlSerializable<TDataObject, TTypeMappings> dataContract = this.CreateDataContract<TDataObject>();

            dataContract.ReadXml( reader );

            return dataContract.DataObject;
        }

        public String ReadMandatoryCData( XmlReader reader )
        {
            this.ThrowIfReadFailed( reader );

            XmlNodeType nodeType = reader.NodeType;

            Debug.Assert( nodeType == XmlNodeType.CDATA, $"{ nameof( nodeType ) } == XmlNodeType.CDATA" );

            if( nodeType != XmlNodeType.CDATA )
            {
                throw new InvalidOperationException( $"Node type of 'CDATA' expected. Actual: { nodeType.ToString() }" );
            }

            return reader.Value;
        }

        public TDataObject ReadMandatoryElement<TDataObject>( XmlReader reader, String name ) where TDataObject:class
        {
            name.ThrowIfNull();
            
            this.ThrowIfReadFailed( reader );

            XmlNodeType nodeType = reader.NodeType;

            Debug.Assert( nodeType == XmlNodeType.Element, $"{ nameof( nodeType ) } == XmlNodeType.Element" );

            if( nodeType != XmlNodeType.Element )
            {
                throw new InvalidOperationException( $"Node type of 'Element' expected. Actual: { nodeType.ToString() }" );
            }

            bool elementFound = String.Equals( reader.Name, name, XmlSerializationSettings.StringComparison );

            Debug.Assert( elementFound == true, $"{ nameof( elementFound ) } == true" );

            if( elementFound == false )
            {
                throw new InvalidOperationException( $"Element '{ name }' not found." );
            }

            XmlSerializable<TDataObject, TTypeMappings> dataContract = this.CreateDataContract<TDataObject>();

            dataContract.ReadXml( reader );

            return dataContract.DataObject;
        }

        public TEnum ReadMandatoryEnum<TEnum>( XmlReader reader, String name ) where TEnum:Enum
        {
            this.ThrowIfMoveToAttributeFailed( reader, name );
            
            String content = reader.ReadContentAsString();
                        
            return ( TEnum )( Enum.Parse(   typeof( TEnum ),
                                            content,
                                            true    ) );
        }

        public int ReadMandatoryInteger( XmlReader reader, String name )
        {
            this.ThrowIfMoveToAttributeFailed( reader, name );

            Object content = reader.ReadContentAs( typeof( int ), null );
            
            return ( int )( content );
        }

        public String ReadMandatoryString( XmlReader reader, String name )
        {
            this.ThrowIfMoveToAttributeFailed( reader, name );

            return reader.ReadContentAsString().Unescape();
        }

        public TDataObject ReadOptionalAttribute<TDataObject>( XmlReader reader, String name ) where TDataObject:class
        {
            return this.ReadOptionalAttribute<TDataObject>( reader, name, null );
        }

        public TDataObject ReadOptionalAttribute<TDataObject>( XmlReader reader, String name, TDataObject defaultValue ) where TDataObject:class
        {
            reader.ThrowIfNull();

            TDataObject result = default( TDataObject );

            if( reader.MoveToAttribute( name ) == true )
            {
                String content = reader.ReadContentAsString();

                if( String.IsNullOrEmpty( content ) == false )
                {
                    XmlSerializable<TDataObject, TTypeMappings> dataContract = this.CreateDataContract<TDataObject>();

                    dataContract.ReadXml( reader );

                    result = dataContract.DataObject;
                }else
                {
                    result = defaultValue;
                }
            }else
            {
                result = defaultValue;
            }

            return result;
        }

        public TDataObject ReadOptionalElement<TDataObject>( XmlReader reader, String name ) where TDataObject:class
        {
            reader.ThrowIfNull();

            TDataObject result = default( TDataObject );

            if( reader.Read() == true )
            {
                if( ( reader.NodeType == XmlNodeType.Element ) &&
                    ( String.Equals( reader.Name, name, XmlSerializationSettings.StringComparison ) == true )   )
                {
                    XmlSerializable<TDataObject, TTypeMappings> dataContract = this.CreateDataContract<TDataObject>();

                    dataContract.ReadXml( reader );

                    result = dataContract.DataObject;
                }
            }

            return result;
        }

        public IEnumerable<TDataObject> ReadOptionalElements<TDataObject>( XmlReader reader, String name ) where TDataObject:class
        {
            List<TDataObject> result = new List<TDataObject>();

            TDataObject dataObject = default( TDataObject );

            do
            {
                dataObject = this.ReadOptionalElement<TDataObject>( reader, name );

                if( !( dataObject is null ) )
                {
                    result.Add( dataObject );
                }
            }while( dataObject != null );

            return result;
        }

        public Nullable<TEnum> ReadOptionalEnum<TEnum>( XmlReader reader, String name ) where TEnum:struct, Enum 
        {
            return this.ReadOptionalEnum<TEnum>( reader, name, null );
        }

        public Nullable<TEnum> ReadOptionalEnum<TEnum>( XmlReader reader, String name, Nullable<TEnum> defaultValue ) where TEnum:struct, Enum 
        {
            reader.ThrowIfNull();

            Nullable<TEnum> result = null;
            
            if( reader.MoveToAttribute( name ) == true )
            {
                String content = reader.ReadContentAsString();

                TEnum value = ( TEnum )( Enum.Parse(    typeof( TEnum ),
                                                        content,
                                                        true    ) );

                result = new Nullable<TEnum>( value );
            }else
            {
                result = defaultValue;
            }

            return result;
        }

        public Nullable<bool> ReadOptionalBool( XmlReader reader, String name )
        {
            return this.ReadOptionalBool( reader, name, null );
        }

        public Nullable<bool> ReadOptionalBool( XmlReader reader, String name, Nullable<bool> defaultValue )
        {
            reader.ThrowIfNull();

            Nullable<bool> result = null;

            if( reader.MoveToAttribute( name ) == true )
            {
                result = bool.Parse( reader.ReadContentAsString() );
            }else
            {
                result = defaultValue;
            }

            return result;
        }

        public Nullable<int> ReadOptionalInteger( XmlReader reader, String name )
        {
            return this.ReadOptionalInteger( reader, name, null );
        }

        public Nullable<int> ReadOptionalInteger( XmlReader reader, String name, Nullable<int> defaultValue )
        {
            reader.ThrowIfNull();

            Nullable<int> result = null;

            if( reader.MoveToAttribute( name ) == true )
            {
                result = int.Parse( reader.ReadContentAsString(), CultureInfo.InvariantCulture );
            }else
            {
                result = defaultValue;
            }

            return result;
        }

        public String ReadOptionalString( XmlReader reader, String name )
        {
            reader.ThrowIfNull();

            String result = null;

            if( reader.MoveToAttribute( name ) == true )
            {
                result = reader.ReadContentAsString().Unescape();
            }

            return result;
        }

        public void WriteMandatoryAttribute<TDataObject>( XmlWriter writer, String name, TDataObject value ) where TDataObject:class
        {
            writer.ThrowIfNull();
            value.ThrowIfNull();

            writer.WriteAttributeString( name, value.ToString() );
        }

        public void WriteMandatoryCData( XmlWriter writer, String value )
        {
            writer.ThrowIfNull();
            value.ThrowIfNull();

            writer.WriteCData( value );
        }

        public void WriteMandatoryElement<TDataObject>( XmlWriter writer, String name, TDataObject element ) where TDataObject:class
        {
            if( !( element is null ) )
            {
                writer.ThrowIfNull();
                writer.WriteStartElement( name );

                XmlSerializable<TDataObject, TTypeMappings> dataContract = this.CreateDataContract<TDataObject>();
                        
                dataContract.DataObject = element;
                dataContract.WriteXml( writer );

                writer.WriteEndElement();
            }
        }

        public void WriteMandatoryEnum<TEnum>( XmlWriter writer, String name, TEnum value ) where TEnum:Enum
        {
            writer.ThrowIfNull();
            writer.WriteAttributeString( name, value.ToString() );
        }

        public void WriteMandatoryInteger( XmlWriter writer, String name, int value )
        {
            writer.ThrowIfNull();
            writer.WriteAttributeString( name, value.ToString( CultureInfo.InvariantCulture ) );
        }

        public void WriteMandatoryString( XmlWriter writer, String name, String value )
        {
            writer.ThrowIfNull();
            value.ThrowIfNull();

            writer.WriteAttributeString( name, value.Escape() );
        }

        public void WriteOptionalAttribute<TDataObject>( XmlWriter writer, String name, TDataObject value ) where TDataObject:class
        {
            if( !( value is null ) )
            {
                writer.ThrowIfNull();
                writer.WriteAttributeString( name, value.ToString() );
            }
        }

        public void WriteOptionalElement<TDataObject>( XmlWriter writer, String name, TDataObject element ) where TDataObject:class
        {
            if( !( element is null ) )
            {
                writer.ThrowIfNull();
                writer.WriteStartElement( name );

                XmlSerializable<TDataObject, TTypeMappings> dataContract = this.CreateDataContract<TDataObject>();
                        
                dataContract.DataObject = element;
                dataContract.WriteXml( writer );

                writer.WriteEndElement();
            }
        }

        public void WriteOptionalElements<TDataObject>( XmlWriter writer, String name, IEnumerable<TDataObject> elements ) where TDataObject:class
        {
            if( !( elements is null ) )
            {
                foreach( TDataObject element in elements )
                {
                    this.WriteOptionalElement<TDataObject>( writer, name, element );
                }
            }
        }

        public void WriteOptionalEnum<TEnum>( XmlWriter writer, String name, Nullable<TEnum> value ) where TEnum:struct, Enum
        {
            writer.ThrowIfNull();
            writer.WriteAttributeString( name, value.ToString() );
        }

        public void WriteOptionalBool( XmlWriter writer, String name, Nullable<bool> value )
        {
            if( !( value is null ) )
            {
                writer.ThrowIfNull();
                writer.WriteAttributeString( name, value.Value.ToString( CultureInfo.InvariantCulture ) );
            }
        }

        public void WriteOptionalInteger( XmlWriter writer, String name, Nullable<int> value )
        {
            if( !( value is null ) )
            {
                writer.ThrowIfNull();
                writer.WriteAttributeString( name, value.Value.ToString( CultureInfo.InvariantCulture ) );
            }
        }

        public void WriteOptionalString( XmlWriter writer, String name, String value )
        {
            if( !( value is null ) )
            {
                writer.ThrowIfNull();
                writer.WriteAttributeString( name, value.Escape() );
            }
        }
    }
}