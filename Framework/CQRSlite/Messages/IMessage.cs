using System;

namespace CQRSlite.Messages
{
    /// <summary>
    /// Marker interface for a message
    /// </summary>
    public interface IMessage
    {
        Guid Id { get; set; }
    }
}