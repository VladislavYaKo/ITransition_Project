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

//EditCollection, post - обновление работает?
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
            return View(CommonHelpers.MakeUpUserCollectionsVM(userId, appContext));
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
                AdditionalFieldsNames afn = SetAdditionalFieldsNames(model.NumericFieldName, 
                    model.SingleLineFieldName, 
                    model.MultiLineFieldName, 
                    model.DateFieldName, 
                    model.BoolFieldName);
                Collection newCollection = new Collection {
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

        private AdditionalFieldsNames SetAdditionalFieldsNames(string[] numericFieldsNames,
            string[] SLFieldsNames,
            string[] MLFieldsNames,
            string[] dateFieldsNames,
            string[] boolFieldsNames)
        {
            bool filled = false;
            AdditionalFieldsNames result = new AdditionalFieldsNames();
            if (numericFieldsNames != null)
            {
                result.SetNumericFieldsNames(numericFieldsNames);
                filled = true;
            }
            if (SLFieldsNames != null)
            {
                result.SetSingleLineFieldsNames(SLFieldsNames);
                filled = true;
            }
            if (MLFieldsNames != null)
            {
                result.SetMultiLineFieldsNames(MLFieldsNames);
                filled = true;
            }
            if(dateFieldsNames != null)
            {
                result.SetDateFieldsNames(dateFieldsNames);
                filled = true;
            }
            if(boolFieldsNames != null)
            {
                result.SetBoolFieldsNames(boolFieldsNames);
                filled = true;
            }
            if (filled)
                return result;
            else
                return null;
        }

        [HttpGet]
        public IActionResult EditCollection(string userId, Guid collectionId)
        {
            Collection col = FindCollection(collectionId);
            return View(new EditCollectionViewModel { Name= col.Name, CollectionId = collectionId, UserId = col.UserId, BriefDesc = col.briefDesc , ImgUrl = col.imgUrl});
        }

        [HttpPost]
        public async Task<IActionResult> EditCollection(EditCollectionViewModel model)
        {
            if(ModelState.IsValid)
            {
                Collection newCol = FindCollection(model.CollectionId);
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
        public IActionResult ViewCollection(string userId, Guid collectionId)
        {
            Collection col = FindCollection(collectionId);
            List<Item> items = appContext.Items.Where(i => i.CollectionId == collectionId).ToList();
            string debug = AdditionalFieldsNames.GetAllNames(appContext, col.AddFieldsNamesId);
            ViewBag.AdditionalFieldsNames = AdditionalFieldsNames.GetAllNames(appContext, col.AddFieldsNamesId);
            return View(new EditCollectionItemsViewModel(userId, collectionId, col.Name, EnumHelper.GetEnumDisplayName(col.Theme), items));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCollection(string userId, Guid collectionId)
        {
            if (!CommonHelpers.HasAccess(userId, userManager.GetUserId(User), User))
                return StatusCode(403);

            appContext.Collections.Remove(await appContext.Collections.FindAsync(collectionId));
            await appContext.SaveChangesAsync();
            ViewBag.userId = userId;
            return View("EditCollections", CommonHelpers.MakeUpUserCollectionsVM(userId, appContext));
        }

        private Collection FindCollection(Guid collectionId)
        {
            return appContext.Collections.Find(collectionId);
        }
    }
}
