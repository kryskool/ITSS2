using System;

using Reth.Protocols.Diagnostics;

namespace Reth.Protocols
{
    public static class UnhandledMessageHandler
    {
        public static void UseCallback( Action<UnhandledMessage> callback )
        {
            UnhandledMessageHandler.Callback = callback;
        }

        public static Action<UnhandledMessage> Callback
        {
            get; private set;
        }

        public static void Invoke( UnhandledMessage message )
        {
            try
            {
                UnhandledMessageHandler.Callback?.Invoke( message );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
                ExecutionLogProvider.LogError( "Execution of unhandled message callback failed." );
            }
        }
    }
}
