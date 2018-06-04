using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BasketApiClient;
using BasketApiClient.Services.Basket;
using Newtonsoft.Json;

namespace ApiClient
{
    internal class ApiServiceClient : IDisposable
    {
        BasketService _basketService;
        public BasketService BasketService { get { return _basketService ?? (_basketService = new BasketService()); } }

         
        private HttpClient httpClient;

        private readonly JsonSerializerSettings jsonSettings =
    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        public ApiServiceClient(string baseAddress = "")
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
        /// <returns></returns>
        public async Task<string> GetAsync(string requestUri)
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
