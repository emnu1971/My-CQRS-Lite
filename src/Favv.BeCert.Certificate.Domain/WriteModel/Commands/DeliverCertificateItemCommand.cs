using Favv.BeCert.Certificate.Common;
using System;

namespace Favv.BeCert.Certificate.Domain.WriteModel.Commands
{
    /// <summary>
    /// Command used to change status of certificate to delivered.
    /// </summary>
    public class DeliverCertificateItemCommand : Command
    {
        #region Ctor

        public DeliverCertificateItemCommand()
        {

        }
        public DeliverCertificateItemCommand(
            Guid id,
            CertificateStatus status
            )
        {
            Id = id;
            Status = status;
        }

        #endregion

        #region Public Interface

        public CertificateStatus Status { get; set; }

        public override string ToString()
        {
            return $"Certificate-Id: {Id}|Status:{Status}";
        }

        #endregion Public Interface
    }
}
