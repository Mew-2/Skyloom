using Microsoft.AspNetCore.Mvc;
using MyToDo2.Api.Context;
using MyToDo2.Api.Service;
using MyToDo2.Shared;
using MyToDo2.Shared.Dtos;
using MyToDo2.Shared.Parameters;

namespace MyToDo2.Api.Controllers
{
    /// <summary>
    /// 待办事项控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService service;

        public ToDoController(IToDoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] ToDoParameter parameter) => await service.GetAllAsync(parameter);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDoDto model) => await service.AddAsync(model);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDoDto model) => await service.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);

        [HttpGet]
        public async Task<ApiResponse> Summary() => await service.Summary();
    }
}