using MyToDo2.Shared;
using MyToDo2.Shared.Collections;
using MyToDo2.Shared.Dtos;
using MyToDo2.Shared.Parameters;

namespace MyToDo2.Services
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        private readonly HttpRestClient client;

        public ToDoService(HttpRestClient client) : base(client, "ToDo")
        {
            this.client = client;
        }

        public async Task<ApiResponse<PagedList<ToDoDto>>> GetFilterAllAsync(ToDoParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"/api/ToDo/GetAll?PageIndex={parameter.PageIndex}"
                + $"&PageSize={parameter.PageSize}"
                + $"&Search={parameter.Search}"
                + $"&Status={parameter.Status}";

            return await client.ExcuteAsync<PagedList<ToDoDto>>(request);
        }

        public async Task<ApiResponse<SummaryDto>> SummaryAsync()
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = "/api/ToDo/Summary";

            return await client.ExcuteAsync<SummaryDto>(request);
        }
    }
}