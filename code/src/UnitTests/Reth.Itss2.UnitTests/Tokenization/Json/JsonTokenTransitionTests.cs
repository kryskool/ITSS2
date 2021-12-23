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

using Reth.Itss2.Tokenization;
using Reth.Itss2.Tokenization.Json;

namespace Reth.Itss2.UnitTests.Tokenization.Json
{
    [TestClass]
    public class JsonTokenTransitionTests:TokenTransitionTestBase<JsonTokenState>
    {
        [DataRow( JsonTokenState.OutOfMessage, JsonTokenState.WithinObject, true )]
        [DataRow( JsonTokenState.OutOfMessage, JsonTokenState.WithinString, false )]
        [DataRow( JsonTokenState.OutOfMessage, JsonTokenState.OutOfMessage, false )]

        [DataRow( JsonTokenState.WithinObject, JsonTokenState.OutOfMessage, false )]
        [DataRow( JsonTokenState.WithinObject, JsonTokenState.WithinString, false )]
        [DataRow( JsonTokenState.WithinObject, JsonTokenState.WithinObject, false )]

        [DataRow( JsonTokenState.WithinString, JsonTokenState.OutOfMessage, false )]
        [DataRow( JsonTokenState.WithinString, JsonTokenState.WithinObject, false )]
        [DataRow( JsonTokenState.WithinString, JsonTokenState.WithinString, false )]
        [DataTestMethod()]
        public void IsMessageBegin_WithProvidedStates_ReturnsExpectedResult( JsonTokenState from, JsonTokenState to, bool expectedResult )
        {
            Mock<ITokenPatternMatch> matchStub = new Mock<ITokenPatternMatch>();

            JsonTokenTransition transition = new JsonTokenTransition( from, to, matchStub.Object );
            
            bool actualResult = transition.IsMessageBegin();

            Assert.AreEqual( expectedResult, actualResult );
        }

        [DataRow( JsonTokenState.OutOfMessage, JsonTokenState.WithinObject, false )]
        [DataRow( JsonTokenState.OutOfMessage, JsonTokenState.WithinString, false )]
        [DataRow( JsonTokenState.OutOfMessage, JsonTokenState.OutOfMessage, false )]

        [DataRow( JsonTokenState.WithinObject, JsonTokenState.OutOfMessage, true )]
        [DataRow( JsonTokenState.WithinObject, JsonTokenState.WithinString, false )]
        [DataRow( JsonTokenState.WithinObject, JsonTokenState.WithinObject, false )]

        [DataRow( JsonTokenState.WithinString, JsonTokenState.OutOfMessage, false )]
        [DataRow( JsonTokenState.WithinString, JsonTokenState.WithinObject, false )]
        [DataRow( JsonTokenState.WithinString, JsonTokenState.WithinString, false )]
        [DataTestMethod()]
        public void IsMessageEnd_WithProvidedStates_ReturnsExpectedResult( JsonTokenState from, JsonTokenState to, bool expectedResult )
        {
            Mock<ITokenPatternMatch> matchStub = new Mock<ITokenPatternMatch>();

            JsonTokenTransition transition = new JsonTokenTransition( from, to, matchStub.Object );
            
            bool actualResult = transition.IsMessageEnd();

            Assert.AreEqual( expectedResult, actualResult );
        }
    }
}
