using CSharpFunctionalExtensions;
using Favv.BeCert.Certificate.Dto;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Favv.BeCert.Web.Services.Api
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Certificate Api client
    /// </summary>
    public static class CertificateApiClient
    {
        #region Private Storage

        private static readonly HttpClient _client = new HttpClient();
        private static string _endpointUrl;

        #endregion Private Storage

        #region Public Interface

        public static void Init(string endpointUrl)
        {
            _endpointUrl = endpointUrl;
        }

        #region Api Method Calls

        public static async Task<Result> RequestNewCertificateAsync(CreateCertificateItemRequestDto dto)
        {
            Result result = await SendRequest<string>("", HttpMethod.Post, dto).ConfigureAwait(false);
            return result;
        }

        public static async Task<Result> DeliverCertificateAsync(DeliverCertificateItemRequestDto dto)
        {
            Result result = await SendRequest<string>("deliver", HttpMethod.Post, dto).ConfigureAwait(false);
            return result;
        }

        #endregion Api Method Calls

        #endregion Public Interface

        #region Private Interface

        private static async Task<Result<T>> SendRequest<T>(string url, HttpMethod method, object content = null)
             where T : class
        {
            var request = new HttpRequestMessage(method, $"{_endpointUrl}/{url}");
            if (content != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage message = await _client.SendAsync(request).ConfigureAwait(false);
            string response = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            var envelope = JsonConvert.DeserializeObject<Envelope<T>>(response);

            if (message.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception(envelope.ErrorMessage);

            if (!message.IsSuccessStatusCode)
                return Result.Fail<T>(envelope.ErrorMessage);

            T result = envelope == null ? null : envelope.Result;

            if (result == null && typeof(T) == typeof(string))
            {
                result = string.Empty as T;
            }

            return Result.Ok(result);
        }

        #endregion Private Interface
    }

    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 05-2019
    /// Purpose     : Message Envelope
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Envelope<T>
    {
        public T Result { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime TimeGenerated { get; set; }
    }
}
