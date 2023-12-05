using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuToDo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDo> Todos => Set<ToDo>();

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }
}