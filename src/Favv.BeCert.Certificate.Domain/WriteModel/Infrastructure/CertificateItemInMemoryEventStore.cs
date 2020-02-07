using CQRSlite.Events;
using Favv.BeCert.Infrastructure;

namespace Favv.BeCert.Certificate.Domain.WriteModel.Infrastructure
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : In Memory Event Store
    /// </summary>
    public class CertificateItemInMemoryEventStore : BaseInMemoryEventStore
    {
        public CertificateItemInMemoryEventStore(IEventPublisher publisher) : base(publisher)
        {

        }
    }
}
