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
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization.Formats.Json;
using Reth.Itss2.UnitTests;
using Reth.Itss2.UnitTests.Tokenization.Json;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Json.Messages
{
    [TestClass]
    public abstract class JsonMessageTests:TestBase
    {
        protected static readonly MessageEnvelopeTimestamp Timestamp = MessageEnvelopeTimestamp.Parse( "2021-05-02T20:58:03Z" );
        protected static readonly MessageId MessageId = new( "10" );
        protected static readonly SubscriberId Source = SubscriberId.DefaultIMS;
        protected static readonly SubscriberId Destination = SubscriberId.DefaultRobot;

        protected bool SerializeMessage( ( String Json, IMessageEnvelope Object ) message )
        {
            JsonMessageParser parser = new JsonMessageParser( typeof( JsonSerializationProvider ) );

            String actualJson = parser.SerializeMessageEnvelope( message.Object );

            return JsonComparer.AreEqual( message.Json, actualJson );
        }

        protected bool DeserializeMessage( ( String Json, IMessageEnvelope Object ) message )
        {
            JsonMessageParser parser = new JsonMessageParser( typeof( JsonSerializationProvider ) );

            IMessageEnvelope actualObject = parser.DeserializeMessageEnvelope( message.Json );

            return message.Object.Equals( actualObject );
        }
    }
}
