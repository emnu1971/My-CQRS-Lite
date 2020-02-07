using System;

namespace Favv.BeCert.Certificate.Dto
{

    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Datacontract used to sync with the certificate domain read model (list projection).
    ///               This projection only contains a subset of certificate for building read-model list
    ///               with certificate enterprisenumber productcategory product and country
    /// </summary>
    public class CertificateItemListDto
    {
        #region Public Properties

        public Guid Id { get; private set; }

        public string EnterpriseNumber { get; private set; }

        public string ProductCategory { get; private set; }

        public string Product { get; private set; }

        public string Country { get; private set; }

        #endregion Public Properties

        #region C'tor

        public CertificateItemListDto(
            Guid id, 
            string enterpriseNumber, 
            string productCategory,
            string product,
            string country)
        {
            Id = id;
            EnterpriseNumber = enterpriseNumber;
            ProductCategory = productCategory;
            Product = product;
            Country = country;
        }

        #endregion C'tor
    }
}
