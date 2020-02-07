using Favv.Bricks.SharedCore.Services;
using System;

namespace Favv.Bricks.MSMQAdapter
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 06-2019
    /// Purpose     : Implementation of InterprocessBus for Microsoft MQ.
    /// </summary>
    public class InterProcessBus : IInterProcessBus<string>
    {
        #region Storage

        #endregion Storage

        #region C'tor

        #endregion C'tor

        #region IInterProcessBus Implementation

        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        #endregion IInterProcessBus Implementation

    }
}
