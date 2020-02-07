using Favv.Bricks.SharedCore.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Configuration;
using System.Text;

namespace Favv.Bricks.RabbitMQAdapter
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 06-2019
    /// Purpose     : Implementation of InterprocessBus for RabbitMQ.
    /// </summary>
    public class InterProcessBus : IInterProcessBus<string>
    {

        #region storage

        private readonly string _busName;
        private readonly string _connectionString;

        #endregion storage


        #region Ctor
        public InterProcessBus()
        {
            _busName = ApplicationSettings.GetSettings()["rabbitMqSettings:busName"];

            _connectionString = ApplicationSettings.GetSettings()["rabbitMqSettings:rabbitMqHost"];

        }
        #endregion Ctor
        #region IInterProcessBus Implementation

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = _connectionString };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var bytes = Encoding.ASCII.GetBytes(message);
                    channel.ExchangeDeclare(_busName, "fanout");
                    channel.BasicPublish(_busName, string.Empty, null, bytes);
                }
            }
        }
        
        #endregion IInterProcessBus Implementation
    }
}
