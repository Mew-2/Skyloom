using MyToDo2.Api.Context;
using MyToDo2.Shared;
using MyToDo2.Shared.Dtos;
using MyToDo2.Shared.Parameters;

namespace MyToDo2.Api.Service
{
    public interface IToDoService : IBaseService<ToDoDto, ToDoParameter>
    {
        Task<ApiResponse> Summary();
    }
}