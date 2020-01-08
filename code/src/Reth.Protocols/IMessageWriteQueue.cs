namespace Reth.Protocols
{
    public interface IMessageWriteQueue
    {
        bool PostMessage( IMessage message );
    }
}