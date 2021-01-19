using ITransitionProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        public UsersController(UserManager<User>  userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult ViewUsers()
        {
            return View(userManager.Users as IEnumerable<User>);
        }

        public async Task<IActionResult> ViewCollections(string ViewUserId)
        {
            //string ViewUserId = Request.RouteValues["ViewUserId"].ToString();
            User user = await userManager.FindByIdAsync(ViewUserId);
            return View(user.Collections);
        }
    }
}
