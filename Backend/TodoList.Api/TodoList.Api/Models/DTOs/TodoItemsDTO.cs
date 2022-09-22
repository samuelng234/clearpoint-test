using System.Collections.Generic;

namespace TodoList.Api.Models
{
    public class TodoItemsDTO
    {
        public IEnumerable<TodoItemDTO> Items { get; set; }
    }
}
