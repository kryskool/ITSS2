using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Status
{
    public class StatusResponse:TraceableResponse, IEquatable<StatusResponse>
    {
        public static bool operator==( StatusResponse left, StatusResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StatusResponse left, StatusResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StatusResponse left, StatusResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= left.State.Equals( right.State );
                result &= String.Equals( left.StateText, right.StateText, StringComparison.InvariantCultureIgnoreCase );
                result &= left.Components.ElementsEqual( right.Components );
            }

            return result;
		}

        public StatusResponse(  IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                ComponentState state    )
        :
            base( DialogName.OutputInfo, id, source, destination )
        {
            this.State = state;
        }

        public StatusResponse(  IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                ComponentState state,
                                String stateText   )
        :
            base( DialogName.Status, id, source, destination )
        {
            this.State = state;
            this.StateText = stateText;
        }

        public StatusResponse(  IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                ComponentState state,
                                String stateText,
                                IEnumerable<Component> components   )
        :
            base( DialogName.OutputInfo, id, source, destination )
        {
            this.State = state;
            this.StateText = stateText;

            if( !( components is null ) )
            {
                this.Components.AddRange( components );
            }
        }

        public StatusResponse(  StatusRequest request,
                                ComponentState state    )
        :
            base( request )
        {
            this.State = state;
        }

        public StatusResponse(  StatusRequest request,
                                ComponentState state,
                                String stateText   )
        :
            base( request )
        {
            this.State = state;
            this.StateText = stateText;
        }

        public StatusResponse(  StatusRequest request,
                                ComponentState state,
                                String stateText,
                                IEnumerable<Component> components   )
        :
            base( request )
        {
            this.State = state;
            this.StateText = stateText;
            
            if( !( components is null ) )
            {
                this.Components.AddRange( components );
            }
        }

        public ComponentState State
        {
            get;
        } = ComponentState.NotReady;

        public String StateText
        {
            get;
        }

        private List<Component> Components
        {
            get;
        } = new List<Component>();

        public Component[] GetComponents()
        {
            Component[] result = null;

            if( !( this.Components is null ) )
            {
                result = this.Components.ToArray();
            }else
            {
                result = Array.Empty<Component>();
            }

            return result;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StatusResponse );
		}
		
		public bool Equals( StatusResponse other )
		{
            return StatusResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

        public override void Dispatch( IMessageDispatcher dispatcher )
        {
            dispatcher.ThrowIfNull();
            dispatcher.Dispatch( this );
        }
    }
}