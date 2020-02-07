using CSharpFunctionalExtensions;
using Favv.BeCert.Certificate.Dto;
using Favv.BeCert.Web.Interfaces;
using Favv.BeCert.Web.Services.Api;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Favv.BeCert.Web.ViewModels
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Viewmodel for creation of a new certificate
    /// </summary>
    public partial class CreateCertificateRequestViewModel : IViewModel
    {
        #region Private Storage

        [Required]
        [MaxLength(50)]
        public string EnterpriseNumber { get; set; }

        [Required]
        [MaxLength(25)]
        public string ProductCategory { get; set; }

        [Required]
        [MaxLength(75)]
        public string Product { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        #endregion Private Storage

        #region Ctor

        public CreateCertificateRequestViewModel()
        {

        }

        #endregion Ctor

        #region Private Interface

        public Task LoadAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveAsync()
        {
            // map viewmodel to dto
            var createCertificateRequestDto = new CreateCertificateItemRequestDto()
            {
                EnterpriseNumber = this.EnterpriseNumber,
                ProductCategory = this.ProductCategory,
                Product = this.Product,
                Country = this.Country
            };

            // send dto to application api service layer
            Result result = CertificateApiClient.RequestNewCertificateAsync(createCertificateRequestDto)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            if (result.IsFailure)
            {
                throw new System.Exception(result.Error);
            }

            await Task.FromResult(Task.CompletedTask);
        }

        public Task ValidateAsync()
        {
            throw new System.NotImplementedException();
        }

        #endregion Private Interface
    }
}
