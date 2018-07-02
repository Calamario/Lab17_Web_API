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
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        
        public TodoController(TodoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// gets all items as a list
        /// </summary>
        /// <returns>list of items</returns>
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return Ok(_context.TodoItems.ToList());
        }

        /// <summary>
        /// returns a single item
        /// </summary>
        /// <param name="id">int id for one item</param>
        /// <returns>todoitem</returns>
        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var item = _context.TodoItems.Find(id);
            //item.List = _context.TodoLists.Find(item.ListID);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }


        /// <summary>
        /// creates a todo item and stores it in the deployed db
        /// </summary>
        /// <param name="item">todoitem</param>
        /// <returns>the item created</returns>
        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.ID }, item);
        }

        /// <summary>
        /// update an existing item
        /// </summary>
        /// <param name="id">int id to specify single item</param>
        /// <param name="item">teh new item information to replace old</param>
        /// <returns>nocontent</returns>
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoItem item)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            todo.ListID = item.ListID;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// deletes a single item
        /// </summary>
        /// <param name="id">int id of a single item</param>
        /// <returns>nocontent</returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
