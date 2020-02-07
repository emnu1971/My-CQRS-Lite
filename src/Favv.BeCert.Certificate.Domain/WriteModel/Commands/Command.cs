using CQRSlite.Commands;
using System.Runtime.Serialization;

namespace Favv.BeCert.Certificate.Domain.WriteModel.Commands
{
    /// <summary>
    /// Base class for creating commands in the Certificate Domain Write Model.
    /// </summary>
    //[KnownType(typeof(CreateCertificateItemCommand))]
    public abstract class Command : CommandBase
    {
    }
}
