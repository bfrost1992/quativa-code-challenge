namespace Todos.API.Data
{
	public class TodoList
	{
		public TodoList(string label)
		{
			Label = label;
			Todos = new List<Todo>();
		}
		public long Id { get; set; }
		public string Label { get; set; }
		public virtual List<Todo> Todos { get; set; }
	}
}
