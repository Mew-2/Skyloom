using MyToDo2.Shared;
using MyToDo2.Shared.Dtos;

namespace MyToDo2.Services
{
    public interface ILoginService
    {
        Task<ApiResponse<UserDto>> LoginAsync(UserDto user);

        Task<ApiResponse> RegisterAsync(UserDto user);
    }
}