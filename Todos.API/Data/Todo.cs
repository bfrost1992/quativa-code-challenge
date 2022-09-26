using System.ComponentModel.DataAnnotations.Schema;

namespace Todos.API.Data
{
	public class Todo
	{
		public long Id { get; set; }
		public string? Label { get; set; }
		public bool Status { get; set; }
		public long TodoListId { get; set; }
	}
}
