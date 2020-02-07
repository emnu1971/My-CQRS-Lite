using Favv.Bricks.SharedCore.Services;
using System;

namespace Favv.Bricks.MSMQAdapter
{
    public class BusSubscriber<T> : IInterProcessBusSubscriber<T>, IDisposable
    {

        public IObservable<T> GetEventStream()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
