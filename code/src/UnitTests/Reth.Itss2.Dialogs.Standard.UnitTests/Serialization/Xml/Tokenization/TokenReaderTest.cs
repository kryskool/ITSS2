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

using Reth.Itss2.Dialogs.Standard.Serialization.Xml;
using Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Xml.Tokenization
{
    [TestClass]
    public class TokenReaderTest
    {
        [TestMethod]
        public void TestRead()
        {
            TokenReader tokenReader = new TokenReader();

            String expectedMessage = TestData.HelloRequest.Xml;

            ( String left, String right ) messageBlocks = expectedMessage.Divide();

            ReadOnlySequence<byte> firstBuffer = new ReadOnlySequence<byte>( XmlSerializationSettings.Encoding.GetBytes( messageBlocks.left ) );
            ReadOnlySequence<byte> secondBuffer = new ReadOnlySequence<byte>( XmlSerializationSettings.Encoding.GetBytes( messageBlocks.left + messageBlocks.right ) );

            tokenReader = tokenReader.Read( ref firstBuffer, out _ );
            tokenReader.Read( ref secondBuffer, out ReadOnlySequence<byte>[] tokens );

            Assert.IsTrue( tokens.Length == 1 );

            String actualMessage = XmlSerializationSettings.Encoding.GetString( tokens[ 0 ].ToArray() );

            XmlComparer.AreEqual( expectedMessage, actualMessage );
        }

        [TestMethod]
        public void TestPartialMessage()
        {
            TokenReader tokenReader = new TokenReader();

            ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>( XmlSerializationSettings.Encoding.GetBytes( @"<WWKS Version=" ) );

            tokenReader = tokenReader.Read( ref buffer, out _ );

            TokenState expectedTokenState = TokenState.WithinMessage;
            TokenState actualTokenState = tokenReader.Sequence.State;

            Assert.AreEqual( expectedTokenState, actualTokenState );
        }

        [TestMethod]
        public void TestMessageTransitionWithPartialMessageAtEnd()
        {
            TokenReader tokenReader = new TokenReader();

            String expectedMessage = TestData.HelloRequest.Xml;

            ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>( XmlSerializationSettings.Encoding.GetBytes( TestData.HelloRequest.Xml + "<W" ) );

            tokenReader = tokenReader.Read( ref buffer, out ReadOnlySequence<byte>[] tokens );

            TokenState expectedTokenState = TokenState.OutOfMessage;
            TokenState actualTokenState = tokenReader.Sequence.State;

            Assert.AreEqual( expectedTokenState, actualTokenState );

            String actualMessage = XmlSerializationSettings.Encoding.GetString( tokens[ 0 ].ToArray() );

            XmlComparer.AreEqual( expectedMessage, actualMessage );
        }
    }
}
