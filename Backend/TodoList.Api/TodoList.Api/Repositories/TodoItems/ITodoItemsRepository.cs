using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Api.Models;

namespace TodoList.Api.Repositories
{
    public interface ITodoItemsRepository
    {
        Task<IEnumerable<TodoItem>> GetItems();
        Task<TodoItem> GetItem(Guid id);
        Task UpdateItem(UpdateTodoItemRequest request, Guid id);
        Task CreateItem(CreateTodoItemRequest request);
    }
}
