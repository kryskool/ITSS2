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
    public class TaskCancelOutputRequestTask:IEquatable<TaskCancelOutputRequestTask>
    {
        public static bool operator==( TaskCancelOutputRequestTask? left, TaskCancelOutputRequestTask? right )
		{
            return TaskCancelOutputRequestTask.Equals( left, right );
		}
		
		public static bool operator!=( TaskCancelOutputRequestTask? left, TaskCancelOutputRequestTask? right )
		{
			return !( TaskCancelOutputRequestTask.Equals( left, right ) );
		}

        public static bool Equals( TaskCancelOutputRequestTask? left, TaskCancelOutputRequestTask? right )
		{
            return MessageId.Equals( left?.Id, right?.Id );
		}

        public TaskCancelOutputRequestTask( MessageId id )
        {
            this.Id = id;
        }

        public MessageId Id
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as TaskCancelOutputRequestTask );
		}
		
        public bool Equals( TaskCancelOutputRequestTask? other )
		{
            return TaskCancelOutputRequestTask.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String? ToString()
        {
            return this.Id.ToString();
        }
    }
}
