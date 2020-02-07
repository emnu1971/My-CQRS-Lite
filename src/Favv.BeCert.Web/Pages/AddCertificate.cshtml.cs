using Favv.BeCert.Certificate.Dto;
using Favv.BeCert.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Favv.BeCert.Web
{
    public class AddCertificateModel : PageModel
    {
        #region Private Storage

        [BindProperty]
        public CreateCertificateRequestViewModel CertificateRequestVm { get; set; }
        
        #endregion Private Storage


        #region Public Interface

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await CertificateRequestVm.SaveAsync();

            return await Task.FromResult(RedirectToPage("Index"));
        }

        #endregion Public Interface


    }
}