using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeuToDo.ViewModel
{
    public class CreateToDoViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}