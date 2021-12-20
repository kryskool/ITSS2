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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutput;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Json.Messages.TaskCancelOutputDialog
{
    [TestClass]
    public class TaskCancelOutputResponseEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( String Json, IMessageEnvelope Object ) Response
        {
            get
            {
                ( MessageId Id, TaskCancelOutputStatus Status ) taskCancelError = ( new MessageId( "4711" ), TaskCancelOutputStatus.CancelError );
                ( MessageId Id, TaskCancelOutputStatus Status ) taskCancelled = ( new MessageId( "4712" ), TaskCancelOutputStatus.Cancelled );
                ( MessageId Id, TaskCancelOutputStatus Status ) taskUnknown = ( new MessageId( "4713" ), TaskCancelOutputStatus.Unknown );

                return (    $@" {{
                                    ""TaskCancelOutputResponse"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""Task"":
                                        [
                                            {{
                                                ""Id"": ""{ taskCancelError.Id }"",
                                                ""Status"": ""{ taskCancelError.Status }""
                                            }},
                                            {{
                                                ""Id"": ""{ taskCancelled.Id }"",
                                                ""Status"": ""{ taskCancelled.Status }""
                                            }},
                                            {{
                                                ""Id"": ""{ taskUnknown.Id }"",
                                                ""Status"": ""{ taskUnknown.Status }""
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope(    new TaskCancelOutputResponse(   JsonMessageTests.MessageId,
                                                                                    JsonMessageTests.Source,
                                                                                    JsonMessageTests.Destination,
                                                                                    new TaskCancelOutputResponseTask[]
                                                                                    {
                                                                                        new TaskCancelOutputResponseTask( taskCancelError.Id, taskCancelError.Status ),
                                                                                        new TaskCancelOutputResponseTask( taskCancelled.Id, taskCancelled.Status ),
                                                                                        new TaskCancelOutputResponseTask( taskUnknown.Id, taskUnknown.Status ),
                                                                                    }   ),
                                                    JsonMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( TaskCancelOutputResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( TaskCancelOutputResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}
