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
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Workflows.Standard.Messages.StockInfo.Active
{
    public interface IStockInfoWorkflow:IWorkflow
    {
        event EventHandler<MessageReceivedEventArgs<StockInfoMessage>>? StockChanged;

        IStockInfoFinishedProcessState StartProcess();

        IStockInfoFinishedProcessState StartProcess(    bool? includePacks,
                                                        bool? includeArticleDetails,
                                                        IEnumerable<StockInfoRequestCriteria>? criterias );
                
        Task<IStockInfoFinishedProcessState> StartProcessAsync( CancellationToken cancellationToken = default );

        Task<IStockInfoFinishedProcessState> StartProcessAsync( bool? includePacks,
                                                                bool? includeArticleDetails,
                                                                IEnumerable<StockInfoRequestCriteria>? criterias,
                                                                CancellationToken cancellationToken = default   );
    }
}
