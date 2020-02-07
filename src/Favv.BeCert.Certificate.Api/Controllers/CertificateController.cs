using CQRSlite.Commands;
using Favv.BeCert.Certificate.Domain.WriteModel.Commands;
using Favv.BeCert.Certificate.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Favv.BeCert.Certificate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        #region Private Storage

        private readonly ICommandSender _commandSender;
        private readonly IHostingEnvironment _hostingEnvironment;

        #endregion Private Storage

        #region C'tor

        public CertificateController(ICommandSender commandSender, IHostingEnvironment hostingEnvironment)
        {
            try
            {
                _commandSender = commandSender ?? 
                    throw new ArgumentNullException(nameof(commandSender));

                _hostingEnvironment = hostingEnvironment ??
                   throw new ArgumentNullException(nameof(hostingEnvironment));
            }
            catch (Exception ex)
            {
                Log.Information($"Error Occured Inside {nameof(CertificateController)} ctor, Error: {ex.Message}");
                throw ex;
            }
        }

        #endregion C'tor

        #region Public Interface

        /// <summary>
        /// Post a new Certificate
        /// </summary>
        /// <param name="request">the certificate creation request</param>
        /// <returns>OK</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCertificateAsync([FromBody] CreateCertificateItemRequestDto request)
        {
            try
            {

                var command =   new CreateCertificateItemCommand
                    (   Guid.NewGuid(), 
                        request.EnterpriseNumber, 
                        request.ProductCategory, 
                        request.Product, 
                        request.Country
                    );

                Log.Information($"Created Command of type [{command.GetType().ToString()}] with values: {command.ToString()}");

                await _commandSender.SendAsync(command);

                Log.Information($"Send Command of type [{command.GetType().ToString()}] with values: {command.ToString()}");

                return Ok();
            }
            catch(Exception ex)
            {
                Log.Error($"Error Occured while executing: {nameof(CreateCertificateAsync)} Error: {ex.Message}");
                return this.Content(ex.Message);
            }
            
        }

        [HttpPost("deliver")]
        public async Task<IActionResult> DeliverCertificateAsync([FromBody] DeliverCertificateItemRequestDto request)
        {
            return null;
        }

        #endregion Public Interface

    }
}
