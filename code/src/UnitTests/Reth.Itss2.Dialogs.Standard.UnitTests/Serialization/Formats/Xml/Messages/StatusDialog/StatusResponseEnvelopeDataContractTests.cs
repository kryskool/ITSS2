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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Status;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.StatusDialog
{
    [TestClass]
    public class StatusResponseEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Response
        {
            get
            {
                ( ComponentType Type, ComponentState State, String Description, String StateText ) storageSystem = ( ComponentType.StorageSystem, ComponentState.Ready, "Vmax1", "Door is open." );
                ( ComponentType Type, ComponentState State, String Description, String StateText ) boxSystem = ( ComponentType.BoxSystem, ComponentState.NotReady, "Box System", "Switched off." );

                ComponentState overallState = ComponentState.NotReady;
                String overallStateText = "Out of order.";

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <StatusResponse Id=""{ XmlMessageTests.MessageId }""
                                                    Source=""{ XmlMessageTests.Source }""
                                                    Destination=""{ XmlMessageTests.Destination }""
                                                    State=""{ overallState }""
                                                    StateText=""{ overallStateText }"">
                                        <Component  Type=""{ storageSystem.Type }""
                                                    State=""{ storageSystem.State }""
                                                    Description=""{ storageSystem.Description }""
                                                    StateText=""{ storageSystem.StateText }"" />
                                        <Component  Type=""{ boxSystem.Type }""
                                                    State=""{ boxSystem.State }""
                                                    Description=""{ boxSystem.Description }""
                                                    StateText=""{ boxSystem.StateText }"" />
                                    </StatusResponse>
                                </WWKS>",
                            new MessageEnvelope(    new StatusResponse(     XmlMessageTests.MessageId,
                                                                            XmlMessageTests.Source,
                                                                            XmlMessageTests.Destination,
                                                                            overallState,
                                                                            overallStateText,
                                                                            new Component[]
                                                                            {
                                                                                new Component(  storageSystem.Type,
                                                                                                storageSystem.State,
                                                                                                storageSystem.Description,
                                                                                                storageSystem.StateText ),
                                                                                new Component(  boxSystem.Type,
                                                                                                boxSystem.State,
                                                                                                boxSystem.Description,
                                                                                                boxSystem.StateText )
                                                                            } ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( StatusResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( StatusResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}
