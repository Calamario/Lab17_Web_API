using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API.Models
{
    public class ToDoList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public List<ToDoItem> ThingsToDo { get; set; }
    }
}
