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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutput;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.TaskCancelOutputDialog
{
    [TestClass]
    public class TaskCancelOutputRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                MessageId taskId = new MessageId( "4711" );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <TaskCancelOutputRequest Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"">
                                        <Task Id=""{ taskId }"" />
                                    </TaskCancelOutputRequest>
                                </WWKS>",
                            new MessageEnvelope(    new TaskCancelOutputRequest(    XmlMessageTests.MessageId,
                                                                                    XmlMessageTests.Source,
                                                                                    XmlMessageTests.Destination,
                                                                                    new TaskCancelOutputRequestTask( taskId ) ),
                                                    XmlMessageTests.Timestamp    )  );
            }
        }   

        public static ( String Xml, IMessageEnvelope Object ) Response
        {
            get
            {
                ( MessageId, TaskCancelOutputStatus ) taskCancelError = ( new MessageId( "4711" ), TaskCancelOutputStatus.CancelError );
                ( MessageId, TaskCancelOutputStatus ) taskCancelled = ( new MessageId( "4712" ), TaskCancelOutputStatus.Cancelled );
                ( MessageId, TaskCancelOutputStatus ) taskUnknown = ( new MessageId( "4713" ), TaskCancelOutputStatus.Unknown );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <TaskCancelOutputResponse Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"">
                                        <Task Id=""{ taskCancelError.Item1 }"" Status=""{ taskCancelError.Item2 }"" />
                                        <Task Id=""{ taskCancelled.Item1 }"" Status=""{ taskCancelled.Item2 }"" />
                                        <Task Id=""{ taskUnknown.Item1 }"" Status=""{ taskUnknown.Item2 }"" />
                                    </TaskCancelOutputResponse>
                                </WWKS>",
                            new MessageEnvelope(    new TaskCancelOutputResponse(   XmlMessageTests.MessageId,
                                                                                    XmlMessageTests.Source,
                                                                                    XmlMessageTests.Destination,
                                                                                    new TaskCancelOutputResponseTask[]
                                                                                    {
                                                                                        new TaskCancelOutputResponseTask( taskCancelError.Item1, taskCancelError.Item2 ),
                                                                                        new TaskCancelOutputResponseTask( taskCancelled.Item1, taskCancelled.Item2 ),
                                                                                        new TaskCancelOutputResponseTask( taskUnknown.Item1, taskUnknown.Item2 ),
                                                                                    }   ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( TaskCancelOutputRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( TaskCancelOutputRequestEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( TaskCancelOutputRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( TaskCancelOutputRequestEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}
