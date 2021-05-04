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
using System.Buffers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Tokenization;
using Reth.Itss2.Dialogs.Standard.Serialization.Tokenization;

using Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.StockLocationInfoDialog;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Tokenization
{
    [TestClass]
    public class XmlTokenReaderTests
    {
        [TestMethod]
        public void Read_SingleChunkMessage_Succeeds()
        {
            ITokenReader tokenReader = new XmlTokenReader();
            ITokenReaderState tokenReaderState = new XmlTokenReaderState();

            String expectedMessage = StockLocationInfoResponseEnvelopeDataContractTests.Response.Xml;

            ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>( XmlSerializationSettings.Encoding.GetBytes( expectedMessage ) );

            bool readResult = tokenReader.Read( ref tokenReaderState, ref buffer, out ReadOnlySequence<byte> token, out _ );

            Assert.IsTrue( readResult );

            String actualMessage = XmlSerializationSettings.Encoding.GetString( token.ToArray() );

            XmlComparer.AreEqual( expectedMessage, actualMessage );
        }

        [TestMethod]
        public void Read_MultipleChunkMessage_Succeeds()
        {
            ITokenReader tokenReader = new XmlTokenReader();
            ITokenReaderState tokenReaderState = new XmlTokenReaderState();

            String expectedMessage = StockLocationInfoResponseEnvelopeDataContractTests.Response.Xml;

            ( String Left, String Right ) messageBlocks = expectedMessage.Divide();

            ReadOnlySequence<byte> firstChunk = new ReadOnlySequence<byte>( XmlSerializationSettings.Encoding.GetBytes( messageBlocks.Left ) );
            ReadOnlySequence<byte> secondChunk = new ReadOnlySequence<byte>( XmlSerializationSettings.Encoding.GetBytes( messageBlocks.Left + messageBlocks.Right ) );

            bool firstReadResult = tokenReader.Read( ref tokenReaderState, ref firstChunk, out _, out _ );

            Assert.IsFalse( firstReadResult );

            bool secondReadResult = tokenReader.Read( ref tokenReaderState, ref secondChunk, out ReadOnlySequence<byte> token, out _ );

            Assert.IsTrue( secondReadResult );

            String actualMessage = XmlSerializationSettings.Encoding.GetString( token.ToArray() );

            XmlComparer.AreEqual( expectedMessage, actualMessage );
        }
    }
}
