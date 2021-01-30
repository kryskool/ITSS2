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
using Reth.Itss2.Dialogs.Standard.Serialization.Tokenization;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Tokenization;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Tokenization
{
    [TestClass]
    public class TokenReaderTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Diagnostics.ExecutionLogProvider.Log = new Diagnostics.DebugExecutionLog();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Diagnostics.ExecutionLogProvider.Log.Dispose();
        }

        [TestMethod]
        public void TestReadWithSingleBlock()
        {
            ITokenReaderState tokenReaderState = new XmlTokenReaderState();

            ITokenReader tokenReader = new XmlTokenReader();

            String expectedMessage = TestData.HelloRequest.Xml;

            ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>( XmlSerializationSettings.Encoding.GetBytes( expectedMessage ) );

            bool readResult = tokenReader.Read( ref tokenReaderState, ref buffer, out ReadOnlySequence<byte> token, out _ );

            Assert.IsTrue( readResult );

            String actualMessage = XmlSerializationSettings.Encoding.GetString( token.ToArray() );

            XmlComparer.AreEqual( expectedMessage, actualMessage );
        }

        [TestMethod]
        public void TestReadWithMultipleBlocks()
        {
            ITokenReaderState tokenReaderState = new XmlTokenReaderState();

            ITokenReader tokenReader = new XmlTokenReader();

            String expectedMessage = TestData.HelloRequest.Xml;

            (String left, String right) messageBlocks = expectedMessage.Divide();

            ReadOnlySequence<byte> firstBuffer = new ReadOnlySequence<byte>( XmlSerializationSettings.Encoding.GetBytes( messageBlocks.left ) );
            ReadOnlySequence<byte> secondBuffer = new ReadOnlySequence<byte>( XmlSerializationSettings.Encoding.GetBytes( messageBlocks.left + messageBlocks.right ) );

            bool firstReadResult = tokenReader.Read( ref tokenReaderState, ref firstBuffer, out _, out _ );

            Assert.IsFalse( firstReadResult );

            bool secondReadResult = tokenReader.Read( ref tokenReaderState, ref secondBuffer, out ReadOnlySequence<byte> token, out _ );

            Assert.IsTrue( secondReadResult );

            String actualMessage = XmlSerializationSettings.Encoding.GetString( token.ToArray() );

            XmlComparer.AreEqual( expectedMessage, actualMessage );
        }
    }
}
