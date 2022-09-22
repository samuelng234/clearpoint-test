using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.Contexts;
using TodoList.Api.Models;

namespace TodoList.Api.Repositories
{
    public class TodoItemsRepository : BaseRepository, ITodoItemsRepository
    {
        private readonly TodoContext _context;

        public TodoItemsRepository(TodoContext context, ILogger<TodoItemsRepository> logger)
            : base(logger)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetItems()
        {
            IEnumerable<TodoItem> results = new List<TodoItem>();

            try
            {
                results = await _context.TodoItems
                    .Where(x => !x.IsCompleted)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred in the TodoItems repository on the GetItems function: {message}", e.Message);
                throw;
            }

            return results;
        }

        public async Task<TodoItem> GetItem(Guid id)
        {
            TodoItem result = null;

            try
            {
                result = await _context.TodoItems.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred in the TodoItems repository on the GetItem function: {message}", e.Message);
                throw;
            }

            return result; 
        }

        public async Task UpdateItem(UpdateTodoItemRequest request, Guid id)
        {
            try
            {
                if (!_context.TodoItems.Any(x => x.Id == id))
                {
                    _logger.LogWarning("The item with id: {id} does not exist", id);
                    throw new KeyNotFoundException($"The item with id: {id} does not exist");
                }

                var todoItem = new TodoItem()
                {
                    Id = id,
                    Description = request.Description,
                    IsCompleted = request.IsCompleted
                };

                _context.Entry(todoItem).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                _logger.LogInformation("The item with id: {id} has been updated", id);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred in the TodoItems repository on the UpdateItem function: {message}", e.Message);
                throw;
            }
        }

        public async Task CreateItem(CreateTodoItemRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request?.Description))
                {
                    throw new BadHttpRequestException("Description is required");
                }
                else if (_context.TodoItems.Any(x => x.Description.ToLowerInvariant() == request.Description.ToLowerInvariant() && !x.IsCompleted))
                {
                    throw new BadHttpRequestException("Description already exists");
                }

                var todoItem = new TodoItem()
                {
                    Id = Guid.NewGuid(),
                    Description = request.Description,
                    IsCompleted = request.IsCompleted
                };

                _context.TodoItems.Add(todoItem);

                await _context.SaveChangesAsync();

                _logger.LogInformation("A new todo item has been create. Id: {id}", todoItem.Id);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred in the TodoItems repository on the CreateItem function: {message}", e.Message);
                throw;
            }
        }
    }
}
