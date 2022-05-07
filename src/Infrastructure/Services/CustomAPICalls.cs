using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infrastructure.Services
{
    public class CustomAPICalls<T>: IAPICalls<T> where T: class
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomAPICalls(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<IEnumerable<T>?> GetAll(string url)
        {
            IEnumerable<T>? result = null;
            
            //_logger.LogInformation($"REQUEST :- {url}");
            
            var httpClient = _httpClientFactory.CreateClient("LeadBase");
           // httpClient.DefaultRequestHeaders.Add("X-Api-Key", key);
            
            var httpResponseMessage = await httpClient.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                await using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();
            
                result = await JsonSerializer.DeserializeAsync
                    <IEnumerable<T>>(contentStream).ConfigureAwait(false);
            }

            //_logger.LogInformation($"RESPONSE :- {JsonConvert.SerializeObject(result)}");
            return result;
        }
    }
}