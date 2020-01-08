using System;

namespace Reth.Protocols
{
    public class MessageInitializer
    {
        public MessageInitializer( Type expectedMessage )
        {
            this.ExpectedMessage = expectedMessage;
        }

        public Type ExpectedMessage
        {
            get;
        }

        private bool IsInitialized
        {
            get; set;
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private bool CanInitialize( IMessage message )
        {
            return this.ExpectedMessage?.Equals( message.GetType() ) ?? true;
        }

        public bool Initialize( IMessage message )
        {
            bool result = true;
            
            if( !( message is null ) )
            {
                lock( this.SyncRoot )
                {
                    if( this.IsInitialized == false )
                    {
                        if( this.CanInitialize( message ) == true )
                        {
                            this.IsInitialized = true;
                        }else
                        {
                            result = false;
                        }
                    }
                }
            }

            return result;
        }
    }
}
