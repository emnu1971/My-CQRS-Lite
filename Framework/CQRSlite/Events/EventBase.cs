using System;

namespace CQRSlite.Events
{

    /// <summary>
    /// Base class for creating domain events.
    /// </summary>
    public abstract class EventBase : IEvent
    {
        #region Public Interface

        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        #endregion Public Interface
    }
}
