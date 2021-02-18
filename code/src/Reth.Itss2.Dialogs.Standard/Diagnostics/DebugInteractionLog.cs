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
using System.Threading.Tasks;

namespace Reth.Itss2.Dialogs.Standard.Diagnostics
{
    public class DebugInteractionLog:IInteractionLog
    {
        public DebugInteractionLog( String localEndPoint, String remoteEndPoint )
        {
            this.LocalEndPoint = localEndPoint;
            this.RemoteEndPoint = remoteEndPoint;

            Trace.AutoFlush = true;
        }

        ~DebugInteractionLog()
        {
            this.Dispose( false );
        }

        public String LocalEndPoint
        {
            get;
        }

        public String RemoteEndPoint
        {
            get;
        }

        private String FormatMessage( String message, bool incoming )
        {
            String result = String.Empty;    

            try
            {
                StringBuilder buffer = new StringBuilder( LogTimestamp.GetUtcNow() );

                buffer.Append( " [I] " );
                buffer.Append( this.LocalEndPoint );
                buffer.Append( " <-> " );
                buffer.Append( this.RemoteEndPoint );

                if( incoming == true )
                {
                    buffer.Append( " (R):" );
                }else
                {
                    buffer.Append( " (S):" );
                }

                buffer.Append( " '" );
                buffer.Append( message );
                buffer.Append( "'" );

                result = buffer.ToString();
            }catch
            {
                result = message;
            }

            return result;
        }

        public void LogIncoming( String message )
        {
            Trace.TraceInformation( this.FormatMessage( message, true ) );
        }

        public void LogOutgoing( String message )
        {
            Trace.TraceInformation( this.FormatMessage( message, false ) );
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
