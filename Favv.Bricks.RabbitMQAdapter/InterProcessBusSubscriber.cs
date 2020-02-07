using System;
using System.Configuration;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Favv.Bricks.SharedCore.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Favv.Bricks.RabbitMQAdapter
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 06-2019
    /// Purpose     : Implementation of a Bus Subscriber for RabbitMQ
    /// </summary>
    public class InterProcessBusSubscriber : IInterProcessBusSubscriber<string>, IDisposable
    {

        #region Private Storage

        private readonly string _busName;
        private readonly string _connectionString;
        private CancellationTokenSource _cancellationToken;
        private Task _workerTask;
        private readonly ISubject<string> _eventsSubject = new Subject<string>();


        #endregion Private Storage

        #region C'tor

        public InterProcessBusSubscriber()
        {
            _busName = ApplicationSettings.GetSettings()["rabbitMqSettings:busName"];

            _connectionString = ApplicationSettings.GetSettings()["rabbitMqSettings:rabbitMqHost"];

            StartMessageListener();
        }

        #endregion C'tor

        #region IInterProcessSubscriber Implementation

        public IObservable<string> GetEventStream()
        {
            return _eventsSubject.AsObservable();
        }

        #endregion IInterProcessSubscriber Implementation

        #region Private Interface

        private void StartMessageListener()
        {
            _cancellationToken = new CancellationTokenSource();
            _workerTask = Task.Factory.StartNew(() => ListenForMessage(), _cancellationToken.Token);
        }

        private void ListenForMessage()
        {
            var factory = new ConnectionFactory() { HostName = _connectionString };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(_busName, "fanout");

                    bool durable = true;
                    bool exclusive = false;
                    bool autoDelete = false;

                    var queue = channel.QueueDeclare(
                        Assembly.GetEntryAssembly().GetName().Name,
                        durable, exclusive, autoDelete, null);
                    channel.QueueBind(queue.QueueName, _busName, string.Empty);
                    var consumer = new QueueingBasicConsumer(channel);

                    channel.BasicConsume(queue.QueueName, false, string.Empty, consumer);

                    while (true)
                    {
                        if (_cancellationToken.IsCancellationRequested)
                            break;
                        BasicDeliverEventArgs ea;
                        consumer.Queue.Dequeue(10, out ea);

                        if (ea == null)
                            continue;

                        var message = Encoding.ASCII.GetString(ea.Body);
                        Task.Run(async () =>
                        {
                            await Task.Run(() =>
                            {
                                _eventsSubject.OnNext(message);
                            });
                        });
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                }
            }
        }

        private void CancelWorkerTask()
        {
            if (_workerTask == null) return;
            _cancellationToken.Cancel();
            _workerTask.Wait();
            _workerTask.Dispose();
        }

        #endregion Private Interface

        #region IDisposable Implementation

        public void Dispose()
        {
            CancelWorkerTask();
        }

        #endregion IDisposable Implementation
    }
}
