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

        public IActionResult UserCollections(int ViewUserIntId)
        {
            User user = userManager.Users.FirstOrDefault(u => u.intId == ViewUserIntId);
            return View(user.Collections);
        }
    }
}
