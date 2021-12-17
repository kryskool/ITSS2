﻿// Implementation of the WWKS2 protocol.
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

using Reth.Itss2.Dialogs.Standard.Protocol;

namespace Reth.Itss2.Workflows.Standard
{
    public abstract class WorkflowProvider<TDialogProvider>:IDisposable
        where TDialogProvider:IDialogProvider
    {
        public event EventHandler<MessageProcessingErrorEventArgs>? MessageProcessingError;

        protected bool isDisposed;

        protected WorkflowProvider( TDialogProvider dialogProvider )
        {
            this.DialogProvider = dialogProvider;

            dialogProvider.MessageProcessingError += this.OnMessageProcessingError;
        }

        ~WorkflowProvider()
        {
            this.Dispose( false );
        }

        public TDialogProvider DialogProvider
        {
            get;
        }

        protected virtual void OnMessageProcessingError( Object? sender, MessageProcessingErrorEventArgs e )
        {
            this.MessageProcessingError?.Invoke( this, e );
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
                if( disposing == true )
                {
                    this.DialogProvider.Dispose();
                }

                this.isDisposed = true;
            }
        }
    }
}
