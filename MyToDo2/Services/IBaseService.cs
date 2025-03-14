using MyToDo2.Shared;
using MyToDo2.Shared.Collections;
using MyToDo2.Shared.Parameters;

namespace MyToDo2.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<ApiResponse<T>> AddAsync(T entity);

        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse<T>> UpdateAsync(T entity);

        Task<ApiResponse<T>> GetAsync(int id);

        Task<ApiResponse<PagedList<T>>> GetAllAsync(QueryParameter parameter);
    }
}