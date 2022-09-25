using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todos.API.Commands;
using Todos.API.Data;

namespace Todos.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TodoController : ControllerBase
	{
		public TodoDbContext Context { get; set; }

		public TodoController(TodoDbContext context)
		{
			Context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Get() 
		{
			var todos = await Context.Todos.ToListAsync();
			return Ok(todos);
		}
		
		[HttpPost("{todoId}/label")]
		public async Task<IActionResult> Rename(long todoId, RenameTodoCommand cmd)
		{
			var existingTodo = await Context.Todos.FindAsync(todoId);

			if(existingTodo == null)
			{
				return NotFound();
			}

			existingTodo.Label = cmd.Label;
			return Ok(existingTodo);
		}
		[HttpPost("{todoId}/status")]
		public async Task<IActionResult> SetStatus(long todoId, SetTodoStatusCommand cmd)
		{
			var existingTodo = await Context.Todos.FindAsync(todoId);

			if (existingTodo == null)
			{
				return NotFound();
			}

			existingTodo.Status = cmd.Completed;

			return Ok();
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(long id)
		{
			var todo = new Todo { Id = id };
			Context.Todos.Attach(todo);
			Context.Todos.Remove(todo);
			await Context.SaveChangesAsync();
			return Ok();
		}

	}

}