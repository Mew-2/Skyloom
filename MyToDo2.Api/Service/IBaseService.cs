using MyToDo2.Shared;
using MyToDo2.Shared.Parameters;

namespace MyToDo2.Api.Service
{
    public interface IBaseService<T, U>
    {
        Task<ApiResponse> GetAllAsync(U parameter);

        Task<ApiResponse> GetSingleAsync(int id);

        Task<ApiResponse> AddAsync(T model);

        Task<ApiResponse> UpdateAsync(T model);

        Task<ApiResponse> DeleteAsync(int id);
    }
}