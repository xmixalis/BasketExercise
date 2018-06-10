using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BasketApi.Client.Exceptions;
using Newtonsoft.Json;

namespace BasketApi.Client.Helpers
{
    /// <summary>
    /// Client object that is used for all the requests to the server with the Basket API
    /// </summary>
    internal class ApiHttpClient : IDisposable
    {
        private HttpClient httpClient;

        private readonly JsonSerializerSettings jsonSettings =
    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        /// <summary>
        /// Constructor of the HTTP client
        /// </summary>
        /// <param name="baseAddress">Base address of the API</param>
        public ApiHttpClient(string baseAddress)
        {
            httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.Timeout = new TimeSpan(0, 20, 0);
        }

        /// <summary>
        /// Submits a POST request to the specified URI within the Base Address
        /// </summary>
        /// <param name="requestUri">URI for POST request.</param>
        /// <param name="data">Data to serialize as JSON in the request body.</param>
        /// <returns></returns>
        public async Task<T> PostAsJsonAsync<T>(string requestUri, object data)
        {
            var json = JsonConvert.SerializeObject(data, jsonSettings);
            HttpResponseMessage response =
                await httpClient.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                string error = response.Headers.Contains("error") ?
                    string.Join(". ", response.Headers.GetValues("error")) : null;

                throw new ApiResponseException(response.StatusCode, error);
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        /// <summary>
        /// Submits a GET request to the specified URI within the Base Address
        /// </summary>
        /// <param name="requestUri">URI for GET request.</param>
        /// <returns>String JSON response</returns>
        public async Task<string> GetAsStringAsync(string requestUri)
        {
            HttpResponseMessage response = httpClient.GetAsync(requestUri).Result;

            if (!response.IsSuccessStatusCode)
            {
                string error = response.Headers.Contains("error") ?
                    string.Join(". ", response.Headers.GetValues("error")) : null;

                throw new ApiResponseException(response.StatusCode, error);
            }
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Submits a GET request to the specified URI within the Base Address
        /// </summary>
        /// <param name="requestUri">URI for GET request.</param>
        /// <returns>Object of the specified type</returns>
        public async Task<T> GetAsync<T>(string requestUri)
        {
            string content = await GetAsStringAsync(requestUri);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public void Dispose()
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }
        }
    }
}
