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

using Reth.Itss2.Dialogs.Standard.Serialization.Tokenization;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Tokenization
{
    [TestClass]
    public class TokenStateTransitionTests
    {
        [TestMethod]
        public void IsMessageBoundary_WithMessageTransitions_ReturnsTrue()
        {
            Mock<ITokenStateTransition> firstStub = new Mock<ITokenStateTransition>();
            Mock<ITokenStateTransition> secondStub = new Mock<ITokenStateTransition>();

            firstStub.Setup( transition => transition.IsMessageBegin() ).Returns( true );
            secondStub.Setup( transition => transition.IsMessageEnd() ).Returns( true );

            bool isMessageBoundary = TokenStateTransition.IsMessageBoundary( firstStub.Object, secondStub.Object );

            Assert.IsTrue( isMessageBoundary );
        }

        [DataRow( true, false )]
        [DataRow( false, true )]
        [DataRow( false, false )]
        [DataTestMethod()]
        public void IsMessageBoundary_NoMessageTransitions_ReturnsFalse( bool firstTransition, bool secondTransition )
        {
            Mock<ITokenStateTransition> firstStub = new Mock<ITokenStateTransition>();
            Mock<ITokenStateTransition> secondStub = new Mock<ITokenStateTransition>();

            firstStub.Setup( transition => transition.IsMessageBegin() ).Returns( firstTransition );
            secondStub.Setup( transition => transition.IsMessageEnd() ).Returns( secondTransition );

            bool isMessageBoundary = TokenStateTransition.IsMessageBoundary( firstStub.Object, secondStub.Object );

            Assert.IsFalse( isMessageBoundary );
        }
    }
}
