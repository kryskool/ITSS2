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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.HelloDialog
{
    [TestClass]
    public class HelloResponseEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Response
        {
            get
            {
                (   SubscriberId Id,
                    SubscriberType Type,
                    String TenantId,
                    String Manufacturer,
                    String ProductInfo,
                    String VersionInfo  ) subscriber = (    SubscriberId.DefaultIMS,
                                                            SubscriberType.IMS,
                                                            "Polly's Pharmacy",
                                                            "4711",
                                                            "Digital Pharmacy",
                                                            "1.3"   );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <HelloResponse Id=""{ XmlMessageTests.MessageId }"">
                                        <Subscriber Id=""{ subscriber.Id }""
                                                    Type=""{ subscriber.Type }""
                                                    Manufacturer=""{ subscriber.Manufacturer }""
                                                    ProductInfo=""{ subscriber.ProductInfo }""
                                                    VersionInfo=""{ subscriber.VersionInfo }""
                                                    TenantId=""{ subscriber.TenantId }"">
                                            <Capability Name=""{ StandardDialogs.KeepAlive }""/>
                                            <Capability Name=""{ StandardDialogs.StockDeliveryInfo }"" />
                                        </Subscriber>
                                    </HelloResponse>
                                </WWKS>",
                            new MessageEnvelope(    new HelloResponse(  XmlMessageTests.MessageId,
                                                                        new Subscriber(   new Capability[]{   new Capability( StandardDialogs.KeepAlive ),
                                                                                                            new Capability( StandardDialogs.StockDeliveryInfo ) },
                                                                                        subscriber.Id,
                                                                                        subscriber.Type,
                                                                                        subscriber.TenantId,
                                                                                        subscriber.Manufacturer,
                                                                                        subscriber.ProductInfo,
                                                                                        subscriber.VersionInfo  )   ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( HelloResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( HelloResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}
