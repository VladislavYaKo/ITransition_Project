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
            Collection col = FindCollection(model.CollectionId, model.UserId);
            Item item = FindItem(model.ItemId, model.UserId);
            List<string> itemTags = appContext.Tags.Where(t => t.ItemCollectionUserId == model.UserId && t.ItemId == model.ItemId).Select(t => t.TagValue).ToList();
            return View(new EditItemViewModel
            {
                Name = item.Name,
                UserId = model.UserId,
                CollectionId = model.CollectionId,
                CollectionName = model.CollectionName,
                CollectionTheme = model.CollectionTheme,
                NumericFieldsNames = AdditionalFieldsNames.GetNumericFieldsArray(appContext, col.AddFieldsNamesId),
                NumericFieldsValues = AdditionalFieldsValues.GetNumericValuesArray(appContext, item.AddFieldsValuesId),
                JsonTags = JsonConvert.SerializeObject(itemTags)
            }
            );
        }

        [HttpGet]
        public IActionResult AddItem(string UserId, int CollectionId, string CollectionName, string CollectionTheme)
        {
            if (!CommonHelpers.HasAccess(UserId, userManager.GetUserId(User), User))
                return StatusCode(403);

            Collection col = FindCollection(CollectionId, UserId);
            string[] NumericFieldsNames = AdditionalFieldsNames.GetNumericFieldsArray(appContext, col.AddFieldsNamesId);

            return View(new EditItemViewModel
            {
                UserId = UserId,
                CollectionId = CollectionId,
                CollectionName = CollectionName,
                CollectionTheme = CollectionTheme,
                NumericFieldsNames = NumericFieldsNames,
                NumericFieldsValues = new string[NumericFieldsNames != null ? NumericFieldsNames.Length : 0],
                JsonInitialTags = CommonHelpers.GetInitialTagsJson(appContext)
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(EditItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> tags = ParseJsonValues(model.JsonTags);
                AdditionalFieldsValues afv = null;
                if (model.NumericFieldsValues.Length > 0)
                    afv = new AdditionalFieldsValues(model.NumericFieldsValues);
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

            appContext.Items.Remove(FindItem(itemId, model.UserId));
            await appContext.SaveChangesAsync();
            return RedirectToAction("ViewCollection", "Collections", new { userId = model.UserId, collectionId = model.CollectionId });
        }

        [HttpGet]
        public IActionResult EditItem(EditItemViewModel model, int plug)  //Костыль
        {
            if (!CommonHelpers.HasAccess(model.UserId, userManager.GetUserId(User), User))
                return StatusCode(403);

            Collection col = FindCollection(model.CollectionId, model.UserId);
            Item item = FindItem(model.ItemId, model.UserId);
            string[] NumericFieldsNames = AdditionalFieldsNames.GetNumericFieldsArray(appContext, col.AddFieldsNamesId);            
            string[] NumericFieldsValues = AdditionalFieldsValues.GetNumericValuesArray(appContext, item.AddFieldsValuesId);
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
            Item item = FindItem(model.ItemId, model.UserId);
            item.Name = model.Name;
            item.AddFieldsValues = new AdditionalFieldsValues(model.NumericFieldsValues);
            List<string> tags = ParseJsonValues(model.JsonTags);
            UpdateTagsInDB(model.UserId, model.ItemId, tags);
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
                Item item = FindItem(tag.ItemId, tag.ItemCollectionUserId);
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
                Collection itemCol = FindCollection(item.CollectionId, item.CollectionUserId);
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

        private Collection FindCollection(int collectionId, string userId)
        {
            return appContext.Collections.Find(collectionId, userId);
        }

        private Item FindItem(int itemId, string userId)
        {
            return appContext.Items.Find(itemId, userId);
        }

        private List<string> FindTagsChanges(string userId, int itemId, List<string> newTags, out List<string> deletingTags)
        {
            List<Tag> oldTags = appContext.Tags.Where(t => t.ItemCollectionUserId == userId && t.ItemId == itemId).ToList();
            List<string> adding = newTags.Except(oldTags.Select(i => i.TagValue)).ToList();
            deletingTags = oldTags.Select(i => i.TagValue).Except(newTags).ToList();
            return adding;
        }

        private void UpdateTagsInDB(string userId, int itemId, List<string> newTags)
        {
            List<string> deleting = new List<string>();
            List<string> adding = FindTagsChanges(userId, itemId, newTags, out deleting);
            List<Tag> addingTags = new List<Tag>();
            foreach (string value in adding)
                addingTags.Add(new Tag(userId, itemId, value));
            appContext.Tags.AddRange(addingTags);
            foreach (string value in deleting)
                appContext.Tags.Remove(appContext.Tags.Find(userId, itemId, value));
        }
    }
}
