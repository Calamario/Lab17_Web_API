using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        public readonly TodoContext _context;
        public TodoListController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<TodoList>> GetAll()
        {
            return _context.TodoLists.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetToDoList")]
        public ActionResult<TodoList> Get([FromRoute]int id)
        {
            var list = _context.TodoLists.Find(id);
            var todoItems = _context.TodoItems.Where(i => i.ListID == id).ToList();
            list.Items = todoItems;
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        //Put api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id , TodoList newList)
        {
            var list = _context.TodoLists.Find(id);
            if (list == null)
            {
                return NotFound();
            }

            list.Name = newList.Name;

            _context.TodoLists.Update(list);
            _context.SaveChanges();
            return NoContent();
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(TodoList list)
        {
            _context.TodoLists.Add(list);
            _context.SaveChanges();

            return CreatedAtRoute("GetToDoList", new { id = list.ID }, list);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var list = _context.TodoLists.Find(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.TodoLists.Remove(list);

            var items = _context.TodoItems.Where(i => i.ListID == id).ToList();
            foreach (var item in items)
            {
                _context.TodoItems.Remove(item);
            }

            _context.SaveChanges();

            return NoContent();
        }
    }
}
