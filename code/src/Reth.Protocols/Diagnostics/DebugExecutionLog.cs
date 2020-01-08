using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Reth.Protocols.Diagnostics
{
    public class DebugExecutionLog:IExecutionLog
    {
        private static String GetTimestamp()
        {
            return DateTimeOffset.UtcNow.ToString( "{0:yyyy-MM-ddTHH:mm:ss.fffZ}", CultureInfo.InvariantCulture );
        }

        private static String GetMessageText( ExecutionLogMessage message )
        {
            StringBuilder result = new StringBuilder( DebugExecutionLog.GetTimestamp() );

            result.Append( " [E]" );

            if( String.IsNullOrEmpty( message.Category ) == false )
            {
                result.Append( " (" );
                result.Append( message.Category );
                result.Append( ")" );
            }

            if( String.IsNullOrEmpty( message.Message ) == false )
            {
                result.Append( " " );
                result.Append( message.Message );
            }

            return result.ToString();
        }

        public DebugExecutionLog()
        {
            Trace.AutoFlush = true;
        }

        ~DebugExecutionLog()
        {
            this.Dispose( false );
        }

        public void LogMessage( ExecutionLogMessage message )
        {
            if( !( message is null ) )
            {
                switch( message.MessageType )
                {
                    case ExecutionLogMessageType.Information:
                        Trace.TraceInformation( DebugExecutionLog.GetMessageText( message ) );
                        break;

                    case ExecutionLogMessageType.Warning:
                        Trace.TraceWarning( DebugExecutionLog.GetMessageText( message ) );
                        break;

                    case ExecutionLogMessageType.Error:
                        Trace.TraceError( DebugExecutionLog.GetMessageText( message ) );
                        break;
                }
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
