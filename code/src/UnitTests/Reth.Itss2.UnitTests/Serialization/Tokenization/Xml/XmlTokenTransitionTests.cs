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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Reth.Itss2.Serialization.Tokenization;
using Reth.Itss2.Serialization.Tokenization.Xml;

namespace Reth.Itss2.UnitTests.Serialization.Tokenization.Xml
{
    [TestClass]
    public class XmlTokenTransitionTests:TokenTransitionTestBase<XmlTokenState>
    {
        [DataRow( XmlTokenState.OutOfMessage, XmlTokenState.WithinMessage, true )]
        [DataRow( XmlTokenState.OutOfMessage, XmlTokenState.WithinData, false )]
        [DataRow( XmlTokenState.OutOfMessage, XmlTokenState.OutOfMessage, false )]

        [DataRow( XmlTokenState.WithinMessage, XmlTokenState.OutOfMessage, false )]
        [DataRow( XmlTokenState.WithinMessage, XmlTokenState.WithinData, false )]
        [DataRow( XmlTokenState.WithinMessage, XmlTokenState.WithinMessage, false )]

        [DataRow( XmlTokenState.WithinData, XmlTokenState.OutOfMessage, false )]
        [DataRow( XmlTokenState.WithinData, XmlTokenState.WithinMessage, false )]
        [DataRow( XmlTokenState.WithinData, XmlTokenState.WithinData, false )]
        [DataTestMethod()]
        public void IsMessageBegin_WithProvidedStates_ReturnsExpectedResult( XmlTokenState from, XmlTokenState to, bool expectedResult )
        {
            Mock<ITokenPatternMatch> matchStub = new Mock<ITokenPatternMatch>();

            XmlTokenTransition transition = new XmlTokenTransition( from, to, matchStub.Object );
            
            bool actualResult = transition.IsMessageBegin();

            Assert.AreEqual( expectedResult, actualResult );
        }

        [DataRow( XmlTokenState.OutOfMessage, XmlTokenState.WithinMessage, false )]
        [DataRow( XmlTokenState.OutOfMessage, XmlTokenState.WithinData, false )]
        [DataRow( XmlTokenState.OutOfMessage, XmlTokenState.OutOfMessage, false )]

        [DataRow( XmlTokenState.WithinMessage, XmlTokenState.OutOfMessage, true )]
        [DataRow( XmlTokenState.WithinMessage, XmlTokenState.WithinData, false )]
        [DataRow( XmlTokenState.WithinMessage, XmlTokenState.WithinMessage, false )]

        [DataRow( XmlTokenState.WithinData, XmlTokenState.OutOfMessage, false )]
        [DataRow( XmlTokenState.WithinData, XmlTokenState.WithinMessage, false )]
        [DataRow( XmlTokenState.WithinData, XmlTokenState.WithinData, false )]
        [DataTestMethod()]
        public void IsMessageEnd_WithProvidedStates_ReturnsExpectedResult( XmlTokenState from, XmlTokenState to, bool expectedResult )
        {
            Mock<ITokenPatternMatch> matchStub = new Mock<ITokenPatternMatch>();

            XmlTokenTransition transition = new XmlTokenTransition( from, to, matchStub.Object );
            
            bool actualResult = transition.IsMessageEnd();

            Assert.AreEqual( expectedResult, actualResult );
        }
    }
}
