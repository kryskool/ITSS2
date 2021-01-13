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
using System.Globalization;

using Reth.Itss2.Dialogs.Standard.Diagnostics;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages
{
    public class SubscriberId:IEquatable<SubscriberId>
    {
        public const int MinReserved = 900;
        public const int MaxReserved = 999;

        public static SubscriberId Parse( String value )
        {
            try
            {
                return new SubscriberId( int.Parse( value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite ) );
            }catch( Exception ex )
            {
                throw Assert.Exception( new ArgumentException( $"The value '{ value }' is not a valid subscriber id.", ex ) );
            }
        }

        public static bool TryParse( String? value, out SubscriberId? result )
        {
            result = default( SubscriberId );

            bool success = false;
            
            if( value is not null )
            {
                if( int.TryParse( value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out int number ) == true )
                {
                    result = new SubscriberId( number );

                    success = true;
                }
            }

            return success;
        }

        public static bool operator==( SubscriberId? left, SubscriberId? right )
		{
            return SubscriberId.Equals( left, right );
		}
		
		public static bool operator!=( SubscriberId? left, SubscriberId? right )
		{
			return !( SubscriberId.Equals( left, right ) );
		}

        public static bool Equals( SubscriberId? left, SubscriberId? right )
		{
            return EqualityComparer<int?>.Default.Equals( left?.Value, right?.Value );
		}

        public static SubscriberId DefaultIMS
        {
            get;
        } = new SubscriberId( 100 );

        public static SubscriberId DefaultRobot
        {
            get;
        } = new SubscriberId( 999 );

        public static bool IsReserved( int value )
        {
            bool result = false;

            if( value >= SubscriberId.MinReserved && value <= SubscriberId.MaxReserved )
            {
                result = true;
            }

            return result;
        }

        public SubscriberId( int value )
        {
            if( value <= 0 )
            {
                throw Assert.Exception( new ArgumentOutOfRangeException( nameof( value ), "Subscriber id must be positive." ) );
            }

            this.Value = value;
        }

        public int Value
        {
            get;
        }

        public bool IsReserved()
        {
            return SubscriberId.IsReserved( this.Value );
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as SubscriberId );
		}
		
        public bool Equals( SubscriberId? other )
		{
            return SubscriberId.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public override String ToString()
        {
            return this.Value.ToString( CultureInfo.InvariantCulture );
        }
    }
}
