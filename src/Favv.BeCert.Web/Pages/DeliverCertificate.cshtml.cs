using Favv.BeCert.Certificate.Common;
using Favv.BeCert.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Favv.BeCert.Web.Pages
{
    public class DeliverCertificateModel : PageModel
    {
        [BindProperty]
        public DeliverCertificateRequestViewModel CertificateDeliverVm { get; set; }

        public void OnGet()
        {
            CertificateDeliverVm = new DeliverCertificateRequestViewModel();
            CertificateDeliverVm.CertificateId = Guid.NewGuid();
            CertificateDeliverVm.Status = CertificateStatus.Delivered;
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await CertificateDeliverVm.SaveAsync();

            return await Task.FromResult(RedirectToPage("Index"));

        }
    }
}