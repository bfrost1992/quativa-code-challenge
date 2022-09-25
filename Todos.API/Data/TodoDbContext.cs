using Microsoft.EntityFrameworkCore;

namespace Todos.API.Data
{
	public class TodoDbContext : DbContext
	{
		public TodoDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Todo> Todos { get; set; }
		public DbSet<TodoList> Lists { get; set; }
		

	}
}
