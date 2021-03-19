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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput
{
    public class InitiateInputResponse:SubscribedResponse, IEquatable<InitiateInputResponse>
    {
        public static bool operator==( InitiateInputResponse? left, InitiateInputResponse? right )
		{
            return InitiateInputResponse.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputResponse? left, InitiateInputResponse? right )
		{
			return !( InitiateInputResponse.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputResponse? left, InitiateInputResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? InitiateInputResponseDetails.Equals( left?.Details, right?.Details ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IsNewDelivery, right?.IsNewDelivery ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.SetPickingIndicator, right?.SetPickingIndicator ) : false );

            return result;
		}

        public InitiateInputResponse(   MessageId id,
								        SubscriberId source,
                                        SubscriberId destination,
                                        InitiateInputResponseDetails details,
                                        IEnumerable<InitiateInputResponseArticle> articles  )
        :
            this( id, source, destination, details, articles, isNewDelivery:null, setPickingIndicator:null )
        {
        }

        public InitiateInputResponse(   MessageId id,
								        SubscriberId source,
                                        SubscriberId destination,
                                        InitiateInputResponseDetails details,
                                        IEnumerable<InitiateInputResponseArticle> articles,
                                        bool? isNewDelivery,
                                        bool? setPickingIndicator   )
        :
            base( id, StandardDialogs.InitiateInput, source, destination )
        {
            this.Details = details;

            this.Articles.AddRange( articles );
            
            this.IsNewDelivery = isNewDelivery;
            this.SetPickingIndicator = setPickingIndicator;
        }

        public InitiateInputResponse(   InitiateInputRequest request,
                                        InitiateInputResponseDetails details,
                                        IEnumerable<InitiateInputResponseArticle> articles  )
        :
            this(   request.Id,
                    request.Source,
                    request.Destination,
                    details,
                    articles,
                    request.IsNewDelivery,
                    request.SetPickingIndicator    )
        {
        }

        public bool? IsNewDelivery
        {
            get;
        }

        public bool? SetPickingIndicator
        {
            get;
        }

        public InitiateInputResponseDetails Details
        {
            get;
        }

        private List<InitiateInputResponseArticle> Articles
        {
            get;
        } = new List<InitiateInputResponseArticle>();

        public InitiateInputResponseArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputResponse );
		}
		
        public bool Equals( InitiateInputResponse? other )
		{
            return InitiateInputResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
