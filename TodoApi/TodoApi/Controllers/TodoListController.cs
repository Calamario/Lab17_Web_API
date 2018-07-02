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

        /// <summary>
        /// returns all lists in a list
        /// </summary>
        /// <returns>list of list</returns>
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<TodoList>> GetAll()
        {
            return _context.TodoLists.ToList();
        }

        /// <summary>
        /// returns a single list
        /// </summary>
        /// <param name="id">an int id to specify single list</param>
        /// <returns>found list or notfound</returns>
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

        /// <summary>
        /// update single list
        /// </summary>
        /// <param name="id">int id to specify single list</param>
        /// <param name="newList">new list information to replace</param>
        /// <returns>no content</returns>
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

        /// <summary>
        /// create new list into DB
        /// </summary>
        /// <param name="list">new list information</param>
        /// <returns>the new list created</returns>
        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(TodoList list)
        {
            _context.TodoLists.Add(list);
            _context.SaveChanges();

            return CreatedAtRoute("GetToDoList", new { id = list.ID }, list);
        }


        /// <summary>
        /// deletes a single list and all its items
        /// </summary>
        /// <param name="id">the int id of the list</param>
        /// <returns>nocontent</returns>
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
