namespace Reth.Protocols.Transfer
{
    public interface ILocalMessageClient:IMessageClient
    {
        void Connect();
        void Disconnect();
    }
}
