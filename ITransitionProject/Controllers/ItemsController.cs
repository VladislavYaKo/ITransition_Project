using ITransitionProject.Helpers;
using ITransitionProject.Models;
using ITransitionProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Controllers
{
    [Authorize(Roles = "user, admin")]
    public class ItemsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext appContext;
        public ItemsController(UserManager<User> userManager, ApplicationContext appContext)
        {
            this.userManager = userManager;
            this.appContext = appContext;
        }
        [HttpGet]
        public async Task<IActionResult> ViewItem()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult AddItem(string userId, int colId, string CollectionName, string CollectionTheme)
        {
            if (!CommonHelpers.HasAccess(userId, userManager.GetUserId(User), User))
                return StatusCode(403);

            Collection col = appContext.Collections.FirstOrDefault(c => c.UserId == userId && c.Id == colId);
            string[] NumericFieldsNames = new string[3];
            if (col.AddFieldsNames.NumericFieldsNames != null)
                NumericFieldsNames = col.AddFieldsNames.NumericFieldsNames.Split(',');

            return View(new EditItemViewModel { 
                UserId = userId, 
                CollectionId = colId, 
                CollectionName = CollectionName, 
                CollectionTheme = CollectionTheme, 
                NumericFieldsNames = NumericFieldsNames,
                NumericFieldsValues = new string[3]
            });
        }
    }
}
