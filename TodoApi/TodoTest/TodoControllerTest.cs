using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TodoApi.Controllers;
using TodoApi.Data;
using TodoApi.Models;
using Xunit;

namespace TodoTest
{
    public class TodoControllerTest
    {
        [Fact]
        public async void DBCanAddNewItem()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            using (TodoContext context = new TodoContext(options))
            {
                TodoItem item = new TodoItem();
                item.Name = "Walk the dog";
                item.ListID = 1;
                item.IsComplete = true;

                await context.TodoItems.AddAsync(item);
                await context.SaveChangesAsync();

                var results = await context.TodoItems.ToListAsync();

                Assert.Single(results);
            }
        }
    }
}
