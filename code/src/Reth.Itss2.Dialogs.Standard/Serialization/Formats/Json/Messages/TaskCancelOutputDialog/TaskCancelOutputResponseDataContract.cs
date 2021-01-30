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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutputDialog;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.TaskCancelOutputDialog
{
    public class TaskCancelOutputResponseDataContract:SubscribedResponseDataContract<TaskCancelOutputResponse>
    {
        public TaskCancelOutputResponseDataContract()
        {
            this.Task = new TaskCancelOutputResponseTaskDataContract();
        }

        public TaskCancelOutputResponseDataContract( TaskCancelOutputResponse dataObject )
        :
            base( dataObject )
        {
            this.Task = TypeConverter.ConvertFromDataObject<TaskCancelOutputResponseTask, TaskCancelOutputResponseTaskDataContract>( dataObject.Task );
        }

        public TaskCancelOutputResponseTaskDataContract Task{ get; set; }

        public override TaskCancelOutputResponse GetDataObject()
        {
            return new TaskCancelOutputResponse(    TypeConverter.MessageId.ConvertTo( this.Id ),
                                                    TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                                    TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                                    TypeConverter.ConvertToDataObject<TaskCancelOutputResponseTask, TaskCancelOutputResponseTaskDataContract>( this.Task )    );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( TaskCancelOutputResponseEnvelopeDataContract );
        }
    }
}
