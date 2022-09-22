using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TodoList.Api.Models;

namespace TodoList.Api.Contexts
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TodoItem> TodoItems { get; set; }
    }
}
