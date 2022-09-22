using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TodoList.Api.Models;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : BaseController
    {
        private readonly ITodoItemsService _service;

        public TodoItemsController(ILogger<TodoItemsController> logger, ITodoItemsService service)
            : base(logger)
        {
            _service = service;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            try
            {
                var items = await _service.GetItems();
                return Ok(items);
            }
            catch (Exception e)
            {
                _logger.LogError("An error has occurred on GetTodoItems controller route: {error}", e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // GET: api/TodoItems/...
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            try
            {
                var result = await _service.GetItem(id);

                if (result == null)
                {
                    return BadRequest("Item does not exist");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError("An error has occurred on GetTodoItems controller route: {error}", e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(Guid id, UpdateTodoItemRequest request)
        {
            try
            {
                await _service.UpdateItem(request, id);

                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogWarning("Todo item could not be found: {error}", e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error has occurred on GetTodoItems controller route: {error}", e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        } 

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<IActionResult> CreateTodoItem(CreateTodoItemRequest request)
        {
            try
            {
                await _service.CreateItem(request);

                return NoContent();
            }
            catch (BadHttpRequestException e)
            {
                _logger.LogWarning("Todo item could not be created: {error}", e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error has occurred on GetTodoItems controller route: {error}", e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
