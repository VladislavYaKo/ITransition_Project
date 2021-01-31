using ITransitionProject.Helpers;
using ITransitionProject.Models;
using ITransitionProject.ViewModels;
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

        public IActionResult UserCollections(string userId)
        {            
            if (User.IsInRole("admin"))
                return RedirectToAction("EditCollections", "Collections", new { userId = userId });

            ViewBag.userId = userId;
            
            return View(CommonHelpers.MakeUpUserCollectionsVM(userId, appContext));
        }

        public IActionResult UserCollection(string userId, int collectionId)
        {
            if (User.IsInRole("admin"))
                return RedirectToAction("ViewCollection", "Collections", new { userId = userId, collectionId = collectionId });

            Collection col = appContext.Collections.FirstOrDefault(c => c.UserId == userId && c.Id == collectionId);
            ViewBag.UserId = userId;
            ViewBag.CollectionId = collectionId;
            if (col.AddFieldsNamesId != Guid.Empty)
                ViewBag.AdditionalFieldsNames = appContext.AdditionalFieldsNames.FirstOrDefault(a => a.Id == col.AddFieldsNamesId).GetAllNames();
            else
                ViewBag.AdditionalFieldsNames = "";

            EditCollectionItemsViewModel model = new EditCollectionItemsViewModel();
            model.CollectionId = collectionId;
            model.UserId = userId;
            model.CollectionName = col.Name;
            model.CollectionTheme = EnumHelper.GetEnumDisplayName(col.Theme);
            model.Items = appContext.Items.Where(i => i.CollectionUserId == userId && i.CollectionId == collectionId).ToList();

            return View(model);
        }

        
    }
}
