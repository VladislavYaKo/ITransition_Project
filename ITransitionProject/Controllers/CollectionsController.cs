using ITransitionProject.Models;
using ITransitionProject.ViewModels;
using ITransitionProject.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class CollectionsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext appContext;
        public CollectionsController(UserManager<User> userManager, ApplicationContext appContext)
        {
            this.userManager = userManager;
            this.appContext = appContext;
        }

        public IActionResult EditCollections(string userId)
        {
            if (userId == null)
                userId = userManager.GetUserId(User);
            List<Collection> collections = appContext.Collections.Where(coll => coll.UserId == userId).ToList();
            ViewBag.userId = userId;
            return View(collections);
        }

        [HttpGet]
        public IActionResult AddCollection(string userId)
        {
            if (userId == null)
                userId = userManager.GetUserId(User);
            ViewBag.Themes = Enum.GetNames(typeof(Collection.Themes)).AsEnumerable<string>();
            return View(new EditCollectionViewModel { userId = userId });
        }

        [HttpPost]
        public async Task<IActionResult> AddCollection(EditCollectionViewModel model)
        {
            if(ModelState.IsValid)
            {
                AdditionalFieldsNames afn = new AdditionalFieldsNames(model.intFieldName);
                Collection newCollection = new Collection { Name = model.Name, Theme = model.Theme, briefDesc = model.BriefDesc, imgUrl = model.ImgUrl, AddFieldsNames = afn };
                User user = await userManager.FindByIdAsync(model.userId);
                if (user.Collections == null)
                    user.Collections = new List<Collection>();
                user.Collections.Add(newCollection);
                await userManager.UpdateAsync(user);
                return RedirectToAction("EditCollections", new { userId = model.userId });
            }
            else
            {
                ModelState.AddModelError("", "Введите все требуемые поля.");
            }
            return View(new EditCollectionViewModel { userId = model.userId });
        }

        [HttpGet]
        public IActionResult EditCollection(int colId)
        {
            Collection col = appContext.Collections.FirstOrDefault(c => c.Id == colId);
            return View(new EditCollectionViewModel { Name= col?.Name, collectionId = colId, userId = col?.UserId, BriefDesc = col?.briefDesc , ImgUrl = col?.imgUrl});
        }

        [HttpPost]
        public async Task<IActionResult> EditCollection(EditCollectionViewModel model)
        {
            if(ModelState.IsValid)
            {
                Collection newCol = appContext.Collections.FirstOrDefault(c => c.Id == model.collectionId);
                User curUser = await userManager.GetUserAsync(User);
                if (newCol.UserId == curUser.Id || await userManager.IsInRoleAsync(curUser, "admin"))
                {
                    newCol.Name = model.Name;
                    newCol.briefDesc = model.BriefDesc;
                    newCol.imgUrl = model.ImgUrl;
                    await appContext.SaveChangesAsync();
                    return RedirectToAction("EditCollections", new { userId = model.userId } );
                }
                else
                    ModelState.AddModelError("", "Не пытайтесь менять чужую коллекцию!");
            }
            else
            {
                ModelState.AddModelError("", "Введите все требуемые поля.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewCollection(string userId, int colId)
        {
            Collection col = appContext.Collections.FirstOrDefault(c => c.UserId == userId && c.Id == colId);
            List<Item> items = appContext.Items.Where(i => i.CollectionId == colId).ToList();
            return View(new EditCollectionItemsViewModel(userId, colId, col.Name, EnumHelper.GetEnumDisplayName(col.Theme), items));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCollection(int colId)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public IActionResult AddITem(string userId, int colId, string CollectionName, string CollectionTheme)
        {
            if (userId != userManager.GetUserId(User) && !User.IsInRole("admin"))
                return StatusCode(403);

            return View(new EditCollectionItemsViewModel { userId = userId, colId = colId, CollectionName = CollectionName, CollectionTheme = CollectionTheme });
        }

        [HttpGet]
        public async Task<IActionResult> ViewItem()
        {
            throw new NotImplementedException();
        }
    }
}
