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
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.HelloDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.PharmacyInventorySystem;
using Reth.Itss2.Dialogs.Standard.Serialization.Xml;
using Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Xml;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Protocol.Roles.PharmacyInventorySystem
{
    [TestClass]
    public class PharmacyInventorySystemDialogProviderTest
    {
        private DataContractResolver DataContractResolver
        {
            get; set;
        }

        [TestInitialize]
        public void Initialize()
        {
            Diagnostics.Assert.SetupForTestEnvironment();

            this.DataContractResolver = new DataContractResolver( typeof( PharmacyInventorySystemDialogProvider ) );
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.DataContractResolver = null;
        }

        [TestMethod]
        public void TestHelloDialogSendRequest()
        {
            Mock<IMessageTransmitter> messageTransmitterMock = new Mock<IMessageTransmitter>();

            messageTransmitterMock.Setup( x => x.SendRequest<HelloRequest, HelloResponse>( It.IsAny<HelloRequest>() ) ).Returns<HelloRequest>(  ( HelloRequest request ) =>
                                                                                                                                                {
                                                                                                                                                    return ( HelloResponse )( TestData.HelloResponse.Object.Message );
                                                                                                                                                } );

            using( IPharmacyInventorySystemDialogProvider dialogProvider = new PharmacyInventorySystemDialogProvider() )
            {
                dialogProvider.Connect( messageTransmitterMock.Object, false );

                HelloResponse response = dialogProvider.HelloDialog.SendRequest( ( HelloRequest )( TestData.HelloRequest.Object.Message ) );

                Assert.AreEqual( TestData.HelloResponse.Object.Message, response );
            }
        }

        [TestMethod]
        public void TestHelloDialogSendRequestWithTimeout()
        {
            TimeSpan timeout = TimeSpan.FromMilliseconds( 1 );

            Mock<IMessageStreamReader> messageStreamReaderMock = new Mock<IMessageStreamReader>();

            messageStreamReaderMock.Setup( x => x.Subscribe( It.IsAny<IObserver<IMessageEnvelope>>() ) ).Callback<IObserver<IMessageEnvelope>>( async( IObserver<IMessageEnvelope> observer ) =>
                                                                                                                                                {
                                                                                                                                                    await Task.Delay( timeout.Add( TimeSpan.FromMilliseconds( 1 ) ) );
                                                                                                                                                }   );
            
            MessageStreamWriter messageStreamWriter = new MessageStreamWriter( Stream.Null, this.DataContractResolver );
            
            using(  IPharmacyInventorySystemDialogProvider dialogProvider = new PharmacyInventorySystemDialogProvider() )
            {
                dialogProvider.Connect( new MessageTransmitter( messageStreamReaderMock.Object,
                                                                messageStreamWriter,
                                                                timeout ),
                                        false   );

                Assert.ThrowsException<MessageTransmissionException>(   () =>
                                                                    {
                                                                        dialogProvider.HelloDialog.SendRequest( ( HelloRequest )( TestData.HelloRequest.Object.Message ) );
                                                                    }   );
            }
        }

        [TestMethod]
        public void TestHelloDialogSendRequestWithoutResponse()
        {
            MessageStreamReader messageStreamReader = new MessageStreamReader( Stream.Null, this.DataContractResolver );
            MessageStreamWriter messageStreamWriter = new MessageStreamWriter( Stream.Null, this.DataContractResolver );

            using(  IPharmacyInventorySystemDialogProvider dialogProvider = new PharmacyInventorySystemDialogProvider() )
            {
                dialogProvider.Connect( new MessageTransmitter( messageStreamReader, messageStreamWriter ), false );

                Assert.ThrowsException<MessageTransmissionException>(   () =>
                                                                    {
                                                                        dialogProvider.HelloDialog.SendRequest( ( HelloRequest )( TestData.HelloRequest.Object.Message ) );
                                                                    }   );
            }
        }
    }
}
