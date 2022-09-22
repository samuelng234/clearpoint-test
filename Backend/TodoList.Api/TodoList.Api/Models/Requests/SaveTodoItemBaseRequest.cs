namespace TodoList.Api.Models
{
    public class SaveTodoItemBaseRequest
    {
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
