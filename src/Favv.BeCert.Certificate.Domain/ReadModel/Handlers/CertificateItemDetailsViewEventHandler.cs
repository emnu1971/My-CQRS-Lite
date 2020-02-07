using System;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;
using Favv.BeCert.Certificate.Domain.ReadModel.Events;
using Favv.BeCert.Certificate.Domain.ReadModel.Infrastructure;
using Favv.BeCert.Certificate.Domain.Services;
using Favv.BeCert.Certificate.Dto;
using Favv.Bricks.SharedCore.Services;

namespace Favv.BeCert.Certificate.Domain.ReadModel.Handlers
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Handles the CertificateItemCreatedEvent : add to read-model details projection.
    /// </summary>
    public class CertificateItemDetailsViewEventHandler : ICancellableEventHandler<CertificateItemCreatedEvent>
    {
        #region Storage

        private readonly IInterProcessBus<string> _interProcessBus;

        #endregion Storage

        #region C'tor
        public CertificateItemDetailsViewEventHandler(IInterProcessBus<string> interProcessBus)
        {
            _interProcessBus = interProcessBus;
        }
        #endregion C'tor

        #region ICancellableEventHandler Implementation

        /// <summary>
        /// Adds the created certificate as a 
        /// (denormalized) dto to the read-side database
        /// </summary>
        /// <param name="message">the certificate created event</param>
        /// <param name="token">optional cancelation token</param>
        /// <returns></returns>
        public Task HandleAsync(CertificateItemCreatedEvent message, CancellationToken token = default(CancellationToken))
        {
            // Update the "Read-Model" database (details-projection)
            CertificateInMemoryDatabase.Details.Add(message.Id, 
                new CertificateItemDetailsDto(
                    message.Id, message.EnterpriseNumber, message.ProductCategory, message.Product, message.Country, DateTime.UtcNow, DateTime.UtcNow.AddDays(30)));

            // notify client subscribers that read-model has been updated
            // this method runs async but should not be awaited because we
            // do not expect a result that has to be interpreted, 
            // so we avoid async/await here and method only returns Task instead of Async Task !
            // for more info : https://stackify.com/when-to-use-asynchronous-programming/
            _interProcessBus.SendMessage("CertificateItemCreatedEventDetailsProjection");

            return Task.CompletedTask;
        }

        #endregion ICancellableEventHandler Implementation
    }
}
