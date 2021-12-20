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
using System.Collections.Generic;
using System.Linq;

using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.Status
{
    public class StatusResponse:SubscribedResponse, IEquatable<StatusResponse>
    {
        public static bool operator==( StatusResponse? left, StatusResponse? right )
		{
            return StatusResponse.Equals( left, right );
		}
		
		public static bool operator!=( StatusResponse? left, StatusResponse? right )
		{
			return !( StatusResponse.Equals( left, right ) );
		}

        public static bool Equals( StatusResponse? left, StatusResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? ( left?.Components.SequenceEqual( right?.Components ) ).GetValueOrDefault() : false );

            return result;
		}

        public StatusResponse(  MessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                ComponentState state    )
        :
            this( id, source, destination, state, stateText:null, components:null )
        {
            this.State = state;
        }

        public StatusResponse(  MessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                ComponentState state,
                                String? stateText,
                                IEnumerable<Component>? components   )
        :
            base( id, StandardDialogs.Status, source, destination )
        {
            this.State = state;
            this.StateText = stateText;

            if( components is not null )
            {
                this.Components.AddRange( components );
            }
        }

        public StatusResponse(  StatusRequest request,
                                ComponentState state    )
        :
            this( request, state, stateText:null, components:null )
        {
        }

        public StatusResponse(  StatusRequest request,
                                ComponentState state,
                                String? stateText,
                                IEnumerable<Component>? components  )
        :
            base( request )
        {
            this.State = state;
            this.StateText = stateText;

            if( components is not null )
            {
                this.Components.AddRange( components );
            }
        }

        public ComponentState State
        {
            get;
        } = ComponentState.NotReady;

        public String? StateText
        {
            get;
        }

        private List<Component> Components
        {
            get;
        } = new List<Component>();
        
        public Component[] GetComponents()
        {
            return this.Components.ToArray();
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as StatusResponse );
		}
		
        public bool Equals( StatusResponse? other )
		{
            return StatusResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
