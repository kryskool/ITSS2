namespace Reth.Protocols.Serialization.Xml.Parsing
{
    internal enum MessageExtractorState
    {
        OutOfMessage,
        WithinComment,
        WithinData,
        WithinMessage
    }
}
