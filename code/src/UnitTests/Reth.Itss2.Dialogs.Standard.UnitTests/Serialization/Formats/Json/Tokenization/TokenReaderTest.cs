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

using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Tokenization;
using Reth.Itss2.Dialogs.Standard.Serialization.Tokenization;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Json.Tokenization
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
            ITokenReaderState tokenReaderState = new JsonTokenReaderState();

            ITokenReader tokenReader = new JsonTokenReader();

            String expectedMessage = TestData.HelloRequest.Json;

            ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>( JsonSerializationSettings.Encoding.GetBytes( expectedMessage ) );

            bool readResult = tokenReader.Read( ref tokenReaderState, ref buffer, out ReadOnlySequence<byte> token, out _ );

            Assert.IsTrue( readResult );

            String actualMessage = JsonSerializationSettings.Encoding.GetString( token.ToArray() );

            Assert.IsTrue( JsonComparer.AreEqual( expectedMessage, actualMessage ) );
        }

        [TestMethod]
        public void TestReadWithMultipleBlocks()
        {
            ITokenReaderState tokenReaderState = new JsonTokenReaderState();

            ITokenReader tokenReader = new JsonTokenReader();

            String expectedMessage = TestData.HelloRequest.Json;

            (String left, String right) messageBlocks = expectedMessage.Divide();

            ReadOnlySequence<byte> firstBuffer = new ReadOnlySequence<byte>( JsonSerializationSettings.Encoding.GetBytes( messageBlocks.left ) );
            ReadOnlySequence<byte> secondBuffer = new ReadOnlySequence<byte>( JsonSerializationSettings.Encoding.GetBytes( messageBlocks.left + messageBlocks.right ) );

            bool firstReadResult = tokenReader.Read( ref tokenReaderState, ref firstBuffer, out _, out _ );

            Assert.IsFalse( firstReadResult );

            bool secondReadResult = tokenReader.Read( ref tokenReaderState, ref secondBuffer, out ReadOnlySequence<byte> token, out _ );

            Assert.IsTrue( secondReadResult );

            String actualMessage = JsonSerializationSettings.Encoding.GetString( token.ToArray() );

            Assert.IsTrue( JsonComparer.AreEqual( expectedMessage, actualMessage ) );
        }
    }
}
