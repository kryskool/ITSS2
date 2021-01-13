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
using System.Globalization;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages
{
    public class MessageId:LimitedStringId<MessageId>
    {
        private static Object SyncRoot
        {
            get;
        } = new Object();
                
        private static ulong LastGeneratedId
        {
            get; set;
        }

        public static MessageId DefaultId
        {
            get;
        } = new MessageId( "0" );

        public static MessageId Parse( String value )
        {
            return new MessageId( value );
        }

        public static bool TryParse( String? value, out MessageId? result )
        {
            return MessageId.TryParse(  value,
                                        ( String value ) => new MessageId( value ),
                                        out result  );
        }

        public static MessageId NewId()
        {
            lock( MessageId.SyncRoot )
            {
                unchecked
                {
                    return new MessageId( ( ++MessageId.LastGeneratedId ).ToString( CultureInfo.InvariantCulture ) );
                }
            }
        }

		public MessageId( String value )
        :
            base( value )
		{
		}
    }
}
