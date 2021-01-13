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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputDialog
{
    public class OutputLabel:IEquatable<OutputLabel>
    {
        public static bool operator==( OutputLabel? left, OutputLabel? right )
		{
            return OutputLabel.Equals( left, right );
		}
		
		public static bool operator!=( OutputLabel? left, OutputLabel? right )
		{
			return !( OutputLabel.Equals( left, right ) );
		}

        public static bool Equals( OutputLabel? left, OutputLabel? right )
		{
            bool result = String.Equals( left?.TemplateId, right?.TemplateId, StringComparison.OrdinalIgnoreCase );
            
            result &= ( result ? String.Equals( left?.Content, right?.Content, StringComparison.OrdinalIgnoreCase ) : false );
            
            return result;
		}

        public OutputLabel( String templateId, String content )
        {
            templateId.ThrowIfEmpty();

            this.TemplateId = templateId;
            this.Content = content;
        }

        public String TemplateId
        {
            get;
        }

        public String Content
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputLabel );
		}
		
        public bool Equals( OutputLabel? other )
		{
            return OutputLabel.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return HashCode.Combine( this.TemplateId, this.Content );
        }

        public override String ToString()
        {
            return this.TemplateId;
        }
    }
}
