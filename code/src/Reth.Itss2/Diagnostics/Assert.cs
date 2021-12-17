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
using System.Diagnostics;

namespace Reth.Itss2.Diagnostics
{
    public static class Assert
    {
        internal static void SetupForTestEnvironment()
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.assert?f1url=%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Diagnostics.Debug.Assert);k(DevLang-csharp)%26rd%3Dtrue&view=net-5.0
            Trace.Listeners.Clear();
        }

        public static Exception Exception( Exception exception )
        {
            ExecutionLogProvider.Log.LogError( exception );

            Debug.Assert( false, exception.Message );

            return exception;
        }
    }
}
