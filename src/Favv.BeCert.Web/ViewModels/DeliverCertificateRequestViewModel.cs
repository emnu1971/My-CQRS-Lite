using CSharpFunctionalExtensions;
using Favv.BeCert.Certificate.Common;
using Favv.BeCert.Certificate.Dto;
using Favv.BeCert.Web.Interfaces;
using Favv.BeCert.Web.Services.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Favv.BeCert.Web.ViewModels
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 06-2019
    /// Purpose     : Viewmodel for delivery of an existing certifcate
    /// </summary>
    public class DeliverCertificateRequestViewModel : IViewModel
    {
        
        #region Private Storage

        [Required]
        public Guid CertificateId { get; set; }

        [Required]
        public CertificateStatus Status { get; set; }

        #endregion Private Storage

        #region IViewModel Implementation

        public Task LoadAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            // map viewmodel to dto
            var deliverCertificateRequestDto = new DeliverCertificateItemRequestDto()
            {
                CertificateId = this.CertificateId,
                Status = this.Status
            };

            // send dto to application api service layer
            Result result = CertificateApiClient.DeliverCertificateAsync(deliverCertificateRequestDto)
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
            throw new NotImplementedException();
        }

        #endregion IViewModel Implementation
    }
}
