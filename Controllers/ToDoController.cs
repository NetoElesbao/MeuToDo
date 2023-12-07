using MeuToDo.Data;
using MeuToDo.Models;
using MeuToDo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuToDo.Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class ToDoController : ControllerBase
    {
        [HttpGet]
        [Route(template: "todos")]
        public async Task<IActionResult> GetAsync
        (
            [FromServices] AppDbContext context
        )
        {
            var listTodos = await context.Todos.AsNoTracking().ToListAsync();

            if (listTodos is null) return NotFound("List empty");
            return Ok(listTodos);
        }

        [HttpGet]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> GetByAsync
        (
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        {
            var todo = await context.Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (todo is null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        [Route(template: "todos")]
        public async Task<IActionResult> PostAsync
        (
            [FromServices] AppDbContext context,
            [FromBody] CreateToDoViewModel viewModel
        )
        {
            if (!ModelState.IsValid) return BadRequest();

            var todo = new ToDo(viewModel.Title);
            await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();

            return Created("todos/", todo.Id);
        }
    }
}
