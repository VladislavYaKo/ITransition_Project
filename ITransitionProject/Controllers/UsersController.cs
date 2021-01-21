using ITransitionProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext appContext;
        public UsersController(UserManager<User>  userManager, ApplicationContext appContext)
        {
            this.userManager = userManager;
            this.appContext = appContext;
        }

        public IActionResult ViewUsers()
        {            
            return View(userManager.Users as IEnumerable<User>);
        }

        public IActionResult UserCollections(int ViewUserIntId)
        {
            string userId = userManager.Users.Where(u => u.intId == ViewUserIntId).Select(u => u.Id).FirstOrDefault();
            ViewBag.userIntId = ViewUserIntId;
            return View(appContext.Collections.Where(col => col.UserId == userId));
        }

        public IActionResult UserCollection(int ViewUserIntId, int ViewCollectionId)
        {
            List<Item> collItems = appContext.Items.Where(i => i.CollectionId == ViewCollectionId).ToList();
            return View();
        }
    }
}
