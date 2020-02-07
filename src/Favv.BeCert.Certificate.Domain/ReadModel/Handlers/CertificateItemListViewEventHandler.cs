using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;
using Favv.BeCert.Certificate.Domain.ReadModel.Events;
using Favv.BeCert.Certificate.Domain.ReadModel.Infrastructure;
using Favv.BeCert.Certificate.Dto;
using Favv.Bricks.SharedCore.Services;

namespace Favv.BeCert.Certificate.Domain.ReadModel.Handlers
{

    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Handles the CertificateItemCreatedEvent: add to read-model list projection.
    /// </summary>
    public class CertificateItemListViewEventHandler : ICancellableEventHandler<CertificateItemCreatedEvent>
    {
        #region Storage

        private readonly IInterProcessBus<string> _interProcessBus;

        #endregion Storage

        #region Ctor

        public CertificateItemListViewEventHandler(IInterProcessBus<string> interProcessBus)
        {
            _interProcessBus = interProcessBus;
        }

        #endregion Ctor

        #region ICancellableEventHandler Implementation
        /// <summary>
        /// Adds the created certificate item as a
        /// (denormalized) dto to the read-side database
        /// </summary>
        /// <param name="message">the certificate created event</param>
        /// <param name="token">optional cancelation token</param>
        /// <returns></returns>
        public Task HandleAsync(CertificateItemCreatedEvent message, CancellationToken token = default(CancellationToken))
        {

            // Update the "Read-Model" database (list-projection)
            CertificateInMemoryDatabase.List.Add(new CertificateItemListDto(message.Id, message.EnterpriseNumber, message.ProductCategory, message.Product, message.Country));

            // notify client subscribers that read-model has been updated
            // this method runs async but should not be awaited because we
            // do not expect a result that has to be interpreted, 
            // so we avoid async/await here and method only returns Task instead of Async Task !
            // for more info : https://stackify.com/when-to-use-asynchronous-programming/
            _interProcessBus.SendMessage("CertificateItemCreatedEventListProjection");

            return Task.CompletedTask;
        }

        #endregion ICancellableEventHandler Implementation
    }
}
