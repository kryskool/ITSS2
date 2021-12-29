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

using Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages.ArticleMasterSet;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages.InitiateInput;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages.Input;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages.Status;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages.StockDeliveryInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages.StockDeliverySet;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages.Output;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages.OutputInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion.Messages.TaskCancelOutput;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Conversion
{
    public class TypeConverter:Reth.Itss2.Serialization.Conversion.TypeConverter
    {
        public static ArticleIdConverter ArticleId{ get; } = new ArticleIdConverter();
        public static ArticleMasterSetResultValueConverter ArticleMasterSetResultValue{ get; } = new ArticleMasterSetResultValueConverter();
        public static ComponentStateConverter ComponentState{ get; } = new ComponentStateConverter();
        public static ComponentTypeConverter ComponentType{ get; } = new ComponentTypeConverter();
        public static InitiateInputErrorTypeConverter InitiateInputErrorType{ get; } = new InitiateInputErrorTypeConverter();
        public static InitiateInputMessageStatusConverter InitiateInputMessageStatus{ get; } = new InitiateInputMessageStatusConverter();
        public static InitiateInputResponseStatusConverter InitiateInputResponseStatus{ get; } = new InitiateInputResponseStatusConverter();
        public static InputMessagePackHandlingInputConverter InputMessagePackHandlingInput{ get; } = new InputMessagePackHandlingInputConverter();
        public static InputResponsePackHandlingInputConverter InputResponsePackHandlingInput{ get; } = new InputResponsePackHandlingInputConverter();
        public static LabelStatusConverter LabelStatus{ get; } = new LabelStatusConverter();
        public static OutputInfoStatusConverter OutputInfoStatus{ get; } = new OutputInfoStatusConverter();
        public static OutputPriorityConverter OutputPriority{ get; } = new OutputPriorityConverter();
        public static OutputMessageStatusConverter OutputMessageStatus{ get; } = new OutputMessageStatusConverter();
        public static OutputResponseStatusConverter OutputResponseStatus{ get; } = new OutputResponseStatusConverter();
        public static PackIdConverter PackId{ get; } = new PackIdConverter();
        public static PackDateConverter PackDate{ get; } = new PackDateConverter();
        public static PackShapeConverter PackShape{ get; } = new PackShapeConverter();
        public static PackStateConverter PackState{ get; } = new PackStateConverter();
        public static ProductCodeIdConverter ProductCodeId{ get; } = new ProductCodeIdConverter();
        public static StockDeliveryInfoStatusConverter StockDeliveryInfoStatus{ get; } = new StockDeliveryInfoStatusConverter();
        public static StockDeliverySetResultValueConverter StockDeliverySetResultValue{ get; } = new StockDeliverySetResultValueConverter();
        public static StockLocationIdConverter StockLocationId{ get; } = new StockLocationIdConverter();
        public static SubscriberIdConverter SubscriberId{ get; } = new SubscriberIdConverter();
        public static SubscriberTypeConverter SubscriberType{ get; } = new SubscriberTypeConverter();
        public static TaskCancelOutputStatusConverter TaskCancelOutputStatus{ get; } = new TaskCancelOutputStatusConverter();
        public static UnprocessedReasonConverter UnprocessedReason{ get; } = new UnprocessedReasonConverter();

        protected TypeConverter()
        {
        }
    }
}
