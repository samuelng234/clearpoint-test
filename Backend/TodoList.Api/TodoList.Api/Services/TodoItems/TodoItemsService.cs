using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Api.Models;
using TodoList.Api.Repositories;

namespace TodoList.Api.Services
{
    public class TodoItemsService : BaseService, ITodoItemsService
    {
        private readonly ITodoItemsRepository _repository;

        public TodoItemsService(ILogger<TodoItemsService> logger, IMapper mapper, ITodoItemsRepository repository)
            : base(logger, mapper)
        {
            _repository = repository;
        }

        public async Task<TodoItemsDTO> GetItems()
        {
            var items = await _repository.GetItems();
            return _mapper.Map<TodoItemsDTO>(items);
        }

        public async Task<TodoItemDTO> GetItem(Guid id)
        {
            var item = await _repository.GetItem(id);
            return _mapper.Map<TodoItemDTO>(item);
        }

        public async Task UpdateItem(UpdateTodoItemRequest request, Guid id)
        {
            await _repository.UpdateItem(request, id);
        }

        public async Task CreateItem(CreateTodoItemRequest request)
        {
            await _repository.CreateItem(request);
        }
    }
}
