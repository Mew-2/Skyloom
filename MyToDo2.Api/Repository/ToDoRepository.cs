using Arch.EntityFrameworkCore.UnitOfWork;
using MyToDo2.Api.Context;

namespace MyToDo2.Api.Repository
{
    public class ToDoRepository : Repository<ToDo>, IRepository<ToDo>
    {
        public ToDoRepository(MyToDo2Context dbContext) : base(dbContext)
        {
        }
    }
}