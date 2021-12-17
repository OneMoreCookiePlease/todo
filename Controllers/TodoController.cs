


using Todo.Models;
using Todo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

[Authorize]
public class TodoController : Controller
{
    private readonly ITodoItemService _todoItemService; 
    private readonly UserManager<IdentityUser> _userManager;

    public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
    {
        _todoItemService = todoItemService;
        _userManager = userManager;
    }
    public async Task<IActionResult> IndexAsync()
    {
        // Get to-do items from database
        // Put items into a model
        // Pass the view to a model and render

        var current_user = await _userManager.GetUserAsync(User);
        if (current_user == null) return Challenge();

        var items = await _todoItemService.GetIncompleteItemsAsync(current_user);
        var model = new TodoViewModel();

        model.Items = items;

        return View(model); 
    
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(TodoItem item)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }


        var current_user = await _userManager.GetUserAsync(User);
        if (current_user == null) return Challenge();

        var successful = await _todoItemService.AddItemAsync(item, current_user);
        if (!successful)
        {
            return BadRequest("Could not add an item.");
        }

        return RedirectToAction("Index");
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkDone(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToAction("index");
        }

        var current_user =await _userManager.GetUserAsync(User);
        if (current_user == null) return Challenge();

        var successful = await _todoItemService.MarkDoneAsync(id, current_user);
        if (!successful)
        {
            return BadRequest("Could not mark an item done");
        }

        return RedirectToAction("index");



    }

}
