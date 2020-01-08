namespace Reth.Protocols
{
    public interface IMessageDispatcher
    {
        void Dispatch<T>( T message )where T:IMessage;
    }
}