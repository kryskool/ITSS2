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

using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.TaskCancelOutput
{
    public class TaskCancelOutputResponseTask:IEquatable<TaskCancelOutputResponseTask>
    {
        public static bool operator==( TaskCancelOutputResponseTask? left, TaskCancelOutputResponseTask? right )
		{
            return TaskCancelOutputResponseTask.Equals( left, right );
		}
		
		public static bool operator!=( TaskCancelOutputResponseTask? left, TaskCancelOutputResponseTask? right )
		{
			return !( TaskCancelOutputResponseTask.Equals( left, right ) );
		}

        public static bool Equals( TaskCancelOutputResponseTask? left, TaskCancelOutputResponseTask? right )
		{
            bool result =  MessageId.Equals( left?.Id, right?.Id );

            result &= ( result ? EqualityComparer<TaskCancelOutputStatus?>.Default.Equals( left?.Status, right?.Status ) : false );

            return result;
		}

        public TaskCancelOutputResponseTask( MessageId id, TaskCancelOutputStatus status )
        {
            this.Id = id;
            this.Status = status;
        }

        public MessageId Id
        {
            get;
        }

        public TaskCancelOutputStatus Status
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as TaskCancelOutputResponseTask );
		}
		
        public bool Equals( TaskCancelOutputResponseTask? other )
		{
            return TaskCancelOutputResponseTask.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String? ToString()
        {
            return $"{ this.Id }, { this.Status }";
        }
    }
}
