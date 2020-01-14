using System;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Serialization.Xml
{
    public abstract class XmlSerializable<TDataObject, TTypeMappings>:IXmlSerializable
        where TTypeMappings:ITypeMappings
        where TDataObject:class
    {
        private static Lazy<XmlDataContractSerializer<TTypeMappings>> SerializerFactory
        {
            get;
        }= new Lazy<XmlDataContractSerializer<TTypeMappings>>(  () =>
                                                                {
                                                                    return new XmlDataContractSerializer<TTypeMappings>();
                                                                },
                                                                LazyThreadSafetyMode.ExecutionAndPublication    );

        private TDataObject dataObject;

        protected XmlSerializable()
        {
        }

        protected XmlSerializable( TDataObject dataObject )
        :
            this()
        {
            this.DataObject = dataObject;
        }

        public TDataObject DataObject
        {
            get{ return this.dataObject; }

            protected internal set
            {
                value.ThrowIfNull();

                this.dataObject = value;
            }
        }

        public XmlDataContractSerializer<TTypeMappings> Serializer
        {
            get{ return XmlSerializable<TDataObject, TTypeMappings>.SerializerFactory.Value; }
        }

        public TTypeMappings TypeMappings
        {
            get{ return this.Serializer.TypeMappings; }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public abstract void ReadXml( XmlReader reader );
        public abstract void WriteXml( XmlWriter writer );

        public override String ToString()
        {
            String result = String.Empty;

            if( !( this.DataObject is null ) )
            {
                result = this.DataObject.ToString();
            }

            return result;
        }
    }
}