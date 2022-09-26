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
		private readonly TodoDbContext Context;

		public TodoController(TodoDbContext context)
		{
			Context = context;
		}

		/// <summary>
		/// Get all todos
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Get() 
		{
			var todos = await Context.Todos.ToListAsync();
			return Ok(todos);
		}
		
		/// <summary>
		/// Set label for a given todo
		/// </summary>
		/// <param name="todoId"></param>
		/// <param name="cmd"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Set status for a given todo
		/// </summary>
		/// <param name="todoId"></param>
		/// <param name="cmd"></param>
		/// <returns></returns>
		[HttpPost("{todoId}/status")]
		public async Task<IActionResult> SetStatus(long todoId, SetTodoStatusCommand cmd)
		{
			var existingTodo = await Context.Todos.FindAsync(todoId);

			if (existingTodo == null)
			{
				return NotFound();
			}

			existingTodo.Status = cmd.Completed;

			return Ok(existingTodo);
		}

		/// <summary>
		/// Remove a given todo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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