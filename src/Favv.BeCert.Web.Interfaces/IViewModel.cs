using System.Threading.Tasks;

namespace Favv.BeCert.Web.Interfaces
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Inteface class for ViewModel
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Load ViewModel
        /// </summary>
        /// <returns></returns>
        Task LoadAsync();

        /// <summary>
        /// Save ViewModel
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        /// <summary>
        /// Validate ViewModel
        /// </summary>
        /// <returns></returns>
        Task ValidateAsync();
    }
}
