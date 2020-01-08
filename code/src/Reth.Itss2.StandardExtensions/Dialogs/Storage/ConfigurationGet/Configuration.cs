using System;
using System.Globalization;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet
{
    public class Configuration:IEquatable<Configuration>
    {
        public static bool operator==( Configuration left, Configuration right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( Configuration left, Configuration right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( Configuration left, Configuration right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return String.Equals( left.Data, right.Data, StringComparison.InvariantCultureIgnoreCase );
                                                    }   );
		}

        public Configuration( String data )
        {
            data.ThrowIfNull();

            this.Data = data;
        }

        public String Data
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as Configuration );
		}
		
		public bool Equals( Configuration other )
		{
            return Configuration.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Data.GetHashCode();
		}

        public override String ToString()
        {
            return this.Data.ToString( CultureInfo.InvariantCulture );
        }
    }
}