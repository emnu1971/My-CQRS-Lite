using CQRSlite.Domain;
using Favv.BeCert.Certificate.Domain.ReadModel.Events;
using System;

namespace Favv.BeCert.Certificate.Domain.WriteModel.Aggregates
{
    /// <summary>
    /// Certificate Aggregate.
    /// </summary>
    public partial class CertificateItem : AggregateRoot
    {
        #region Storage

        private string _enterpriseNumber;
        private string _productCategory;
        private string _product;
        private string _country;

        #endregion Storage

        #region Ctor

        private CertificateItem(){}

        public CertificateItem(
            Guid id,
            int version,
            string enterpriseNumber,
            string productCategory,
            string product,
            string country
            )
        {
            Id = id;
            ApplyChange(new CertificateItemCreatedEvent(id, version, enterpriseNumber,productCategory,product,country));
        }

        #endregion Ctor

        #region Private Interface

        /// <summary>
        /// Apply method for initial creation of a Certificate.
        /// method is dynamically called from AggregateRoot ApplyEvent method
        /// </summary>
        /// <param name="e"></param>
        private void Apply(CertificateItemCreatedEvent e)
        {
            Version = e.Version;
            _enterpriseNumber = e.EnterpriseNumber;
            _productCategory = e.ProductCategory;
            _product = e.Product;
            _country = e.Country;
        }

        #endregion Private Interface
    }
}
