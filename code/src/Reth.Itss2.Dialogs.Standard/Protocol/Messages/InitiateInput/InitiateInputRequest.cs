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
    public class InitiateInputRequest:SubscribedRequest, IEquatable<InitiateInputRequest>
    {
        public static bool operator==( InitiateInputRequest? left, InitiateInputRequest? right )
		{
            return InitiateInputRequest.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputRequest? left, InitiateInputRequest? right )
		{
			return !( InitiateInputRequest.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputRequest? left, InitiateInputRequest? right )
		{
            bool result = SubscribedRequest.Equals( left, right );

            result &= ( result ? InitiateInputRequestDetails.Equals( left?.Details, right?.Details ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IsNewDelivery, right?.IsNewDelivery ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.SetPickingIndicator, right?.SetPickingIndicator ) : false );

            return result;
		}

        public InitiateInputRequest(    MessageId id,
								        SubscriberId source,
                                        SubscriberId destination,
                                        InitiateInputRequestDetails details,
                                        IEnumerable<InitiateInputRequestArticle> articles  )
        :
            this(   id,
                    source,
                    destination,
                    details,
                    articles,
                    isNewDelivery:null,
                    setPickingIndicator:null    )
        {
        }

        public InitiateInputRequest(    MessageId id,
								        SubscriberId source,
                                        SubscriberId destination,
                                        InitiateInputRequestDetails details,
                                        IEnumerable<InitiateInputRequestArticle> articles,
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

        public bool? IsNewDelivery
        {
            get;
        }

        public bool? SetPickingIndicator
        {
            get;
        }

        public InitiateInputRequestDetails Details
        {
            get;
        }

        private List<InitiateInputRequestArticle> Articles
        {
            get;
        } = new List<InitiateInputRequestArticle>();

        public InitiateInputRequestArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputRequest );
		}
		
        public bool Equals( InitiateInputRequest? other )
		{
            return InitiateInputRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
