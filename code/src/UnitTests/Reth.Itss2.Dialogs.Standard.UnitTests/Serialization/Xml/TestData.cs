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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfoDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.HelloDialog;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Xml
{
    internal static class TestData
    {
        public static readonly ( String Xml, IMessageEnvelope Object ) HelloRequest = ( @"  <WWKS Version=""2.0"" TimeStamp=""2020-09-16T19:28:00Z"">
                                                                                                <HelloRequest Id=""10"">
                                                                                                    <Subscriber Id=""100"" Type=""IMS"" Manufacturer=""Polly's Pharmacy"" ProductInfo=""Digital Pharmacy"" VersionInfo=""1.3"" TenantId=""4711"">
                                                                                                        <Capability Name=""KeepAlive""/>
                                                                                                        <Capability Name=""Status""/>
                                                                                                    </Subscriber>
                                                                                                </HelloRequest>
                                                                                            </WWKS>",
                                                                                        new MessageEnvelope(    new HelloRequest(   new MessageId( "10" ),
                                                                                                                                    new Subscriber( new Capability[]{   new Capability( "KeepAlive" ),
                                                                                                                                                                        new Capability( "Status" ) },
                                                                                                                                                    SubscriberId.DefaultIMS,
                                                                                                                                                    SubscriberType.IMS,
                                                                                                                                                    "Polly's Pharmacy",
                                                                                                                                                    "4711",
                                                                                                                                                    "Digital Pharmacy",
                                                                                                                                                    "1.3" ) ),
                                                                                                                MessageEnvelopeTimestamp.Parse( "2020-09-16T19:28:00Z" )    ) );

        public static readonly ( String Xml, IMessageEnvelope Object ) HelloResponse = (    @"  <WWKS Version=""2.0"" TimeStamp=""2020-09-16T19:28:00Z"">
                                                                                                    <HelloResponse Id=""10"">
                                                                                                        <Subscriber Id=""999"" Type=""Robot"" Manufacturer=""ACME"" ProductInfo=""ACME Storage System"" VersionInfo=""1.0"">
                                                                                                            <Capability Name=""KeepAlive""/>
                                                                                                            <Capability Name=""Status""/>
                                                                                                        </Subscriber>
                                                                                                    </HelloResponse>
                                                                                                </WWKS>",
                                                                                            new MessageEnvelope(    new HelloResponse(  new MessageId( "10" ),
                                                                                                                                        new Subscriber( new Capability[]{   new Capability( "KeepAlive" ),
                                                                                                                                                                            new Capability( "Status" ) },
                                                                                                                                                        SubscriberId.DefaultRobot,
                                                                                                                                                        SubscriberType.Robot,
                                                                                                                                                        String.Empty,
                                                                                                                                                        "ACME",
                                                                                                                                                        "ACME Storage System",
                                                                                                                                                        "1.0" ) ),
                                                                                                                    MessageEnvelopeTimestamp.Parse( "2020-09-16T19:28:00Z" )    )    );

        public static readonly (String Xml, IMessageEnvelope Object) ArticleInfoRequest = ( @"  <WWKS Version=""2.0"" TimeStamp=""2020-09-16T19:28:00Z"">
                                                                                                    <ArticleInfoRequest Id=""1100"" Source=""100"" Destination=""999"">
                                                                                                        <Article Id=""1234""/>
                                                                                                    </ArticleInfoRequest>
                                                                                                </WWKS>",
                                                                                            new MessageEnvelope( new ArticleInfoRequest(    new MessageId( "1100" ),
                                                                                                                                            SubscriberId.DefaultRobot,
                                                                                                                                            SubscriberId.DefaultIMS,
                                                                                                                                            new ArticleInfoRequestArticle[]
                                                                                                                                            {
                                                                                                                                                new ArticleInfoRequestArticle( new ArticleId( "1234" ) )
                                                                                                                                            }   ) ) );
    }
}
