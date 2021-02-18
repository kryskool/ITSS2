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
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Itss2.Dialogs.Standard.Diagnostics
{
    public class DebugExecutionLog:IExecutionLog
    {
        public DebugExecutionLog()
        {
            Trace.AutoFlush = true;
        }

        ~DebugExecutionLog()
        {
            this.Dispose( false );
        }

        private String FormatMessage( String message, String category, ExecutionLogLevel logLevel )
        {
            String result = String.Empty;    

            try
            {
                StringBuilder buffer = new StringBuilder( LogTimestamp.GetUtcNow() );

                buffer.Append( " [E] " );
                buffer.Append( logLevel.ToString() );
                buffer.Append( " (TID: " );
                buffer.Append( Thread.CurrentThread.ManagedThreadId.ToString() );
                buffer.Append( ")" );
                buffer.Append( " (" );
                buffer.Append( category );
                buffer.Append( ") '" );
                buffer.Append( message );
                buffer.Append( "'" );

                result = buffer.ToString();
            }catch
            {
                result = message;
            }

            return result;
        }

        private String GetCategory()
        {
            String result = String.Empty;

            try
            {
                const int skippedFrames = 2; // The skipped Frames are: this.GetCategory() and  this.LogTrace()

                StackFrame stackFrame = new StackFrame( skippedFrames );

                Type declaringType = stackFrame.GetMethod().DeclaringType;

                result = declaringType.FullName;
            }catch
            {
                // Can be ignored.
            }

            return result;
        }

        private void LogMessage( String message, String category, ExecutionLogLevel logLevel )
        {
            Trace.TraceInformation( this.FormatMessage( message, category, logLevel ) );
        }

        public void LogTrace( String message )
        {
            this.LogMessage( message, this.GetCategory(), ExecutionLogLevel.Trace );
        }

        public void LogTrace( Exception exception )
        {
            this.LogMessage( exception.Format(), this.GetCategory(), ExecutionLogLevel.Trace );
        }

        public void LogDebug( String message )
        {
            this.LogMessage( message, this.GetCategory(), ExecutionLogLevel.Debug );
        }

        public void LogDebug( Exception exception )
        {
            this.LogMessage( exception.Format(), this.GetCategory(), ExecutionLogLevel.Debug );
        }

        public void LogInformation( String message )
        {
            this.LogMessage( message, this.GetCategory(), ExecutionLogLevel.Information );
        }

        public void LogInformation( Exception exception )
        {
            this.LogMessage( exception.Format(), this.GetCategory(), ExecutionLogLevel.Information );
        }

        public void LogWarning( String message )
        {
            this.LogMessage( message, this.GetCategory(), ExecutionLogLevel.Warning );
        }

        public void LogWarning( Exception exception )
        {
            this.LogMessage( exception.Format(), this.GetCategory(), ExecutionLogLevel.Warning );
        }

        public void LogError( String message )
        {
            this.LogMessage( message, this.GetCategory(), ExecutionLogLevel.Error );
        }

        public void LogError( Exception exception )
        {
            this.LogMessage( exception.Format(), this.GetCategory(), ExecutionLogLevel.Error );
        }

        public void LogFatal( String message )
        {
            this.LogMessage( message, this.GetCategory(), ExecutionLogLevel.Fatal );
        }

        public void LogFatal( Exception exception )
        {
            this.LogMessage( exception.Format(), this.GetCategory(), ExecutionLogLevel.Fatal );
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            Trace.Flush();
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            await Task.CompletedTask.ConfigureAwait( continueOnCapturedContext:false );
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsyncCore();

            this.Dispose( false );

            GC.SuppressFinalize( this );
        }
    }
}
