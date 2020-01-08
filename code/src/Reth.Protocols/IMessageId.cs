using System;

namespace Reth.Protocols
{
    public interface IMessageId:IComparable<IMessageId>, IEquatable<IMessageId>
    {
        String Value{ get; }
    }
}
