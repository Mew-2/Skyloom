using MyToDo2.Shared.Dtos;
using MyToDo2.Shared.Parameters;

namespace MyToDo2.Api.Service
{
    public interface IMemoService : IBaseService<MemoDto, QueryParameter>
    {
    }
}