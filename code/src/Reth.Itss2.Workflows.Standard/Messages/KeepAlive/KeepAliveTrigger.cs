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
using System.Timers;

namespace Reth.Itss2.Workflows.Standard.Messages.KeepAlive
{
    internal class KeepAliveTrigger:IDisposable
    {
        public static readonly TimeSpan DefaultInterval = TimeSpan.FromSeconds( 2 );

        private bool isDisposed;

        public KeepAliveTrigger( Action? callback )
        :
            this( callback, KeepAliveTrigger.DefaultInterval )
        {
        }

        public KeepAliveTrigger( Action? callback, TimeSpan interval )
        {
            this.Callback = callback;

            this.Timer.Interval = interval.TotalMilliseconds;
            this.Timer.Elapsed += this.Timer_Elapsed;
            this.Timer.AutoReset = true;
            this.Timer.Enabled = true;
        }

        ~KeepAliveTrigger()
        {
            this.Dispose( false );
        }

        private Action? Callback
        {
            get;
        }

        private Timer Timer
        {
            get; set;
        } = new Timer();

        private void Timer_Elapsed( Object sender, ElapsedEventArgs e )
        {
            try
            {
                this.Timer.Enabled = false;

                this.Callback?.Invoke();
            }catch
            {
                // TODO: Log exception
            }finally
            {
                this.Timer.Enabled = true;
            }
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if( this.isDisposed == false )
            {
                this.Timer.Elapsed -= this.Timer_Elapsed;
                this.Timer.Stop();

                if( disposing == true )
                {
                    this.Timer.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
