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
using System.Collections.Generic;
using System.IO;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization;
using Reth.Itss2.Serialization.Formats.Xml;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml
{
    [TestClass]
    public class MessageTransmitterTest
    {
        private List<String> Messages
        {
            get;
        } = new List<String>();

        private MemoryStream BaseStream
        {
            get; set;
        } = new MemoryStream();

        [TestInitialize]
        public void Initialize()
        {
            this.Messages.Add( TestData.HelloRequest.Xml );
            this.Messages.Add( TestData.ArticleInfoRequest.Xml );

            StringBuilder messageChain = new StringBuilder();

            foreach( String message in this.Messages )
            {
                messageChain.Append( message );
            }

            this.BaseStream = new MemoryStream( XmlSerializationSettings.Encoding.GetBytes( messageChain.ToString() ) );
            this.BaseStream.Position = 0;
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            this.BaseStream.Dispose();
        }

        [TestMethod]
        public void TestSubscriptionOfSingleObserver()
        {
            
            using(  IMessageTransmitter transmitter = new MessageTransmitter(   new XmlMessageStreamReader( this.BaseStream, new XmlSerializationProvider() ),
                                                                                new XmlMessageStreamWriter( this.BaseStream, new XmlSerializationProvider() ),
                                                                                this.BaseStream ) )
            {
                Mock<IObserver<IMessage>> observerMock = new Mock<IObserver<IMessage>>();

                using( ManualResetEventSlim sync = new() )
                {
                    observerMock.Setup( x => x.OnCompleted() ).Callback(    () =>
                                                                            {
                                                                                sync.Set();
                                                                            }   );

                    IConnectableObservable<IMessage> observable = transmitter;

                    using( IDisposable subscription = transmitter.Subscribe( observerMock.Object ) )
                    {
                        observable.Connect();

                        sync.Wait();

                        observerMock.Verify( x => x.OnNext( It.IsAny<IMessage>() ), Times.Exactly( this.Messages.Count ) );

                        Assert.IsNotNull( subscription );
                    }
                }
            }
        }

        [TestMethod]
        public void TestSubscriptionOfMultipleObservers()
        {
            using(  IMessageTransmitter transmitter = new MessageTransmitter(   new XmlMessageStreamReader( this.BaseStream, new XmlSerializationProvider() ),
                                                                                new XmlMessageStreamWriter( this.BaseStream, new XmlSerializationProvider() ),
                                                                                this.BaseStream ) )
            {
                Mock<IObserver<IMessage>> observerMock1 = new Mock<IObserver<IMessage>>();
                Mock<IObserver<IMessage>> observerMock2 = new Mock<IObserver<IMessage>>();

                using( ManualResetEventSlim observerMock1Sync = new ManualResetEventSlim() )
                {
                    using( ManualResetEventSlim observerMock2Sync = new ManualResetEventSlim() )
                    {
                        observerMock1.Setup( x => x.OnCompleted() ).Callback(   () =>
                                                                                {
                                                                                    observerMock1Sync.Set();
                                                                                }   );

                        observerMock2.Setup( x => x.OnCompleted() ).Callback(   () =>
                                                                                {
                                                                                    observerMock2Sync.Set();
                                                                                }   );

                        IConnectableObservable<IMessage> observable =  transmitter;

                        observable.Subscribe( observerMock1.Object );
                        observable.Subscribe( observerMock2.Object );

                        observable.Connect();

                        observerMock1Sync.Wait();
                        observerMock2Sync.Wait();

                        observerMock1.Verify( x => x.OnNext( It.IsAny<IMessage>() ), Times.Exactly( this.Messages.Count ) );
                        observerMock2.Verify( x => x.OnNext( It.IsAny<IMessage>() ), Times.Exactly( this.Messages.Count ) );
                    }
                }
            }
        }
    }
}
