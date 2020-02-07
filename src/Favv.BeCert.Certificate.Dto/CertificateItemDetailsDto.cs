using System;

namespace Favv.BeCert.Certificate.Dto
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Datacontract used to sync with the certificate domain read model (details projection).
    ///               The details projecten includes the from and until validity dates.
    /// </summary>
    public class CertificateItemDetailsDto
    {
        #region Public Properties

        public Guid Id { get; private set; }

        public string EnterpriseNumber { get; private set; }

        public string ProductCategory { get; private set; }

        public string Product { get; private set; }

        public string Country { get; private set; }

        public DateTime ValidFrom { get; private set; }

        public DateTime ValidUntil { get; private set; }

        #endregion Public Properties

        #region C'tor

        public CertificateItemDetailsDto(
            Guid id,
            string enterpriseNumber,
            string productCategory,
            string product,
            string country,
            DateTime validFrom,
            DateTime validUntil)
        {
            Id = id;
            EnterpriseNumber = enterpriseNumber;
            ProductCategory = productCategory;
            Product = product;
            Country = country;
            ValidFrom = validFrom;
            ValidUntil = validUntil;
        }

        #endregion C'tor
    }
}
