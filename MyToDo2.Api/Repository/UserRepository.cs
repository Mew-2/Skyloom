using Arch.EntityFrameworkCore.UnitOfWork;
using MyToDo2.Api.Context;

namespace MyToDo2.Api.Repository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(MyToDo2Context dbContext) : base(dbContext)
        {
        }
    }
}