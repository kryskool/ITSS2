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
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Itss2.Dialogs.Standard.Diagnostics
{
    public class NullExecutionLog:IExecutionLog
    {
        private static Lazy<NullExecutionLog> instance = new Lazy<NullExecutionLog>(    () =>
                                                                                        {
                                                                                            return new NullExecutionLog();
                                                                                        },
                                                                                        LazyThreadSafetyMode.ExecutionAndPublication    );
        public static NullExecutionLog Instance
        {
            get{ return NullExecutionLog.instance.Value; }
        }

        private NullExecutionLog()
        {
        }

        ~NullExecutionLog()
        {
            this.Dispose( false );
        }

        public void LogTrace( String message )
        {
            // Empty by intention.
        }

        public void LogTrace( Exception exception )
        {
            // Empty by intention.
        }

        public void LogDebug( String message )
        {
            // Empty by intention.
        }

        public void LogDebug( Exception exception )
        {
            // Empty by intention.
        }

        public void LogInformation( String message )
        {
            // Empty by intention.
        }

        public void LogInformation( Exception exception )
        {
            // Empty by intention.
        }

        public void LogWarning( String message )
        {
            // Empty by intention.
        }

        public void LogWarning( Exception exception )
        {
            // Empty by intention.
        }

        public void LogError( String message )
        {
            // Empty by intention.
        }

        public void LogError( Exception exception )
        {
            // Empty by intention.
        }

        public void LogFatal( String message )
        {
            // Empty by intention.
        }

        public void LogFatal( Exception exception )
        {
            // Empty by intention.
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
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
