using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuToDo.Models
{
    public class ToDo
    {
        // public ToDo() { }
        public ToDo(string title)
        {
            Random random = new();
            Id = random.Next(1, 100);
            Title = title;
            Date = DateTime.Now;
            Done = true;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime Date { get; set; }
    }
}