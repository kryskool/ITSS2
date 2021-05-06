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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages
{
    public class OutputBox:IEquatable<OutputBox>
    {
        public static bool operator==( OutputBox? left, OutputBox? right )
		{
            return OutputBox.Equals( left, right );
		}
		
		public static bool operator!=( OutputBox? left, OutputBox? right )
		{
			return !( OutputBox.Equals( left, right ) );
		}

        public static bool Equals( OutputBox? left, OutputBox? right )
		{
            return String.Equals( left?.Number, right?.Number, StringComparison.OrdinalIgnoreCase );
		}

        public OutputBox( String number )
        {
            number.ThrowIfEmpty();

            this.Number = number;
        }

        public String Number
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputBox );
		}
		
		public bool Equals( OutputBox? other )
		{
            return OutputBox.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Number.GetHashCode();
		}

        public override String ToString()
        {
            return this.Number;
        }
    }
}
