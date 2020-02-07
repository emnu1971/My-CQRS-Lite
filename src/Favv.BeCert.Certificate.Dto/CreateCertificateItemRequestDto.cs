using System;
using System.ComponentModel.DataAnnotations;

namespace Favv.BeCert.Certificate.Dto
{

    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Datacontract used from the ui layer to application services to request a new certificate.
    /// </summary>
    public class CreateCertificateItemRequestDto
    {
        public string EnterpriseNumber { get; set; }

        public string ProductCategory { get; set; }

        public string Product { get; set; }

        public string Country { get; set; }

    }
}
