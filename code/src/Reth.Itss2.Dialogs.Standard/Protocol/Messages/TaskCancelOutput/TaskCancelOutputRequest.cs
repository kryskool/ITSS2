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

using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutput
{
    public class TaskCancelOutputRequest:SubscribedRequest, IEquatable<TaskCancelOutputRequest>
    {
        public static bool operator==( TaskCancelOutputRequest? left, TaskCancelOutputRequest? right )
		{
            return TaskCancelOutputRequest.Equals( left, right );
		}
		
		public static bool operator!=( TaskCancelOutputRequest? left, TaskCancelOutputRequest? right )
		{
			return !( TaskCancelOutputRequest.Equals( left, right ) );
		}

        public static bool Equals( TaskCancelOutputRequest? left, TaskCancelOutputRequest? right )
		{
            bool result = SubscribedRequest.Equals( left, right );

            result &= ( result ? TaskCancelOutputRequestTask.Equals( left?.Task, right?.Task ) : false );

            return result;
		}

        public TaskCancelOutputRequest( MessageId id,
									    SubscriberId source,
                                        SubscriberId destination,
                                        TaskCancelOutputRequestTask task    )
        :
            base( id, StandardDialogs.TaskCancelOutput, source, destination )
        {
            this.Task = task;
        }

        public TaskCancelOutputRequestTask Task
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as TaskCancelOutputRequest );
		}
		
        public bool Equals( TaskCancelOutputRequest? other )
		{
            return TaskCancelOutputRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
