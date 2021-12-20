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

using Reth.Itss2.Messaging;

namespace Reth.Itss2.UnitTests.Messaging
{
    [TestClass]
    public class MessageFilterTests
    {
        [TestMethod]
        public void Intercept_FilterMessage_ReturnsTrue()
        {
            MessageId messageId = new MessageId( "4711" );

            MessageFilter messageFilter = new MessageFilter( messageId );

            Mock<IMessage> messageStub = new Mock<IMessage>();

            messageStub.Setup( message => message.Id ).Returns( messageId );

            bool intercepted = messageFilter.Intercept( messageStub.Object );

            Assert.IsTrue( intercepted );
        }

        [TestMethod]
        public void Intercept_NoFilterMessage_ReturnsFalse()
        {
            MessageId messageId = new MessageId( "4711" );

            MessageFilter messageFilter = new MessageFilter( messageId );

            Mock<IMessage> messageStub = new Mock<IMessage>();

            bool intercepted = messageFilter.Intercept( messageStub.Object );

            Assert.IsFalse( intercepted );
        }
    }
}
