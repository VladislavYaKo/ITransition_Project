using ITransitionProject.Helpers;
using ITransitionProject.Models;
using ITransitionProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        [AllowAnonymous]
        public IActionResult ViewItem(EditItemViewModel model)
        {
            Collection col = appContext.Collections.FirstOrDefault(c => c.UserId == model.UserId && c.Id == model.CollectionId);
            Item item = appContext.Items.FirstOrDefault(i => i.CollectionUserId == model.UserId && i.Id == model.ItemId);
            AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.FirstOrDefault(a => a.Id == col.AddFieldsNamesId);
            AdditionalFieldsValues afv = appContext.AdditionalFieldsValues.FirstOrDefault(a => a.Id == item.AddFieldsValuesId);
            List<string> itemTags = appContext.Tags.Where(t => t.ItemCollectionUserId == model.UserId && t.ItemId == model.ItemId).Select(t => t.TagValue).ToList();
            return View(new EditItemViewModel
            {
                Name = item.Name,
                UserId = model.UserId,
                CollectionId = model.CollectionId,
                CollectionName = model.CollectionName,
                CollectionTheme = model.CollectionTheme,
                NumericFieldsNames = afn.GetNumericFieldsArray(),
                NumericFieldsValues = afv.GetNumericValuesArray(),
                JsonTags = JsonConvert.SerializeObject(itemTags)
            }
            );
        }

        [HttpGet]
        public IActionResult AddItem(string UserId, int CollectionId, string CollectionName, string CollectionTheme)
        {
            if (!CommonHelpers.HasAccess(UserId, userManager.GetUserId(User), User))
                return StatusCode(403);

            Collection col = appContext.Collections.FirstOrDefault(c => c.UserId == UserId && c.Id == CollectionId);
            AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.FirstOrDefault(a => a.Id == col.AddFieldsNamesId);
            string[] NumericFieldsNames;
            if (afn.NumericFieldsNames != null)
                NumericFieldsNames = col.AddFieldsNames.NumericFieldsNames.Split(',');
            else
                NumericFieldsNames = new string[0];
            
            return View(new EditItemViewModel
            {
                UserId = UserId,
                CollectionId = CollectionId,
                CollectionName = CollectionName,
                CollectionTheme = CollectionTheme,
                NumericFieldsNames = NumericFieldsNames,
                NumericFieldsValues = new string[NumericFieldsNames.Length],
                JsonInitialTags = CommonHelpers.GetInitialTagsJson(appContext)
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(EditItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> tags = ParseJsonValues(model.JsonTags);
                AdditionalFieldsValues afv = new AdditionalFieldsValues(model.NumericFieldsValues);
                Item newItem = new Item
                {
                    Name = model.Name,
                    Id = CalculateNewItemIndex(model.UserId),
                    CollectionId = model.CollectionId,
                    CollectionUserId = model.UserId,
                    AddFieldsValues = afv
                };
                newItem.SetTags(tags);
                AddUniqueTags(tags);
                await appContext.Items.AddAsync(newItem);
                await appContext.SaveChangesAsync();
                return RedirectToAction("ViewCollection", "Collections", new { userId = model.UserId, collectionId = model.CollectionId });
            }
            else
                ModelState.AddModelError("", "Некорректно заполнены поля.");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteItem(EditCollectionItemsViewModel model, int itemId)
        {
            if (!CommonHelpers.HasAccess(model.UserId, userManager.GetUserId(User), User))
                return StatusCode(403);

            appContext.Items.Remove(appContext.Items.FirstOrDefault(i => i.CollectionUserId == model.UserId && i.Id == itemId));
            await appContext.SaveChangesAsync();
            return RedirectToAction("ViewCollection", "Collections", new { userId = model.UserId, collectionId = model.CollectionId });
        }

        [HttpGet]
        public IActionResult EditItem(EditItemViewModel model, int plug)  //Костыль
        {
            if (!CommonHelpers.HasAccess(model.UserId, userManager.GetUserId(User), User))
                return StatusCode(403);

            Collection col = appContext.Collections.FirstOrDefault(c => c.UserId == model.UserId && c.Id == model.CollectionId);
            Item item = appContext.Items.FirstOrDefault(i => i.CollectionUserId == model.UserId && i.Id == model.ItemId);
            string[] NumericFieldsNames = null;
            if (col.AddFieldsNamesId != Guid.Empty)
            {
                AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.FirstOrDefault(a => a.Id == col.AddFieldsNamesId);
                if (afn.NumericFieldsNames != null)
                    NumericFieldsNames = afn.GetNumericFieldsArray();
            }
            string[] NumericFieldsValues = null;
            if (col.AddFieldsNamesId != Guid.Empty)
            {
                AdditionalFieldsValues afv = appContext.AdditionalFieldsValues.FirstOrDefault(a => a.Id == item.AddFieldsValuesId);
                if (afv.NumericFieldsValues != null)
                    NumericFieldsValues = afv.GetNumericValuesArray();
            }
            List<string> itemTags = appContext.Tags.Where(t => t.ItemCollectionUserId == model.UserId && t.ItemId == model.ItemId).Select(t => t.TagValue).ToList();

            return View(new EditItemViewModel
            {
                UserId = model.UserId,
                CollectionId = model.CollectionId,
                ItemId = model.ItemId,
                Name = item.Name,
                CollectionName = model.CollectionName,
                CollectionTheme = model.CollectionTheme,
                NumericFieldsNames = NumericFieldsNames,
                NumericFieldsValues = NumericFieldsValues,
                JsonTags = JsonConvert.SerializeObject(itemTags),
                JsonInitialTags = CommonHelpers.GetInitialTagsJson(appContext)
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(EditItemViewModel model)
        {
            Item item = appContext.Items.FirstOrDefault(i => i.CollectionUserId == model.UserId && i.Id == model.ItemId);
            item.Name = model.Name;
            item.AddFieldsValues = new AdditionalFieldsValues(model.NumericFieldsValues);
            item.SetTags(ParseJsonValues(model.JsonTags));
            appContext.Items.Update(item);
            await appContext.SaveChangesAsync();
            return RedirectToAction("ViewCollection", "Collections", new { userId = model.UserId, collectionId = model.CollectionId });
        }

        public IActionResult TagFoundResult(string search)
        {
            List<Tag> foundTags = appContext.Tags.Where(t => t.TagValue.ToLower() == search.ToLower()).ToList();
            List<Item> foundItems = new List<Item>();
            foreach(Tag tag in foundTags)
            {
                Item item = appContext.Items.Find(tag.ItemId, tag.ItemCollectionUserId);
                if (item != null)
                    foundItems.Add(item);
            }
            return View(MakeUpFoundResultVM(foundItems));
        }

        private int CalculateNewItemIndex(string userId)
        {
            List<Item> items = appContext.Items.Where(i => i.CollectionUserId == userId).ToList();
            if (items.Count > 0)
                return items.Max(i => i.Id) + 1;
            else
                return 1;
        }

        private List<string> ParseJsonValues(string jsonStr)
        {
            var parsed = JArray.Parse(jsonStr);
            List<string> result = new List<string>();
            foreach(JToken jt in parsed)
            {
                result.Add(jt["value"].ToString());
            }
            return result;
        }

        private void AddUniqueTags(List<string> tags)
        {
            foreach(string tag in tags)
            {
                string normTag = tag.ToLower();
                normTag = Char.ToUpper(normTag[0]) + normTag.Substring(1);
                if (!appContext.UniqueTags.Select(i => i.TagValue).Contains(normTag))
                    appContext.UniqueTags.Add(new UniqueTag(normTag));
                else
                    appContext.UniqueTags.Find(normTag).Usage++;
            }
        }  
        
        private List<FoundResultViewModel> MakeUpFoundResultVM(List<Item> items)
        {
            List<FoundResultViewModel> result = new List<FoundResultViewModel>();
            foreach(Item item in items)
            {
                FoundResultViewModel elem = new FoundResultViewModel();
                Collection itemCol = appContext.Collections.Find(item.CollectionId, item.CollectionUserId);
                elem.Item = item;
                elem.CollectionName = itemCol.Name;
                elem.CollectionTheme = EnumHelper.GetEnumDisplayName(itemCol.Theme);
                if (itemCol.AddFieldsNamesId != Guid.Empty)
                    elem.AdditionalFields = appContext.AdditionalFieldsNames.FirstOrDefault(a => a.Id == itemCol.AddFieldsNamesId).GetAllNames();
                else
                    elem.AdditionalFields = "";
                result.Add(elem);
            }
            return result;
        }
    }
}
