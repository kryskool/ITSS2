using System;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server
{
    public class StorageSystemEventArgs:EventArgs
    {
        public StorageSystemEventArgs( IStorageSystem storageSystem )
        {
            storageSystem.ThrowIfNull();

            this.StorageSystem = storageSystem;
        }

        public IStorageSystem StorageSystem
        {
            get;
        }

        public override String ToString()
        {
            return this.StorageSystem.ToString();
        }
    }
}
