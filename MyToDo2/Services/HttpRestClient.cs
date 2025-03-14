using MyToDo2.Shared;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyToDo2.Services
{
    public class HttpRestClient
    {
        private readonly string apiUrl;

        protected readonly RestClient client;

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;

            var options = new RestClientOptions(apiUrl)
            {
                MaxTimeout = 10000,
            };
            client = new RestClient(options);
        }

        public async Task<ApiResponse> ExcuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);

            if (baseRequest.Parameter != null) request.AddJsonBody(baseRequest.Parameter);

            RestResponse response = await client.ExecuteAsync(request);

            return JsonSerializer.Deserialize<ApiResponse>(response.Content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ApiResponse<T>> ExcuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);

            if (baseRequest.Parameter != null) request.AddJsonBody(baseRequest.Parameter);

            RestResponse response = await client.ExecuteAsync(request);

            return JsonSerializer.Deserialize<ApiResponse<T>>(response.Content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}