using CQRSlite.Messages;
using System;

namespace CQRSlite.Events
{
    /// <summary>
    /// Defines an event with required fields.
    /// </summary>
    public interface IEvent : IMessage
    {
       
        int Version { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}
