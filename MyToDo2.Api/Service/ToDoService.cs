using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo2.Api.Context;
using MyToDo2.Shared;
using MyToDo2.Shared.Dtos;
using MyToDo2.Shared.Parameters;
using System.Collections.ObjectModel;

namespace MyToDo2.Api.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public ToDoService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(ToDoDto model)
        {
            try
            {
                var todo = mapper.Map<ToDo>(model);
                await work.GetRepository<ToDo>().InsertAsync(todo);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, todo);
                return new ApiResponse("添加数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(todo);

                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, "");
                return new ApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync(ToDoParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todos = await repository.GetPagedListAsync(
                    predicate: x => (string.IsNullOrEmpty(parameter.Search) ? true : x.Title.Contains(parameter.Search))
                    && parameter.Status == null ? true : x.Status.Equals(parameter.Status),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new ApiResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                return new ApiResponse(true, todo);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDoDto model)
        {
            try
            {
                var repository = work.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));

                if (todo != null)
                {
                    todo.Title = model.Title;
                    todo.Content = model.Content;
                    todo.Status = model.Status;
                    todo.UpdateDate = DateTime.Now;

                    repository.Update(todo);
                    if (await work.SaveChangesAsync() > 0)
                        return new ApiResponse(true, todo);
                }
                return new ApiResponse("更新数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> Summary()
        {
            try
            {
                //待办事项结果
                var todos = await work.GetRepository<ToDo>().GetPagedListAsync(orderBy: source => source.OrderByDescending(t => t.CreateDate));
                //备忘录结果
                var memos = await work.GetRepository<Memo>().GetPagedListAsync(orderBy: source => source.OrderByDescending(t => t.CreateDate));

                SummaryDto summary = new SummaryDto();

                summary.ToDoSum = todos.Items.Count();//汇总待办事项数量
                summary.CompletedCount = todos.Items.Where(t => t.Status == 1).Count();//统计完成数量
                summary.CompletedRatio = (summary.CompletedCount / (double)summary.ToDoSum).ToString("0%");//统计完成比例
                summary.MemoryCount = memos.Items.Count();//汇总备忘录数量
                summary.TodoList = new ObservableCollection<ToDoDto>(mapper.Map<List<ToDoDto>>(todos.Items.Where(t => t.Status == 0)));
                summary.MemoList = new ObservableCollection<MemoDto>(mapper.Map<List<MemoDto>>(memos.Items));

                return new ApiResponse(true, summary);
            }
            catch (Exception)
            {
                return new ApiResponse(false, "");
            }
        }
    }
}