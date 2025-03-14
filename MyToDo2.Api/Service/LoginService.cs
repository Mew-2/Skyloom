using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo2.Api.Context;
using MyToDo2.Shared;
using MyToDo2.Shared.Dtos;
using MyToDo2.Shared.Extensions;
using System.Security.Principal;

namespace MyToDo2.Api.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public LoginService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> LoginAsync(string account, string password)
        {
            try
            {
                password = password.GetMD5();
                var repository = work.GetRepository<User>();
                var user = await repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Account.Equals(account) && x.Password.Equals(password));
                if (user != null)
                    return new ApiResponse(true, user);
                return new ApiResponse("登录失败，用户名或密码错误");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> RegisterAsync(UserDto model)
        {
            try
            {
                var user = mapper.Map<User>(model);
                var repository = work.GetRepository<User>();

                var result = await repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Account.Equals(user.Account));

                if (result != null) return new ApiResponse("当前账号已存在，请重试");

                user.Password = user.Password.GetMD5();
                user.CreateDate = DateTime.Now;

                await repository.InsertAsync(user);

                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, user);
                return new ApiResponse("注册失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}