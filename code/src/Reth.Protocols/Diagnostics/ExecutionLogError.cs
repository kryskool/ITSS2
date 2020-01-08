using System;

namespace Reth.Protocols.Diagnostics
{
    public class ExecutionLogError:ExecutionLogMessage
    {
        public static ExecutionLogError FromException( Exception exception, String category )
        {
            ExecutionLogError result = null;

            if( !( exception is null ) )
            {
                result = new ExecutionLogError( ExecutionLogMessage.FormatException( exception ), category );
            }else
            {
                result = new ExecutionLogError( "Tracing null exception.", category );
            }

            return result;
        }

        public ExecutionLogError( String message, String category )
        :
            base( ExecutionLogMessageType.Error, message, category )
        {
        }
    }
}
