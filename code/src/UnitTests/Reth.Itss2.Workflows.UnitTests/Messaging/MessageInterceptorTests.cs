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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Reth.Itss2.Dialogs;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization;
using Reth.Itss2.Workflows.Messaging;

namespace Reth.Itss2.Workflows.UnitTests.Messaging
{
    [TestClass]
    public class MessageInterceptorTests
    {
        [TestMethod]
        public void Interception_OfAnyMessage_IsSuccessfull()
        {
            Mock<IDialog> dialogStub = new Mock<IDialog>();
            Mock<IMessageFilter> messageFilterStub = new Mock<IMessageFilter>();
            Mock<IMessage> messageStub = new Mock<IMessage>();
            Mock<IDialogProvider> dialogProviderStub = new Mock<IDialogProvider>();           

            messageFilterStub.Setup( x => x.Intercept( It.IsAny<IMessage>() ) ).Returns( true );

            bool messageIntercepted = false;

            using( MessageInterceptor<IMessage> messageInterceptor = new MessageInterceptor<IMessage>(  dialogStub.Object,
                                                                                                        messageFilterStub.Object,
                                                                                                        ( MessageReceivedEventArgs<IMessage> e ) =>
                                                                                                        {
                                                                                                            messageIntercepted = true;
                                                                                                        }   ) )
            {
                dialogStub.Raise( x => x.MessageDispatching += null, new MessageDispatchingEventArgs(   messageStub.Object,
                                                                                                        dialogProviderStub.Object   ) );

                Assert.IsTrue( messageIntercepted );
            }
        }

        [TestMethod]
        public void Interception_OfAnyMessage_FailsWithException()
        {
            Mock<IDialog> dialogStub = new Mock<IDialog>();
            Mock<IMessageFilter> messageFilterStub = new Mock<IMessageFilter>();
            Mock<IMessage> messageStub = new Mock<IMessage>();
            Mock<IDialogProvider> dialogProviderStub = new Mock<IDialogProvider>();           

            messageFilterStub.Setup( x => x.Intercept( It.IsAny<IMessage>() ) ).Returns( true );

            using( MessageInterceptor<IMessage> messageInterceptor = new MessageInterceptor<IMessage>(  dialogStub.Object,
                                                                                                        messageFilterStub.Object,
                                                                                                        ( MessageReceivedEventArgs<IMessage> e ) =>
                                                                                                        {
                                                                                                            throw new InvalidOperationException();
                                                                                                        }   ) )
            {
                Assert.ThrowsException<InvalidOperationException>(  () =>
                                                                    {
                                                                        dialogStub.Raise( x => x.MessageDispatching += null, new MessageDispatchingEventArgs(   messageStub.Object,
                                                                                                                                                                dialogProviderStub.Object   ) );
                                                                    }   );
            }
        }
    }
}
