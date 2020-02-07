using System.ComponentModel.DataAnnotations;

namespace Favv.BeCert.Certificate.Vm
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Viewmodel used from the ui layer to request a new certificate.
    /// </summary>
    public class CreateCertificateRequestVm
    {
        [Required]
        [MaxLength(50)]
        public string EnterpriseNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductCategory { get; set; }

        [Required]
        [MaxLength(150)]
        public string Product { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
    }
}
