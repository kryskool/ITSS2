using System;

namespace Reth.Protocols.Diagnostics
{
    public interface IInteractionLog:IDisposable
    {
        void LogMessage( InteractionLogMessage message );
    }
}
