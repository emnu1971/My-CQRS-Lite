using CQRSlite.Events;
using System;

namespace Favv.BeCert.Certificate.Domain.ReadModel.Events
{
    /// <summary>
    /// Event published when certificate has been created.
    /// </summary>
    public class CertificateItemCreatedEvent : EventBase
    {
        #region Ctor

        public CertificateItemCreatedEvent()
        {

        }

        public CertificateItemCreatedEvent(
            Guid id,
            int version,
            string enterpriseNumber,
            string productCategory,
            string product,
            string country
            )
        {
            Id = id;
            Version = version;
            EnterpriseNumber = enterpriseNumber;
            ProductCategory = productCategory;
            Product = product;
            Country = country;
        }

        #endregion Ctor

        #region Public Interface

        public string EnterpriseNumber { get; private set; }

        public string ProductCategory { get; private set; }

        public string Product { get; private set; }

        public string Country { get; private set; }

        #endregion Public Interface
    }
}
