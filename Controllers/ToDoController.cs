using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MeuToDo.Data;
using MeuToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MeuToDo.Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class ToDoController : ControllerBase
    {
        [HttpGet]
        [Route(template: "todos")]
        public async Task<IActionResult> Get
        (
            [FromServices] AppDbContext context
        )
        {
            var listTodos = await context.Todos.FirstOrDefaultAsync();

            if (listTodos is null) return NotFound("List empty");
            return Ok(listTodos);
        }
    }
}
