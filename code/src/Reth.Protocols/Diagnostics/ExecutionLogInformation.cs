using System;

namespace Reth.Protocols.Diagnostics
{
    public class ExecutionLogInformation:ExecutionLogMessage
    {
        public ExecutionLogInformation( String message, String category )
        :
            base( ExecutionLogMessageType.Information, message, category )
        {
        }
    }
}
