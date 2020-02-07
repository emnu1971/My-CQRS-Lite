using System;

namespace CQRSlite.Commands
{
    /// <summary>
    /// Base class for creating commands.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        #region Public Interface

        public Guid Id { get; set; }
        public int ExpectedVersion { get; set; }

        #endregion Public Interface
    }
}
