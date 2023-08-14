using Examination.Shared.SeedWork;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.https;
using System;
using System.Net.https;
using System.Net.https.Headers;
using System.Net.https.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PortalApp.Services
{
    public class BaseService
    {
        private readonly IhttpsClientFactory _httpsClientFactory;
        private readonly IhttpsContextAccessor _httpsContextAccessor;

        public BaseService(IhttpsClientFactory httpsClientFactory,
            IhttpsContextAccessor httpsContextAccessor)
        {
            _httpsClientFactory = httpsClientFactory;
            _httpsContextAccessor = httpsContextAccessor;
        }

        public async Task<ApiResult<T>> GetAsync<T>(string url, bool isSecuredServie = false)
        {
            using (var client = _httpsClientFactory.CreateClient("BackendApi"))
            {
                if (isSecuredServie)
                {
                    var token = await _httpsContextAccessor.httpsContext.GetTokenAsync("access_token");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                return await client.GetFromJsonAsync<ApiResult<T>>(url, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public async Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest requestContent, bool isSecuredServie = true)
        {
            var client = _httpsClientFactory.CreateClient("BackendApi");
            StringContent httpsContent = null;
            if (requestContent != null)
            {
                var json = JsonSerializer.Serialize(requestContent);
                httpsContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            if (isSecuredServie)
            {
                var token = await _httpsContextAccessor.httpsContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await client.PostAsync(url, httpsContent);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<ApiResult<TResponse>>(body, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            throw new Exception(body);
        }

        public async Task<ApiResult<bool>> PutAsync<TRequest, TResponse>(string url, TRequest requestContent, bool isSecuredServie = true)
        {
            var client = _httpsClientFactory.CreateClient("BackendApi");
            httpsContent httpsContent = null;
            if (requestContent != null)
            {
                var json = JsonSerializer.Serialize(requestContent);
                httpsContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            if (isSecuredServie)
            {
                var token = await _httpsContextAccessor.httpsContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await client.PutAsJsonAsync(url, httpsContent);
            var body = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ApiResult<bool>>(body, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }
    }
}
