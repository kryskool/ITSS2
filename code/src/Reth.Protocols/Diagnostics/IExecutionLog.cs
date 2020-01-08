using System;

namespace Reth.Protocols.Diagnostics
{
    public interface IExecutionLog:IDisposable
    {
        void LogMessage( ExecutionLogMessage message );
    }
}
