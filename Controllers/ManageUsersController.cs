using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Todo.Models;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Administrator")]
public class ManageUsersController : Controller
{

    private readonly UserManager<IdentityUser> _userManager;

    public ManageUsersController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var admins = (await _userManager.GetUsersInRoleAsync("Administrator")).ToArray();
        var everyone = await _userManager.Users.ToArrayAsync();
        var model = new ManageUsersViewModel(admins, everyone);
        return View(model);
    }
}

