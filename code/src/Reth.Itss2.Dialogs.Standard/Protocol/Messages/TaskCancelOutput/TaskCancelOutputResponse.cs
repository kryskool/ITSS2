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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutput
{
    public class TaskCancelOutputResponse:SubscribedResponse, IEquatable<TaskCancelOutputResponse>
    {
        public static bool operator==( TaskCancelOutputResponse? left, TaskCancelOutputResponse? right )
		{
            return TaskCancelOutputResponse.Equals( left, right );
		}
		
		public static bool operator!=( TaskCancelOutputResponse? left, TaskCancelOutputResponse? right )
		{
			return !( TaskCancelOutputResponse.Equals( left, right ) );
		}

        public static bool Equals( TaskCancelOutputResponse? left, TaskCancelOutputResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? TaskCancelOutputResponseTask.Equals( left?.Task, right?.Task ) : false );

            return result;
		}

        public TaskCancelOutputResponse(    MessageId id,
									        SubscriberId source,
                                            SubscriberId destination,
                                            TaskCancelOutputResponseTask task    )
        :
            base( id, StandardDialogs.TaskCancelOutput, source, destination )
        {
            this.Task = task;
        }

        public TaskCancelOutputResponse(    TaskCancelOutputRequest request,
                                            TaskCancelOutputResponseTask task    )
        :
            base( request )
        {
            this.Task = task;
        }

        public TaskCancelOutputResponseTask Task
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as TaskCancelOutputResponse );
		}
		
        public bool Equals( TaskCancelOutputResponse? other )
		{
            return TaskCancelOutputResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
