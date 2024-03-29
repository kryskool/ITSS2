﻿// Implementation of the WWKS2 protocol.
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Reth.Itss2.Tokenization;

namespace Reth.Itss2.UnitTests.Tokenization
{
    [TestClass]
    public abstract class TokenTransitionTestBase<TState>:TokenizationTestBase
        where TState:Enum
    {
        [TestMethod]
        public void EmbracesMessages_WithEmbracingTransitions_ReturnsTrue()
        {
            Mock<ITokenTransition<TState>> firstStub = new Mock<ITokenTransition<TState>>();
            Mock<ITokenTransition<TState>> secondStub = new Mock<ITokenTransition<TState>>();

            firstStub.Setup( transition => transition.IsMessageBegin() ).Returns( true );
            secondStub.Setup( transition => transition.IsMessageEnd() ).Returns( true );

            bool embracesMessage = TokenTransition<TState>.EmbracesMessage( firstStub.Object, secondStub.Object );

            Assert.IsTrue( embracesMessage );
        }

        [DataRow( true, false )]
        [DataRow( false, true )]
        [DataRow( false, false )]
        [DataTestMethod()]
        public void IsMessageBoundary_WithoutEmbracingTransitions_ReturnsFalse( bool firstTransition, bool secondTransition )
        {
            Mock<ITokenTransition<TState>> firstStub = new Mock<ITokenTransition<TState>>();
            Mock<ITokenTransition<TState>> secondStub = new Mock<ITokenTransition<TState>>();

            firstStub.Setup( transition => transition.IsMessageBegin() ).Returns( firstTransition );
            secondStub.Setup( transition => transition.IsMessageEnd() ).Returns( secondTransition );

            bool isMessageBoundary = TokenTransition<TState>.EmbracesMessage( firstStub.Object, secondStub.Object );

            Assert.IsFalse( isMessageBoundary );
        }
    }
}
