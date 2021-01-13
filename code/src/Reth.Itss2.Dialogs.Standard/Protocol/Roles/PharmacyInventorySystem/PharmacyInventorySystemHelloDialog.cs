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

using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.HelloDialog;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Roles.PharmacyInventorySystem
{
    public class PharmacyInventorySystemHelloDialog:Dialog, IPharmacyInventorySystemHelloDialog
    {
        public PharmacyInventorySystemHelloDialog( IDialogProvider dialogProvider )
        :
            base( "Hello", dialogProvider )
        {
        }

        public HelloResponse SendRequest( HelloRequest request )
        {
            return base.SendRequest<HelloRequest, HelloResponse>( request );
        }

        public Task<HelloResponse> SendRequestAsync( HelloRequest request )
        {
            return base.SendRequestAsync<HelloRequest, HelloResponse>( request );
        }

        public Task<HelloResponse> SendRequestAsync( HelloRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<HelloRequest, HelloResponse>( request, cancellationToken );
        }
    }
}
