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

using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion.Messages;
using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion.Messages.ArticleInfoDialog;
using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion.Messages.ArticlePriceDialog;
using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion.Messages.ShoppingCartUpdateDialog;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Conversion
{
    public class TypeConverter:Standard.Serialization.Conversion.TypeConverter
    {
        public static ArticleTagValueConverter ArticleTagValue{ get; } = new ArticleTagValueConverter();
        public static CustomerIdConverter CustomerId{ get; } = new CustomerIdConverter();
        public static Iso4217CodeConverter Iso4217Code{ get; } = new Iso4217CodeConverter();
        public static PriceCategoryConverter PriceCategory{ get; } = new PriceCategoryConverter();
        public static SalesPersonIdConverter SalesPersonId{ get; } = new SalesPersonIdConverter();
        public static SalesPointIdConverter SalesPointId{ get; } = new SalesPointIdConverter();
        public static ShoppingCartIdConverter ShoppingCartId{ get; } = new ShoppingCartIdConverter();
        public static ShoppingCartStatusConverter ShoppingCartStatus{ get; } = new ShoppingCartStatusConverter();
        public static ShoppingCartUpdateStatusConverter ShoppingCartUpdateStatus{ get; } = new ShoppingCartUpdateStatusConverter();
        public static ViewPointIdConverter ViewPointId{ get; } = new ViewPointIdConverter();
    }
}
