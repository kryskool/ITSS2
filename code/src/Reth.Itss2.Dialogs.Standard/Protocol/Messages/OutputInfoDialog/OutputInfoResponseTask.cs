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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfoDialog
{
    public class OutputInfoResponseTask:IEquatable<OutputInfoResponseTask>
    {
        public static bool operator==( OutputInfoResponseTask? left, OutputInfoResponseTask? right )
		{
            return OutputInfoResponseTask.Equals( left, right );
		}
		
		public static bool operator!=( OutputInfoResponseTask? left, OutputInfoResponseTask? right )
		{
			return !( OutputInfoResponseTask.Equals( left, right ) );
		}

        public static bool Equals( OutputInfoResponseTask? left, OutputInfoResponseTask? right )
		{
            bool result = MessageId.Equals( left?.Id, right?.Id );

            result &= ( result ? EqualityComparer<OutputInfoStatus?>.Default.Equals( left?.Status, right?.Status ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public OutputInfoResponseTask(  MessageId id,
                                        OutputInfoStatus status  )
        :
            this( id, status, null )
        {
        }

        public OutputInfoResponseTask(  MessageId id,
                                        OutputInfoStatus status,
                                        IEnumerable<OutputInfoArticle>? articles )
        {
            this.Id = id;
            this.Status = status;

            if( articles is not null )
            {
                this.Articles.AddRange( articles );
            }
        }

        public MessageId Id
        {
            get;
        }

        public OutputInfoStatus Status
        {
            get;
        }

        private List<OutputInfoArticle> Articles
        {
            get;
        } = new List<OutputInfoArticle>();
        
        public OutputInfoArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputInfoResponseTask );
		}
		
        public bool Equals( OutputInfoResponseTask? other )
		{
            return OutputInfoResponseTask.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return $"{ this.Id }, { this.Status }";
        }
    }
}
