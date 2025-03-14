using MyToDo2.Shared.Collections;
using MyToDo2.Shared;
using MyToDo2.Shared.Dtos;
using MyToDo2.Shared.Parameters;

namespace MyToDo2.Services
{
    public interface IToDoService : IBaseService<ToDoDto>
    {
        Task<ApiResponse<PagedList<ToDoDto>>> GetFilterAllAsync(ToDoParameter parameter);

        Task<ApiResponse<SummaryDto>> SummaryAsync();
    }
}