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
		private readonly TodoDbContext Context;

		public ListController(TodoDbContext context)
		{
			Context = context;
		}

		/// <summary>
		/// Get all lists
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("", Name ="Get all lists")]
		public async Task<IActionResult> Get()
		{
			var lists = await Context.Lists.ToListAsync();
			return Ok(lists);
		}

		/// <summary>
		/// Get a specific list
		/// </summary>
		/// <param name="listId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{listId}", Name = "Get a specific list")]
		public async Task<IActionResult> Get(long listId)
		{
			var list = await Context.Lists.FindAsync(listId);

			if (list == null)
			{
				return NotFound();
			}

			return Ok(list);
		}

		/// <summary>
		/// Get all todos for a given list
		/// </summary>
		/// <param name="listId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{listId}/todos", Name = "Get todos for list")]
		public async Task<IActionResult> GetTodos(long listId)
		{
			var list = await Context.Lists.Include(s => s.Todos).FirstOrDefaultAsync(s => s.Id == listId);

			if (list == null)
			{
				return NotFound();
			}

			return Ok(list.Todos.ToList());
		}

		/// <summary>
		/// Add a new list
		/// </summary>
		/// <param name="cmd"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("", Name="Add new list")]
		public async Task<IActionResult> Add(AddListCommand cmd)
		{
			var newList = new TodoList(cmd.Label);
			Context.Lists.Add(newList);
			await Context.SaveChangesAsync();
			return Ok(newList);
		}

		/// <summary>
		/// Add a new todo to a list
		/// </summary>
		/// <param name="listId"></param>
		/// <param name="cmd"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("{listId}/todos", Name="Add new todo to list")]
		public async Task<IActionResult> AddTodo(long listId, AddTodoCommand cmd)
		{
			var newTodo = new Todo { Label = cmd.Label };
			var list = await Context.Lists.FindAsync(listId);

			if (list == null)
			{
				return NotFound();
			}

			list.Todos.Add(newTodo);
			await Context.SaveChangesAsync();

			return Ok(newTodo);
		}

		
	}

}