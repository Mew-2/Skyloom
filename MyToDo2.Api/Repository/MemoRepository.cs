using Arch.EntityFrameworkCore.UnitOfWork;
using MyToDo2.Api.Context;

namespace MyToDo2.Api.Repository
{
    public class MemoRepository : Repository<Memo>, IRepository<Memo>
    {
        public MemoRepository(MyToDo2Context dbContext) : base(dbContext)
        {
        }
    }
}