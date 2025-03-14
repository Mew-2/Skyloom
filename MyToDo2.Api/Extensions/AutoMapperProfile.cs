using AutoMapper;
using MyToDo2.Api.Context;
using MyToDo2.Shared.Dtos;

namespace MyToDo2.Api.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap();
            CreateMap<Memo, MemoDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}