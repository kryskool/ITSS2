namespace Reth.Protocols.Transfer
{
    public interface IRemoteMessageClient:IMessageClient
    {
        void Start();
        void Terminate();
    }
}