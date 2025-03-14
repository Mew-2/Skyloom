using Microsoft.EntityFrameworkCore;

namespace MyToDo2.Api.Context
{
    public class MyToDo2Context : DbContext
    {
        public MyToDo2Context(DbContextOptions<MyToDo2Context> options) : base(options)
        {
        }

        public DbSet<ToDo> ToDo { get; set; }
        public DbSet<Memo> Memo { get; set; }
        public DbSet<User> User { get; set; }
    }
}