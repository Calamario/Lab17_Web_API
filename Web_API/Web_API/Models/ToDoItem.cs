using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API.Models
{
    public class ToDoItem
    {
        public int ID { get; set; }
        public string Item { get; set; }
        public bool IsComplete { get; set; }
        public int ListID { get; set; }
    }
}
