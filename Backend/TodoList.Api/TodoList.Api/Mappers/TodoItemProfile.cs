using AutoMapper;
using TodoList.Api.Models;

namespace TodoList.Api.Mappers
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>();
        }
    }
}
