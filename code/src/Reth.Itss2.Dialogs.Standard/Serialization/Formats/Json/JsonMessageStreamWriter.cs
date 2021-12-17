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

using System.IO;
using System.Text;

using Reth.Itss2.Diagnostics;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json
{
    public class JsonMessageStreamWriter:MessageStreamWriter
    {
        public JsonMessageStreamWriter( Stream baseStream )
        :
            this( baseStream, NullInteractionLog.Instance )
        {
        }

        public JsonMessageStreamWriter( Stream baseStream, IInteractionLog interactionLog )
        :
            base( baseStream, NullInteractionLog.Instance )
        {
            this.MessageParser = new JsonMessageParser( typeof( JsonDataContractResolver ) );
        }

        public JsonMessageStreamWriter( Stream baseStream, ISerializationProvider serializationProvider )
        :
            this( baseStream, serializationProvider, NullInteractionLog.Instance )
        {
        }

        public JsonMessageStreamWriter( Stream baseStream, ISerializationProvider serializationProvider, IInteractionLog interactionLog )
        :
            base( baseStream, interactionLog )
        {
            this.MessageParser = new JsonMessageParser( serializationProvider );
        }

        protected override Encoding Encoding
        {
            get;
        } = JsonSerializationSettings.Encoding;

        protected override IMessageParser MessageParser
        {
            get;
        }
    }
}
