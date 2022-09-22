using System;
using System.Threading.Tasks;
using TodoList.Api.Models;

namespace TodoList.Api.Services
{
    public interface ITodoItemsService
    {
        Task<TodoItemsDTO> GetItems();
        Task<TodoItemDTO> GetItem(Guid id);
        Task UpdateItem(UpdateTodoItemRequest request, Guid id);
        Task CreateItem(CreateTodoItemRequest request);
    }
}
