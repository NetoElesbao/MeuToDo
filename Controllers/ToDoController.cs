using MeuToDo.Data;
using MeuToDo.Models;
using MeuToDo.ViewModels;
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
            var listTodos = await context.Todos.AsNoTracking().FirstOrDefaultAsync();

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

            return todo is null ? NotFound() : Ok(todo);
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

            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created(uri: $"v1/todos/{todo.Id}", todo);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> PutAsync
        (
           [FromServices] AppDbContext context,
           [FromBody] CreateToDoViewModel view,
           [FromRoute] int id
        )
        {
            if (!ModelState.IsValid) return BadRequest();

            var todo = await context.Todos.FirstOrDefaultAsync(e => e.Id == id);
            if (todo is null) return NotFound("Not found!");
            try
            {
                todo.UpdateToDo(view.Title);

                await context.SaveChangesAsync();

                return Ok(todo);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpDelete]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> DeleteAsync
        (
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        {

            var todo = await context.Todos.FirstOrDefaultAsync(e => e.Id == id);
            if (todo is null) return NotFound("Not found!");

            context.Todos.Remove(todo);
            await context.SaveChangesAsync();

            return Ok("Deleted");
        }
    }
}

