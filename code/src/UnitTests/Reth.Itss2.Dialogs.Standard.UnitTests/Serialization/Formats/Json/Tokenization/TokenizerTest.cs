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
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Tokenization;
using Reth.Itss2.Dialogs.Standard.Serialization.Tokenization;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Json.Tokenization
{
    [TestClass]
    public class TokenizerTest
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
            Diagnostics.Assert.SetupForTestEnvironment();
            Diagnostics.ExecutionLogProvider.Log = new Diagnostics.DebugExecutionLog();

            this.Messages.Add( TestData.HelloRequest.Json );
            this.Messages.Add( TestData.ArticleInfoRequest.Json );

            StringBuilder messageChain = new StringBuilder();

            foreach( String message in this.Messages )
            {
                messageChain.Append( message );
            }
            
            this.BaseStream = new MemoryStream( JsonSerializationSettings.Encoding.GetBytes( messageChain.ToString() ) );
            this.BaseStream.Position = 0;
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            this.BaseStream.Dispose();

            Diagnostics.ExecutionLogProvider.Log.Dispose();
        }

        [TestMethod]
        public void TestSubscriptionOfSingleObserver()
        {
            using( ITokenizer tokenizer = new JsonTokenizer( this.BaseStream ) )
            {
                Mock<IObserver<ReadOnlySequence<byte>>> observerMock = new Mock<IObserver<ReadOnlySequence<byte>>>();

                List<String> actualMessages = new List<String>();

                using( ManualResetEventSlim sync = new ManualResetEventSlim() )
                {
                    observerMock.Setup( x => x.OnNext( It.IsAny<ReadOnlySequence<byte>>() ) ).Callback(     ( ReadOnlySequence<byte> token ) =>
                                                                                                            {
                                                                                                                String message = JsonSerializationSettings.Encoding.GetString( token.ToArray() );

                                                                                                                actualMessages.Add( message );
                                                                                                            }   );

                    observerMock.Setup( x => x.OnCompleted() ).Callback(    () =>
                                                                            {
                                                                                sync.Set();
                                                                            }   );

                    using( IDisposable subscription = tokenizer.Subscribe( observerMock.Object ) )
                    {
                        sync.Wait();

                        // Number of calls depends on buffer size.
                        observerMock.Verify( x => x.OnNext( It.IsAny<ReadOnlySequence<byte>>() ), Times.Exactly( this.Messages.Count ) );

                        Assert.AreEqual( actualMessages.Count, this.Messages.Count );

                        for( int i = 0; i < actualMessages.Count; i++ )
                        {
                            String expected = this.Messages[ i ];
                            String actual = actualMessages[ i ];

                            JsonComparer.AreEqual( expected, actual );
                        }

                        Assert.IsNotNull( subscription );
                    }
                }
            }
        }

        [TestMethod]
        public void TestSubscriptionOfMultipleObservers()
        {
            using( ITokenizer tokenizer = new JsonTokenizer( this.BaseStream, Diagnostics.NullInteractionLog.Instance, bufferSize:16, JsonSerializationSettings.MaximumMessageSize ) )
            {
                Mock<IObserver<ReadOnlySequence<byte>>> observerMockFirst = new();
                Mock<IObserver<ReadOnlySequence<byte>>> observerMockSecond = new();

                using( ManualResetEventSlim observerMockFirstSync = new ManualResetEventSlim() )
                {
                    using( ManualResetEventSlim observerMockSecondSync = new ManualResetEventSlim() )
                    {
                        observerMockFirst.Setup( x => x.OnCompleted() ).Callback(   () =>
                                                                                    {
                                                                                        observerMockFirstSync.Set();
                                                                                    }   );

                        observerMockSecond.Setup( x => x.OnCompleted() ).Callback(  () =>
                                                                                    {
                                                                                        observerMockSecondSync.Set();
                                                                                    }   );

                        IConnectableObservable<ReadOnlySequence<byte>> observable = (   from token in tokenizer
                                                                                        select token    ).Publish();

                        observable.Subscribe( observerMockFirst.Object );
                        observable.Subscribe( observerMockSecond.Object );

                        observable.Connect();

                        observerMockFirstSync.Wait();
                        observerMockSecondSync.Wait();

                        // Number of calls depends on buffer size.
                        observerMockFirst.Verify( x => x.OnNext( It.IsAny<ReadOnlySequence<byte>>() ), Times.AtLeast( this.Messages.Count ) );
                        observerMockSecond.Verify( x => x.OnNext( It.IsAny<ReadOnlySequence<byte>>() ), Times.AtLeast( this.Messages.Count ) );
                    }
                }
            }
        }
    }
}
