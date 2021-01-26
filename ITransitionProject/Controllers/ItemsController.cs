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
    [Authorize(Roles = "user,admin")]
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
            AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.FirstOrDefault(a => a.Id == col.AddFieldsNamesId);
            string[] NumericFieldsNames;
            if (afn.NumericFieldsNames != null)
                NumericFieldsNames = col.AddFieldsNames.NumericFieldsNames.Split(',');
            else
                NumericFieldsNames = new string[0];

            return View(new EditItemViewModel { 
                UserId = userId, 
                CollectionId = colId, 
                CollectionName = CollectionName, 
                CollectionTheme = CollectionTheme, 
                NumericFieldsNames = NumericFieldsNames,
                NumericFieldsValues = new string[NumericFieldsNames.Length]
            });
        }

        [HttpPost]
        public async Task< IActionResult >AddItem(EditItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                AdditionalFieldsValues afv = new AdditionalFieldsValues(model.NumericFieldsValues);
                Item newItem = new Item
                {
                    Name = model.Name,
                    Id = CalculateNewItemIndex(model.UserId, model.CollectionId),
                    CollectionId = model.CollectionId,
                    CollectionUserId = model.UserId,
                    AddFieldsValues = afv
                };
                await appContext.Items.AddAsync(newItem);
                return RedirectToAction("ViewCollection", "Collections", new { userId = model.UserId, colId = model.CollectionId });
            }
            else
                ModelState.AddModelError("", "Некорректно заполнены поля.");

            return View(model);
        }

        private int CalculateNewItemIndex(string userId, int collectionId)
        {
            List<Item> items = appContext.Items.Where(i => i.CollectionUserId == userId && i.CollectionId == collectionId).ToList();
            if (items.Count > 0)
                return items.Max(i => i.Id) + 1;
            else
                return 1;
        }
    }
}
