using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todos.API.Commands;
using Todos.API.Data;

namespace Todos.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ListController : ControllerBase
	{
		public TodoDbContext Context { get; set; }

		public ListController(TodoDbContext context)
		{
			Context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var lists = await Context.Lists.ToListAsync();
			return Ok(lists);
		}

		[HttpGet("{listId}")]
		public async Task<IActionResult> Get(long listId)
		{
			var list = await Context.Lists.FindAsync(listId);

			if (list == null)
			{
				return NotFound();
			}

			return Ok(list);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddListCommand cmd)
		{
			Context.Lists.Add(new TodoList(cmd.Label));
			await Context.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("{listId}/todos")]
		public async Task<IActionResult> AddTodo(long listId, AddTodoCommand cmd)
		{
			var list = await Context.Lists.FindAsync(listId);

			if (list == null)
			{
				return NotFound();
			}

			list.Todos.Add(new Todo { Label = cmd.Label});
			await Context.SaveChangesAsync();

			return Ok();
		}

		[HttpGet("{listId}/todos")]
		public async Task<IActionResult> GetTodos(long listId)
		{
			var list = await Context.Lists.FindAsync(listId);

			if(list == null)
			{
				return NotFound();
			}

			return Ok(list.Todos);
		}
	}

}