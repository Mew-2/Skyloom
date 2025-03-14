using MyToDo2.Shared;
using MyToDo2.Shared.Collections;
using MyToDo2.Shared.Parameters;

namespace MyToDo2.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly HttpRestClient client;
        private readonly string serviceName;

        public BaseService(HttpRestClient client, string serviceName)
        {
            this.client = client;
            this.serviceName = serviceName;
        }

        public async Task<ApiResponse<T>> AddAsync(T entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"/api/{serviceName}/Add";
            request.Parameter = entity;

            return await client.ExcuteAsync<T>(request);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Delete;
            request.Route = $"/api/{serviceName}/Delete?id={id}";

            return await client.ExcuteAsync(request);
        }

        public async Task<ApiResponse<PagedList<T>>> GetAllAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"/api/{serviceName}/GetAll?PageIndex={parameter.PageIndex}"
                + $"&PageSize={parameter.PageSize}"
                + $"&Search={parameter.Search}";

            return await client.ExcuteAsync<PagedList<T>>(request);
        }

        public async Task<ApiResponse<T>> GetAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"/api/{serviceName}/Get?id={id}";

            return await client.ExcuteAsync<T>(request);
        }

        public async Task<ApiResponse<T>> UpdateAsync(T entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"/api/{serviceName}/Update";
            request.Parameter = entity;

            return await client.ExcuteAsync<T>(request);
        }
    }
}