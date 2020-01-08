namespace Reth.Protocols
{
    public enum UnhandledReason
    {
        Cancelled,
        NotDispatched,
        NotProcessed,
        Unsupported,
        InvalidFormat,
        TooLarge,
        Shutdown,
        Timeout,
        ConnectionError,
        UnknownError
    }
}