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
            ViewBag.userId = userId;            
            return View(CommonHelpers.MakeUpUserCollectionsViewModel(userId, appContext));
        }

        [HttpGet]
        public IActionResult AddCollection(string userId)
        {
            if (userId == null)
                userId = userManager.GetUserId(User);
            ViewBag.Themes = Enum.GetNames(typeof(Collection.Themes)).AsEnumerable<string>();
            return View(new EditCollectionViewModel { UserId = userId });
        }

        [HttpPost]
        public async Task<IActionResult> AddCollection(EditCollectionViewModel model)
        {
            if(ModelState.IsValid)
            {
                AdditionalFieldsNames afn = new AdditionalFieldsNames(model.NumericFieldName);
                Collection newCollection = new Collection { Id = CalculateNewCollectionIndex(model.UserId), 
                    UserId = model.UserId,
                    Name = model.Name, 
                    Theme = model.Theme, 
                    briefDesc = model.BriefDesc, 
                    imgUrl = model.ImgUrl, 
                    AddFieldsNames = afn };
                User user = await userManager.FindByIdAsync(model.UserId);
                if (user.Collections == null)
                    user.Collections = new List<Collection>();
                user.Collections.Add(newCollection);
                await userManager.UpdateAsync(user);
                return RedirectToAction("EditCollections", new { userId = model.UserId });
            }
            else
            {
                ModelState.AddModelError("", "Введите все требуемые поля.");
            }
            return View(new EditCollectionViewModel { UserId = model.UserId });
        }

        [HttpGet]
        public IActionResult EditCollection(string userId, int collectionId)
        {
            Collection col = appContext.Collections.FirstOrDefault(c => c.UserId == userId && c.Id == collectionId);
            return View(new EditCollectionViewModel { Name= col?.Name, CollectionId = collectionId, UserId = col?.UserId, BriefDesc = col?.briefDesc , ImgUrl = col?.imgUrl});
        }

        [HttpPost]
        public async Task<IActionResult> EditCollection(EditCollectionViewModel model)
        {
            if(ModelState.IsValid)
            {
                Collection newCol = appContext.Collections.FirstOrDefault(c => c.Id == model.CollectionId);
                User curUser = await userManager.GetUserAsync(User);
                if (newCol.UserId == curUser.Id || await userManager.IsInRoleAsync(curUser, "admin"))
                {
                    newCol.Name = model.Name;
                    newCol.briefDesc = model.BriefDesc;
                    newCol.imgUrl = model.ImgUrl;
                    await appContext.SaveChangesAsync();
                    return RedirectToAction("EditCollections", new { userId = model.UserId } );
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
        public IActionResult ViewCollection(string userId, int collectionId)
        {
            Collection col = appContext.Collections.FirstOrDefault(c => c.UserId == userId && c.Id == collectionId);
            List<Item> items = appContext.Items.Where(i => i.CollectionUserId == userId && i.CollectionId == collectionId).ToList();
            if (col.AddFieldsNamesId != Guid.Empty)
                ViewBag.AdditionalFieldsNames = appContext.AdditionalFieldsNames.FirstOrDefault(a => a.Id == col.AddFieldsNamesId).GetAllNames();
            else
                ViewBag.AdditionalFieldsNames = "";
            return View(new EditCollectionItemsViewModel(userId, collectionId, col.Name, EnumHelper.GetEnumDisplayName(col.Theme), items));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCollection(string userId, int collectionId)
        {
            if (!CommonHelpers.HasAccess(userId, userManager.GetUserId(User), User))
                return StatusCode(403);

            appContext.Collections.Remove(appContext.Collections.FirstOrDefault(c => c.UserId == userId && c.Id == collectionId));
            await appContext.SaveChangesAsync();
            ViewBag.userId = userId;
            return View("EditCollections", CommonHelpers.MakeUpUserCollectionsViewModel(userId, appContext));
        }              

        private int CalculateNewCollectionIndex(string userId)
        {
            List<Collection> colList = appContext.Collections.Where(p => p.UserId == userId).ToList();
            if (colList.Count > 0)
                return colList.Max(p => p.Id) + 1;
            else
                return 1;
        }        
    }
}
