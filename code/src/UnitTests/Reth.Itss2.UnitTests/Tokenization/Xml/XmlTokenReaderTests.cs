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
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Reth.Itss2.Tokenization;
using Reth.Itss2.Tokenization.Xml;

namespace Reth.Itss2.UnitTests.Tokenization.Xml
{
    [TestClass]
    public class XmlTokenReaderTests:TokenizationTestBase
    {
        [TestMethod]
        public void Read_CompleteMessage_ReturnsTrue()
        {
            String expectedMessage = TestData.HelloRequest;
            
            using( MemoryStream stream = this.GetStream( expectedMessage ) )
            {
                using( XmlTokenReader tokenReader = new XmlTokenReader( stream, this.Encoding ) )
                {
                    bool actualResult = tokenReader.Read( out ReadOnlySequence<byte> token );

                    Assert.IsTrue( actualResult );

                    String? actualMessage = token.ToString( this.Encoding );

                    XmlComparer.AreEqual( expectedMessage, actualMessage );
                }
            }
        }
    }
}
