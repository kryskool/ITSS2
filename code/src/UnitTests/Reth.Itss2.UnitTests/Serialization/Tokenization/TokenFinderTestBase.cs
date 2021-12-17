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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Reth.Itss2.Serialization.Tokenization;

namespace Reth.Itss2.UnitTests.Serialization.Tokenization
{
    [TestClass]
    public abstract class TokenFinderTestBase<TState>:TokenizationTestBase
        where TState:Enum
    {
        protected abstract ITokenTransition<TState>? CreateTransition(  IEnumerable<ITokenTransition<TState>> transitions,
                                                                        ITokenPatternMatch nextMatch    );

        protected abstract ITokenPatternMatch? FindNextMatch(   TState currentState,
                                                                ref SequenceReader<byte> sequenceReader );

        protected Mock<ITokenTransition<TState>> CreateTransitionStub(  ITokenPattern tokenPattern,
                                                                        TState expectedFromState,
                                                                        TState expectedToState  )
        {
            Mock<ITokenPatternMatch> matchStub = new Mock<ITokenPatternMatch>();

            matchStub.Setup( x => x.Pattern ).Returns( tokenPattern );

            Mock<ITokenTransition<TState>> result = new Mock<ITokenTransition<TState>>();

            result.Setup( x => x.From ).Returns( expectedFromState );
            result.Setup( x => x.To ).Returns( expectedToState );
            result.Setup( x => x.Match ).Returns( matchStub.Object );

            return result;
        }

        protected void CreateTransition_WithGivenContext_Succeeds(  ITokenPattern tokenPattern,
                                                                    ITokenTransition<TState>? previousTransition,
                                                                    TState expectedFromState,
                                                                    TState expectedToState  )
        {
            Mock<ITokenPatternMatch> nextMatchStub = new Mock<ITokenPatternMatch>();

            nextMatchStub.Setup( x => x.Pattern ).Returns( tokenPattern );

            List<ITokenTransition<TState>> transitions = new List<ITokenTransition<TState>>{};

            if( previousTransition is not null )
            {
                transitions.Add( previousTransition );
            }

            ITokenTransition<TState>? actualResult = this.CreateTransition( transitions, nextMatchStub.Object );

            Assert.IsNotNull( actualResult );

            Assert.AreEqual( expectedFromState, actualResult.From );
            Assert.AreEqual( expectedToState, actualResult.To );
            Assert.AreSame( nextMatchStub.Object, actualResult.Match );
        }

        protected void FindNextMatch_WithGivenContext_Succeeds( TState currentState,
                                                                String content,
                                                                ITokenPattern expectedPattern   )
        {
            this.GetSequenceReader( content, out SequenceReader<byte> sequenceReader );

            ITokenPatternMatch? actualMatch = this.FindNextMatch( currentState, ref sequenceReader );

            Assert.IsNotNull( actualMatch );
            Assert.AreEqual( expectedPattern, actualMatch.Pattern );
        }
    }
}
