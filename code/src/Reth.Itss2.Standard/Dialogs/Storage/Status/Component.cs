using System;
using System.Text;

using Reth.Protocols;
using Reth.Protocols.Extensions.StringExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Status
{
    public class Component:IEquatable<Component>
    {
        public static bool operator==( Component left, Component right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( Component left, Component right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( Component left, Component right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.Type == right.Type );
                                                        result &= ( left.State == right.State );
                                                        result &= String.Equals( left.Description, right.Description, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.StateText, right.StateText, StringComparison.InvariantCultureIgnoreCase );

                                                        return result;
                                                    }   );
		}

        private String description = String.Empty;

        public Component(   ComponentType type,
                            ComponentState state,                    
                            String description    )
        {
            this.Type = type;
            this.State = state;
            this.Description = description;
        }

        public Component(   ComponentType type,
                            ComponentState state,                    
                            String description,
                            String stateText    )
        {
            this.Type = type;
            this.State = state;
            this.Description = description;
            this.StateText = stateText;
        }

        public ComponentType Type
        {
            get;
        } = ComponentType.StorageSystem;

        public ComponentState State
        {
            get;
        } = ComponentState.NotReady;

        public String Description
        {
            get{ return this.description; }
            
            private set
            {
                value.ThrowIfNullOrEmpty();

                this.description = value;
            }
        }

        public String StateText
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as Component );
		}
		
		public bool Equals( Component other )
		{
            return Component.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Type.GetHashCode();
		}

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append( this.Type.ToString() );
            result.Append( " (" );
            result.Append( this.State.ToString() );
            result.Append( ")" );

            return result.ToString();
        }
    }
}