using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.General.Hello
{
    internal class SubscriberDataContract<TTypeMappings>:XmlSerializable<Subscriber, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public SubscriberDataContract()
        {
        }

        public SubscriberDataContract( Subscriber dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {
            SubscriberId id = base.Serializer.ReadMandatoryAttribute<SubscriberId>( reader, nameof( this.DataObject.Id ) );
            SubscriberType type = base.Serializer.ReadMandatoryEnum<SubscriberType>( reader, nameof( this.DataObject.Type ) );
            String manufacturer = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.Manufacturer ) );
            String productInfo = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.ProductInfo ) );
            String versionInfo = base.Serializer.ReadMandatoryString( reader, nameof( this.DataObject.VersionInfo ) );
            String tenantId = base.Serializer.ReadOptionalString( reader, nameof( this.DataObject.TenantId ) );
            
            IEnumerable<Capability> capabilities = base.Serializer.ReadOptionalElements<Capability>( reader, nameof( Capability ) );

            HashSet<IDialogName> dialogs = new HashSet<IDialogName>( capabilities.Select(   ( Capability capability ) =>
                                                                                            {
                                                                                                IDialogName result = null;

                                                                                                if( !( capability is null ) )
                                                                                                {
                                                                                                    result = capability.Name;
                                                                                                }

                                                                                                return result;
                                                                                            }   ) );

            if( dialogs.Contains( DialogName.Hello ) == false )
            {
                dialogs.Add( DialogName.Hello );
            }

            if( dialogs.Contains( DialogName.Unprocessed ) == false )
            {
                dialogs.Add( DialogName.Unprocessed );
            }

            this.DataObject = new Subscriber(   id,
                                                type,
                                                manufacturer,
                                                productInfo,
                                                versionInfo,
                                                tenantId,
                                                dialogs    );
        }

        public override void WriteXml( XmlWriter writer )
        {
            Subscriber dataObject = this.DataObject;

            base.Serializer.WriteMandatoryAttribute<SubscriberId>( writer, nameof( dataObject.Id ), dataObject.Id );
            base.Serializer.WriteMandatoryEnum<SubscriberType>( writer, nameof( dataObject.Type ), dataObject.Type );
            base.Serializer.WriteMandatoryString( writer, nameof( dataObject.Manufacturer ), dataObject.Manufacturer );
            base.Serializer.WriteMandatoryString( writer, nameof( dataObject.ProductInfo ), dataObject.ProductInfo );
            base.Serializer.WriteMandatoryString( writer, nameof( dataObject.VersionInfo ), dataObject.VersionInfo );
            base.Serializer.WriteOptionalString(  writer, nameof( dataObject.TenantId ), dataObject.TenantId );

            Capability[] capabilities = dataObject.GetCapabilities();

            IEnumerable<Capability> elements = capabilities.Where(  ( Capability capability ) =>
                                                                    {
                                                                        bool result = false;

                                                                        if( !( capability is null ) )
                                                                        {
                                                                            IDialogName dialogName = capability.Name;

                                                                            if( DialogName.Hello.Equals( dialogName ) == false &&
                                                                                DialogName.Unprocessed.Equals( dialogName ) == false  )
                                                                            {
                                                                                result = true;
                                                                            }
                                                                        }

                                                                        return result;
                                                                    }   );

            base.Serializer.WriteOptionalElements<Capability>( writer, nameof( Capability ), elements );
        }
    }
}