using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Favv.BeCert.Certificate.Domain.WriteModel.Aggregates;
using Favv.BeCert.Certificate.Domain.WriteModel.Commands;

namespace Favv.BeCert.Certificate.Domain.WriteModel.Handlers
{
    /// <summary>
    /// Handles certificate commands.
    /// </summary>
    public class CertificateItemCommandHandlers : ICommandHandler<CreateCertificateItemCommand>
    {
        #region Private Storage

        private readonly ISession _session;

        #endregion Private Storage

        #region Ctor
        public CertificateItemCommandHandlers(ISession session)
        {
            _session = session;
        }

        #endregion Ctor

        #region Public Interface

        #region ICommandHandler<TCommand> Implementation

        public async Task HandleAsync(CreateCertificateItemCommand message)
        {
            var item = new CertificateItem(
                message.Id, 
                message.ExpectedVersion, 
                message.EnterpriseNumber,
                message.ProductCategory,
                message.Product,
                message.Country);

            await _session.AddAsync(item);
            await _session.CommitAsync();
        }

        #endregion ICommandHandler<TCommand> Implementation

        #endregion Public Interface
    }
}
