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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Reth.Itss2.Tokenization;
using Reth.Itss2.Tokenization.Xml;

namespace Reth.Itss2.UnitTests.Tokenization
{
    [TestClass]
    public class ExtensionMethodsTests:TestBase
    {
        public ExtensionMethodsTests()
        {
            this.TokenPatterns = new XmlTokenPatterns( this.Encoding );
        }

        private Encoding Encoding
        {
            get;
        } = Encoding.UTF8;

        private XmlTokenPatterns TokenPatterns
        {
            get;
        }

        [TestMethod]
        public void TryGetMessage_WithMessageTransitions_ReturnsTrue()
        {
            List<XmlTokenTransition> transitions = new List<XmlTokenTransition>();

            transitions.Add( new XmlTokenTransition(    XmlTokenState.OutOfMessage,
                                                        XmlTokenState.WithinMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.BeginOfMessage, 0 ) ) );

            transitions.Add( new XmlTokenTransition(    XmlTokenState.WithinMessage,
                                                        XmlTokenState.WithinData,
                                                        new TokenPatternMatch( this.TokenPatterns.BeginOfData, transitions.Last().Match.EndIndex ) ) );

            transitions.Add( new XmlTokenTransition(    XmlTokenState.WithinData,
                                                        XmlTokenState.WithinMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.EndOfData, transitions.Last().Match.EndIndex ) ) );

            transitions.Add( new XmlTokenTransition(    XmlTokenState.WithinMessage,
                                                        XmlTokenState.OutOfMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.EndOfMessage, transitions.Last().Match.EndIndex ) ) );

            List<byte> bufferContent = new List<byte>();

            bufferContent.AddRange( this.TokenPatterns.BeginOfMessage.Value );
            bufferContent.AddRange( this.TokenPatterns.BeginOfData.Value );
            bufferContent.AddRange( this.TokenPatterns.EndOfData.Value );
            bufferContent.AddRange( this.TokenPatterns.EndOfMessage.Value );

            ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>( bufferContent.ToArray() );

            SequenceReader<byte> bufferReader = new SequenceReader<byte>( buffer );

            bool actualResult = transitions.TryGetMessage(  ref bufferReader,
                                                            out ReadOnlySequence<byte> token,
                                                            out ITokenTransition<XmlTokenState>? firstTransition,
                                                            out ITokenTransition<XmlTokenState>? lastTransition   );

            Assert.IsTrue( actualResult );

            SequenceReader<byte> tokenReader = new SequenceReader<byte>( token );

            Assert.IsTrue( tokenReader.IsNext( this.TokenPatterns.BeginOfMessage.Value.AsSpan(), advancePast:true ) );
            Assert.IsTrue( tokenReader.IsNext( this.TokenPatterns.BeginOfData.Value.AsSpan(), advancePast:true ) );
            Assert.IsTrue( tokenReader.IsNext( this.TokenPatterns.EndOfData.Value.AsSpan(), advancePast:true ) );
            Assert.IsTrue( tokenReader.IsNext( this.TokenPatterns.EndOfMessage.Value.AsSpan(), advancePast:true ) );
        }

        [TestMethod]
        public void TryGetMessage_WithMessage_ReturnsTrue()
        {
            String messageContent = "abcd";

            List<XmlTokenTransition> transitions = new List<XmlTokenTransition>();

            transitions.Add( new XmlTokenTransition(    XmlTokenState.OutOfMessage,
                                                        XmlTokenState.WithinMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.BeginOfMessage, 0 ) ) );

            transitions.Add( new XmlTokenTransition(    XmlTokenState.WithinMessage,
                                                        XmlTokenState.OutOfMessage,
                                                        new TokenPatternMatch( this.TokenPatterns.EndOfMessage, transitions.Last().Match.EndIndex + messageContent.Length ) ) );

            List<byte> bufferContent = new List<byte>();

            bufferContent.AddRange( this.TokenPatterns.BeginOfMessage.Value );
            bufferContent.AddRange( this.Encoding.GetBytes( messageContent ) );
            bufferContent.AddRange( this.TokenPatterns.EndOfMessage.Value );

            ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>( bufferContent.ToArray() );

            String expectedMessage = this.Encoding.GetString( buffer );

            SequenceReader<byte> sequenceReader = new SequenceReader<byte>( buffer );

            bool actualResult = transitions.TryGetMessage(  ref sequenceReader,
                                                            out ReadOnlySequence<byte> token,
                                                            out ITokenTransition<XmlTokenState>? firstTransition,
                                                            out ITokenTransition<XmlTokenState>? lastTransition   );

            Assert.IsTrue( actualResult );

            String? actualMessage = token.ToString( this.Encoding );

            Assert.AreEqual( expectedMessage, actualMessage );
        }
    }
}
