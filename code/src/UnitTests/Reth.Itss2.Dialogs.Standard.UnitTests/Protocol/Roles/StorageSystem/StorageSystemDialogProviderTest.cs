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
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Hello;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml;
using Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml;
using Reth.Itss2.UnitTests;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Protocol.Roles.StorageSystem
{
    [TestClass]
    public class StorageSystemDialogProviderTest:TestBase
    {

        [TestMethod]
        public void TestHelloDialogSendResponse()
        {
            using( MemoryStream stream = new MemoryStream() )
            {
                Mock<IMessageStreamReader> messageStreamReaderMock = new Mock<IMessageStreamReader>();
                IMessageStreamWriter messageStreamWriter = new XmlMessageStreamWriter( stream );

                using( IStorageSystemDialogProvider dialogProvider = new StorageSystemDialogProvider() )
                {
                    dialogProvider.Connect( new MessageTransmitter( messageStreamReaderMock.Object, messageStreamWriter, stream ) );

                    dialogProvider.HelloDialog.SendResponse( ( HelloResponse )( TestData.HelloResponse.Object.Message ) );

                    stream.Position = 0;

                    using( IMessageStreamReader messageStreamReader = new XmlMessageStreamReader( stream ) )
                    {
                        Mock<IObserver<IMessageEnvelope>> observerMock = new Mock<IObserver<IMessageEnvelope>>();

                        observerMock.Setup( x => x.OnNext( It.IsAny<IMessageEnvelope>() ) ).Callback<IMessageEnvelope>( ( IMessageEnvelope value ) =>
                                                                                                                        {
                                                                                                                            Assert.AreEqual( TestData.HelloResponse.Object.Message, value.Message );
                                                                                                                        }   );

                        messageStreamReader.Subscribe( observerMock.Object );
                    }
                }
            }
        }
    }
}
