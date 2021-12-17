

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Models;

namespace Todo.Services {

    public class TodoItemService : ITodoItemService {

        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user )
        {
            return await _context.Items
            .Where(x => x.IsDone == false && x.UserId == user.Id)
            .ToArrayAsync();
        }

        public async Task<bool> AddItemAsync(TodoItem item, IdentityUser user)
        {
            item.Id = Guid.NewGuid();
            item.IsDone = false;
            item.DueAt = DateTimeOffset.Now.AddDays(3);
            item.UserId = user.Id;


            _context.Items.Add(item);

            var saveResults = await _context.SaveChangesAsync();
            return saveResults == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id, IdentityUser user) {

            var item = _context.Items.Where((x) => x.Id == id && x.UserId == user.Id).FirstOrDefault(); //???

            if (item == null) return false;

            item.IsDone = true;

            var successful = await _context.SaveChangesAsync();
            return successful == 1;


        }

    }

}