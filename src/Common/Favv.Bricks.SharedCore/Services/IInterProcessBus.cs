namespace Favv.Bricks.SharedCore.Services
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 06-2019
    /// Purpose     : Inter process bus interface.
    ///               Used to send loosely coupled messages between different domain context.
    /// </summary>
    public interface IInterProcessBus<T>
    {
        void SendMessage(T message);
    }
}
