using MyToDo2.Shared;
using MyToDo2.Shared.Dtos;

namespace MyToDo2.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName = "Login";

        public LoginService(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ApiResponse<UserDto>> LoginAsync(UserDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"/api/{serviceName}/Login";
            request.Parameter = user;
            return await client.ExcuteAsync<UserDto>(request);
        }

        public async Task<ApiResponse> RegisterAsync(UserDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"/api/{serviceName}/Register";
            request.Parameter = user;
            return await client.ExcuteAsync(request);
        }
    }
}