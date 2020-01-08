using System;
using System.Collections.Generic;
using System.Threading;

using Reth.Protocols.Diagnostics;

namespace Reth.Protocols
{
    internal class MessageQueue:IMessageReadQueue, IMessageWriteQueue
    {
        private const int MaxCount = 100;
        private const int MinCount = 0;

        private volatile bool isCancellationRequested;

        public MessageQueue()
        {
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private Queue<IMessage> Messages
        {
            get;
        } = new Queue<IMessage>();

        private bool IsCancellationRequested
        {
            get
            {
                return this.isCancellationRequested;
            }

            set
            {
                this.isCancellationRequested = value;
            }
        }

        public void Terminate()
        {
            lock( this.SyncRoot )
            {
                if( this.IsCancellationRequested == false )
                {
                    this.PostMessage( null );
                }
            }
        }

        public bool PostMessage( IMessage message )
        {
            bool result = true;
            
            lock( this.SyncRoot )
            {
                if( message is null )
                {
                    this.IsCancellationRequested = true;
                }

                while(  ( this.Messages.Count == MessageQueue.MaxCount ) &&
                        ( this.IsCancellationRequested == false   )  )
                {
                    ExecutionLogProvider.LogInformation( "Waiting to enqueue next message..." );

                    Monitor.Wait( this.SyncRoot );
                }

                if( this.IsCancellationRequested == false )
                {
                    this.Messages.Enqueue( message );

                    ExecutionLogProvider.LogInformation( $"Message enqueued: { message.ToString() } ({ message.GetType().FullName })." );
                }else
                {
                    if( !( message is null ) )
                    {
                        result = false;

                        ExecutionLogProvider.LogInformation( $"Posting message is rejected due to cancellation: { message.ToString() } ({ message.GetType().FullName })." );
                    }
                }

                Monitor.PulseAll( this.SyncRoot );
            }

            return result;
        }

        public IMessage GetMessage()
        {
            IMessage result = null;

            lock( this.SyncRoot )
            {
                while(  ( this.Messages.Count == MessageQueue.MinCount ) &&
                        ( this.IsCancellationRequested == false   )  )
                {
                    ExecutionLogProvider.LogInformation( "Waiting to dequeue next message..." );

                    Monitor.Wait( this.SyncRoot );
                }

                if( this.Messages.Count > MessageQueue.MinCount )
                {
                    if( this.IsCancellationRequested == false )
                    {
                        result = this.Messages.Dequeue();    
                    }else
                    {
                        IMessage message = null;

                        do
                        {
                            message = this.Messages.Dequeue();

                            if( !( message is null ) )
                            {
                                ExecutionLogProvider.LogInformation( $"Getting message is rejected due to cancellation: { message.ToString() } ({ message.GetType().FullName })." );
                            }
                        }while( message != null );
                    }
                }

                Monitor.PulseAll( this.SyncRoot );

                if( !( result is null ) )
                {
                    ExecutionLogProvider.LogInformation( $"Message dequeued: { result.ToString() } ({ result.GetType().FullName })." );
                }
            }

            return result;
        }
    }
}