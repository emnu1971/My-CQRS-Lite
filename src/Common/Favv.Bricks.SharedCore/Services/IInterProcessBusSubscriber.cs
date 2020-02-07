using System;

namespace Favv.Bricks.SharedCore.Services
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 06-2019
    /// Purpose     : InterProcessBus Subscriber interface. 
    ///               Used to subscribe to messages published on the Inter Process Bus.
    /// </summary>
    public interface IInterProcessBusSubscriber<T>
    {
        IObservable<T> GetEventStream();
    }
}
