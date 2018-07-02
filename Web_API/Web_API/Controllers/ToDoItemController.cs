using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_API.Data;
using Web_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public ToDoItemController(ToDoDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<ToDoItem> Get()
        {
            return _context.ToDoItems;
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get([FromRoute]int id)
        {
            var item = _context.ToDoItems.FirstOrDefault(i => i.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ToDoItem todoitem)
        {
            await _context.ToDoItems.AddAsync(todoitem);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("Get", new { id = todoitem.ID }, todoitem);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
