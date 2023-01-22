using Core.ServeHttp.Interface;
using General.Models.Http;

namespace Core.ServeHttp.Service
{
    public class DefaultHttpService : IDefaultHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAsync(string clientName, BaseHttpRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient(clientName);

            if (request.Headers?.Count > 0)
            {
                foreach (var header in request.Headers)
                {
                    if (httpClient.DefaultRequestHeaders.Contains(header.Key))
                        httpClient.DefaultRequestHeaders.Remove(header.Key);

                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            if (string.IsNullOrEmpty(request.BaseAddress))
                httpClient.BaseAddress = new Uri(request.BaseAddress);


            return await httpClient.GetAsync(request.Url);
        }
    }
}
