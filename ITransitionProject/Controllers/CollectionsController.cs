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

        public async Task<IActionResult> EditCollections()
        {
            string userId = userManager.GetUserId(User);
            List<Collection> collections = appContext.Collections.Where(coll => coll.UserId == userId).ToList();
            return View(collections);
        }

        [HttpGet]
        public IActionResult AddCollection()
        {
            ViewBag.Themes = Enum.GetNames(typeof(Collection.Themes)).ToList() as IEnumerable<string>;
            return View(new EditCollectionsViewModel { userId = userManager.GetUserId(User)});
        }

        [HttpPost]
        public async Task<IActionResult> AddCollection(EditCollectionsViewModel model, string[] intFieldName)
        {
            int userIntId = 0;
            if(ModelState.IsValid)
            {
                AdditionalFieldsNames afn = new AdditionalFieldsNames(intFieldName);
                Collection newCollection = new Collection { Name = model.Name, Theme = model.Theme, briefDesc = model.BriefDesc, imgUrl = model.ImgUrl, AddFieldsNames = afn };
                User user = await userManager.FindByIdAsync(model.userId);
                if (user.Collections == null)
                    user.Collections = new List<Collection>();
                user.Collections.Add(newCollection);
                await userManager.UpdateAsync(user);
                userIntId = user.intId;
                return RedirectToAction("EditCollections"); //добавить передачу id пользователя
            }
            else
            {
                ModelState.AddModelError("", "Введите все требуемые поля.");
            }
            return View(new EditCollectionsViewModel { userId = model.userId });
        }

        [HttpGet]
        public IActionResult EditCollection()
        {
            return View();
        }
    }
}
