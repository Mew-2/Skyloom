using MyToDo2.Shared;
using MyToDo2.Shared.Dtos;

namespace MyToDo2.Api.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(string account, string password);

        Task<ApiResponse> RegisterAsync(UserDto user);
    }
}