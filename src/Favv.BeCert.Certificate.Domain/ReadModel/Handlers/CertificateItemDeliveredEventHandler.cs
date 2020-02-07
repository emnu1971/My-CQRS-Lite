using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;
using Favv.BeCert.Certificate.Domain.ReadModel.Events;
using Favv.Bricks.SharedCore.Services;

namespace Favv.BeCert.Certificate.Domain.ReadModel.Handlers
{
    public class CertificateItemDeliveredEventHandler : ICancellableEventHandler<CertificateItemDeliveredEvent>
    {
        #region Storage
        private readonly IInterProcessBus<string> _interProcessBus;
        #endregion Storage

        #region C'tor
        public CertificateItemDeliveredEventHandler(IInterProcessBus<string> interProcessBus)
        {
            _interProcessBus = interProcessBus;
        }
        #endregion C'tor

        #region ICancellableEventHandler Implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task HandleAsync(CertificateItemDeliveredEvent message, CancellationToken token = default(CancellationToken))
        {
            //Update the Read database (code ommited here ...)
            // <Read-Model update code comes here ...>

            // notify client subscribers that read-model has been updated
            // this method runs async but should not be awaited because we
            // do not expect a result that has to be interpreted, 
            // so we avoid async/await here and method only returns Task instead of Async Task !
            // for more info : https://stackify.com/when-to-use-asynchronous-programming/
            _interProcessBus.SendMessage("CertificateItemDeliveredMessage");

            return Task.CompletedTask;
        }
        #endregion ICancellableEventHandler Implementation
    }
}
