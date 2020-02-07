using System;

namespace Favv.BeCert.Certificate.Domain.WriteModel.Commands
{
    /// <summary>
    /// Command to create an certificate.
    /// </summary>
    public class CreateCertificateItemCommand : Command
    {
        #region Ctor

        public CreateCertificateItemCommand()
        {

        }

        public CreateCertificateItemCommand(
            Guid id,
            int expectedVersion,
            string enterpriseNumber,
            string productCategory,
            string product,
            string country
            ) : this(id,enterpriseNumber,productCategory,product,country)
        {
            ExpectedVersion = expectedVersion;
        }

        public CreateCertificateItemCommand(
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

        #endregion Ctor

        #region Public Interface

        public string EnterpriseNumber { get; private set; }

        public string ProductCategory { get; private set; }

        public string Product { get; private set; }

        public string Country { get; private set; }

        public override string ToString()
        {
            return $"EnterpriseNumber:{EnterpriseNumber}|ProductCategory:{ProductCategory}|Product:{Product}|Country:{Country}";
        }

        #endregion Public Interface
    }
}
