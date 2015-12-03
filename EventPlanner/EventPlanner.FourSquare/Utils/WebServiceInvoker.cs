using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EventPlanner.FourSquare.Utils
{
    class WebServiceInvoker<T> where T : class
    {
        private HttpClient _httpClient;
        private Uri _serviceUri;

        /// <summary>
        /// Create and initialize object to handle WebService invoking
        /// </summary>
        /// <param name="httpClient">Client to communicate through</param>
        /// <param name="serviceUri">Uri of the request</param>
        /// <returns>Initialized WebServiceInvoker object</returns>
        public static WebServiceInvoker<T> Create(HttpClient httpClient, Uri serviceUri)
        {
            var invoker = new WebServiceInvoker<T> {_httpClient = httpClient};
            invoker._httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            invoker._serviceUri = serviceUri;

            return invoker;
        }

        /// <summary>
        /// Asynchronously invoke WebService.
        /// </summary>
        /// <returns>Deserialized call result</returns>
        public async Task<T> InvokeWebService()
        {
            _httpClient.CancelPendingRequests();
            var callResult = await _httpClient.GetStringAsync(_serviceUri);
            return JsonConvert.DeserializeObject<T>(callResult);
        }
    }
}
