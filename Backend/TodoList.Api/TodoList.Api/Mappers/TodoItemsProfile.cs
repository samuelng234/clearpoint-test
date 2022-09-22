using AutoMapper;
using System.Collections.Generic;
using TodoList.Api.Models;

namespace TodoList.Api.Mappers
{
    public class TodoItemsProfile : Profile
    {
        public TodoItemsProfile()
        {
            CreateMap<IEnumerable<TodoItem>, TodoItemsDTO>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));
        }
    }
}
