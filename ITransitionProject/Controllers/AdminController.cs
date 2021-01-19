using ITransitionProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Controllers
{
    //[Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;
        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult EditUsers()
        {
            return View(userManager);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsers(string actionBtn)
        {
            int[] ids = Request.Form["choosenUser"].Select(int.Parse).ToArray();
            List<User> affectedUsers = userManager.Users.Where(u => ids.Contains(u.intId)).ToList();
            Task result = (Task)this.GetType()
                    .GetMethod(actionBtn + "Users", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(this, new object[] { affectedUsers });
            await result;
            return RedirectToAction("EditUsers");
        }

        private async Task DeleteUsers(List<User> affectedUsers)
        {
            foreach(User user in affectedUsers)
            {
                await userManager.UpdateSecurityStampAsync(user);
                await userManager.DeleteAsync(user);
            }            
        }

        private async Task BlockUsers(List<User> affectedUsers)
        {
            foreach(User user in affectedUsers)
            {
                await userManager.SetLockoutEndDateAsync(user, DateTime.MaxValue);  //Нужно только для уже зареганых пользователей. Если всех удалим - можно будет убрать
                await userManager.SetLockoutEnabledAsync(user, true);
            }
        }
        private async Task UnblockUsers(List<User> affectedUsers)
        {
            foreach (User user in affectedUsers)
            {
                await userManager.SetLockoutEnabledAsync(user, false);
            }
        }
    }
}
