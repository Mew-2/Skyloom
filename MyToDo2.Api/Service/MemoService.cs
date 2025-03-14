using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo2.Api.Context;
using MyToDo2.Shared;
using MyToDo2.Shared.Dtos;
using MyToDo2.Shared.Parameters;

namespace MyToDo2.Api.Service
{
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public MemoService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(MemoDto model)
        {
            try
            {
                var memo = mapper.Map<Memo>(model);
                await work.GetRepository<Memo>().InsertAsync(memo);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, memo);
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
                var repository = work.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(memo);

                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, "");
                return new ApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<Memo>();
                var memos = await repository.GetPagedListAsync(
                    predicate: x => string.IsNullOrEmpty(parameter.Search) ? true : x.Title.Contains(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new ApiResponse(true, memos);
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
                var repository = work.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));

                return new ApiResponse(true, memo);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(MemoDto model)
        {
            try
            {
                var repository = work.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));

                if (memo != null)
                {
                    memo.Title = model.Title;
                    memo.Content = model.Content;
                    memo.UpdateDate = DateTime.Now;

                    repository.Update(memo);
                    if (await work.SaveChangesAsync() > 0)
                        return new ApiResponse(true, memo);
                }
                return new ApiResponse("更新数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}