using System;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.Hello
{
    public class Capability:IEquatable<Capability>
    {
        public static bool operator==( Capability left, Capability right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( Capability left, Capability right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( Capability left, Capability right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return left.Name.Equals( right.Name );
                                                    }   );
		}

        public Capability( IDialogName name )
        {
            name.ThrowIfNull();

            this.Name = name;
        }
        
        public IDialogName Name
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as Capability );
		}
		
		public bool Equals( Capability other )
		{
            return Capability.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

        public override String ToString()
        {
            return this.Name.ToString();
        }
    }
}