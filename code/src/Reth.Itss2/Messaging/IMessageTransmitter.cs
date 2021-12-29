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
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Reth.Itss2.Messaging
{
    public interface IMessageTransmitter:IConnectableObservable<IMessage>, IDisposable
    {
        event EventHandler<MessageProcessingErrorEventArgs>? MessageProcessingError;

        TimeSpan MessageRoundTripTimeout{ get; }

        void SendMessage( String messageEnvelope );
        void SendMessage( IMessage message );

        Task SendMessageAsync( String messageEnvelope, CancellationToken cancellationToken = default );
        Task SendMessageAsync( IMessage message, CancellationToken cancellationToken = default );

        TResponse SendRequest<TRequest, TResponse>( TRequest request )
            where TRequest:IRequest
            where TResponse:IResponse;

        Task<TResponse> SendRequestAsync<TRequest, TResponse>( TRequest request, CancellationToken cancellationToken = default )
            where TRequest:IRequest
            where TResponse:IResponse;
    }
}
