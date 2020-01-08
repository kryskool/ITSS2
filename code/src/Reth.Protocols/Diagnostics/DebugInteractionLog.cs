using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Reth.Protocols.Diagnostics
{
    public class DebugInteractionLog:IInteractionLog
    {
        private static String GetTimestamp()
        {
            return DateTimeOffset.UtcNow.ToString( "{0:yyyy-MM-ddTHH:mm:ss.fffZ}", CultureInfo.InvariantCulture );
        }

        private static String GetMessage( String text, MessageDirection direction )
        {
            StringBuilder result = new StringBuilder( DebugInteractionLog.GetTimestamp() );

            result.Append( " [I]" );

            if( MessageDirection.Incoming == direction )
            {
                result.Append( " R:" );
            }else
            {
                result.Append( " S:" );
            }

            if( String.IsNullOrEmpty( text ) == false )
            {
                result.Append( " " );
                result.Append( text );
            }

            return result.ToString();
        }

        public DebugInteractionLog()
        {
            Trace.AutoFlush = true;
        }

        ~DebugInteractionLog()
        {
            this.Dispose( false );
        }

        public void LogMessage( InteractionLogMessage message )
        {
            if( !( message is null ) )
            {
                Trace.TraceInformation( DebugInteractionLog.GetMessage( message.Message, message.Direction ) );
            }
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}
