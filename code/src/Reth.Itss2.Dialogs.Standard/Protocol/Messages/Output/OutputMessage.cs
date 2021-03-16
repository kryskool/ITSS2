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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output
{
    public class OutputMessage:SubscribedMessage, IEquatable<OutputMessage>
    {
        public static bool operator==( OutputMessage? left, OutputMessage? right )
		{
            return OutputMessage.Equals( left, right );
		}
		
		public static bool operator!=( OutputMessage? left, OutputMessage? right )
		{
			return !( OutputMessage.Equals( left, right ) );
		}

        public static bool Equals( OutputMessage? left, OutputMessage? right )
		{
            bool result = SubscribedMessage.Equals( left, right );

            result &= ( result ? OutputMessageDetails.Equals( left?.Details, right?.Details ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );
            result &= ( result ? ( left?.Boxes.SequenceEqual( right?.Boxes ) ).GetValueOrDefault() : false );

            return result;
		}

        public OutputMessage(   MessageId id,
								SubscriberId source,
                                SubscriberId destination,
                                OutputMessageDetails details    )
        :
            base( id, Dialogs.Output, source, destination )
        {
            this.Details = details;
        }

        public OutputMessage(   MessageId id,
								SubscriberId source,
                                SubscriberId destination,
                                OutputMessageDetails details,
                                IEnumerable<OutputArticle>? articles,
                                IEnumerable<OutputBox>? boxes    )
        :
            base( id, Dialogs.Output, source, destination )
        {
            this.Details = details;

            if( articles is not null )
            {
                this.Articles.AddRange( articles );
            }

            if( boxes is not null )
            {
                this.Boxes.AddRange( boxes );
            }
        }

        public OutputMessage(   OutputRequest request,
                                OutputMessageDetails details    )
        :
            this( request.Id, request.Destination, request.Source, details )
        {
        }

        public OutputMessage(   OutputRequest request,
                                OutputMessageDetails details,
                                IEnumerable<OutputArticle>? articles,
                                IEnumerable<OutputBox>? boxes    )
        :
            this( request.Id, request.Destination, request.Source, details, articles, boxes )
        {
        }

        public OutputMessageDetails Details
        {
            get;
        }

        private List<OutputArticle> Articles
        {
            get;
        } = new List<OutputArticle>();

        private List<OutputBox> Boxes
        {
            get;
        } = new List<OutputBox>();

        public OutputArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public OutputBox[] GetBoxes()
        {
            return this.Boxes.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputMessage );
		}
		
        public bool Equals( OutputMessage? other )
		{
            return OutputMessage.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
