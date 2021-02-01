using ITransitionProject.Helpers;
using ITransitionProject.Models;
using ITransitionProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            Collection col = FindCollection(model.CollectionId);
            Item item = FindItem(model.ItemId);
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
                SLFieldsNames = AdditionalFieldsNames.GetSingleLineFieldsArray(appContext, col.AddFieldsNamesId),
                SLFieldsValues = AdditionalFieldsValues.GetSingleLineValuesArray(appContext, item.AddFieldsValuesId),
                MLFieldsNames = AdditionalFieldsNames.GetMultiLineFieldsArray(appContext, col.AddFieldsNamesId),
                MLFieldsValues = AdditionalFieldsValues.GetMultiLineValuesArray(appContext, item.AddFieldsValuesId),
                DateFieldsNames = AdditionalFieldsNames.GetDateFieldsArray(appContext, col.AddFieldsNamesId),
                DateFieldsValues = AdditionalFieldsValues.GetDateValuesArray(appContext, item.AddFieldsValuesId),
                BoolFieldsNames = AdditionalFieldsNames.GetBooleanFieldsArray(appContext, col.AddFieldsNamesId),
                BoolFieldsValues = AdditionalFieldsValues.GetBooleanValuesArray(appContext, item.AddFieldsValuesId),
                JsonTags = JsonConvert.SerializeObject(itemTags)
            }
            );
        }

        [HttpGet]
        public IActionResult AddItem(string UserId, Guid CollectionId, string CollectionName, string CollectionTheme)
        {
            if (!CommonHelpers.HasAccess(UserId, userManager.GetUserId(User), User))
                return StatusCode(403);

            Collection col = FindCollection(CollectionId);
            string[] NumericFieldsNames = AdditionalFieldsNames.GetNumericFieldsArray(appContext, col.AddFieldsNamesId);
            string[] SLFieldsNames = AdditionalFieldsNames.GetSingleLineFieldsArray(appContext, col.AddFieldsNamesId);
            string[] MLFieldsNames = AdditionalFieldsNames.GetMultiLineFieldsArray(appContext, col.AddFieldsNamesId);
            string[] DateFieldsNames = AdditionalFieldsNames.GetDateFieldsArray(appContext, col.AddFieldsNamesId);
            string[] BoolFieldsNames = AdditionalFieldsNames.GetBooleanFieldsArray(appContext, col.AddFieldsNamesId);

            return View(new EditItemViewModel
            {
                UserId = UserId,
                CollectionId = CollectionId,
                CollectionName = CollectionName,
                CollectionTheme = CollectionTheme,
                NumericFieldsNames = NumericFieldsNames,
                NumericFieldsValues = new string[NumericFieldsNames != null ? NumericFieldsNames.Length : 0],
                SLFieldsNames = SLFieldsNames,
                SLFieldsValues = new string[SLFieldsNames != null ? SLFieldsNames.Length : 0],
                MLFieldsNames = MLFieldsNames,
                MLFieldsValues = new string[MLFieldsNames != null ? MLFieldsNames.Length : 0],
                DateFieldsNames = DateFieldsNames,
                DateFieldsValues = new string[DateFieldsNames != null ? DateFieldsNames.Length : 0],
                BoolFieldsNames = BoolFieldsNames,
                BoolFieldsValues = new string[BoolFieldsNames != null ? BoolFieldsNames.Length : 0],
                JsonInitialTags = CommonHelpers.GetInitialTagsJson(appContext)
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(EditItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                string[] boolFieldsNames = Request.Form["BoolFieldsNames"].ToArray();
                List<string> tags = ParseJsonValues(model.JsonTags);
                AdditionalFieldsValues afv = SetAdditionalFieldsValues(model.NumericFieldsValues,
                    model.SLFieldsValues,
                    model.MLFieldsValues,
                    model.DateFieldsValues,
                    model.BoolFieldsValues,
                    model.BoolFieldsNames);
                Item newItem = new Item
                {
                    Name = model.Name,
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

        private AdditionalFieldsValues SetAdditionalFieldsValues(string[] numericFieldsValues,
            string[] SLFieldsValues,
            string[] MLFieldsvalues,
            string[] dateFieldsValues,
            string[] boolFieldsValues,
            string[] boolFieldsNames)
        {
            bool filled = false;
            AdditionalFieldsValues result = new AdditionalFieldsValues();
            if (numericFieldsValues != null)
            {
                result.SetNumericFieldsValues(numericFieldsValues);
                filled = true;
            }
            if(SLFieldsValues != null)
            {
                result.SetSingleLineFieldsValues(SLFieldsValues);
                filled = true;
            }
            if(MLFieldsvalues != null)
            {
                result.SetMultiLineFieldsValues(MLFieldsvalues);
                filled = true;
            }
            if(dateFieldsValues != null)
            {
                result.SetDateFieldsValues(dateFieldsValues);
                filled = true;
            }
            if (boolFieldsNames != null)
            {
                List<string> boolsStrs = new List<string>();
                foreach (string s in boolFieldsNames)
                {
                    bool? contains = boolFieldsValues?.Contains(s);
                    if (contains != null && (bool)contains)
                        boolsStrs.Add("True");
                    else
                        boolsStrs.Add("False");
                }
                result.SetBooleanFieldsValues(boolsStrs.ToArray());
                filled = true;
            }


            if (filled)
                return result;
            else
                return null;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteItem(EditCollectionItemsViewModel model, Guid itemId)
        {
            if (!CommonHelpers.HasAccess(model.UserId, userManager.GetUserId(User), User))
                return StatusCode(403);

            appContext.Items.Remove(FindItem(itemId));
            await appContext.SaveChangesAsync();
            return RedirectToAction("ViewCollection", "Collections", new { userId = model.UserId, collectionId = model.CollectionId });
        }

        [HttpGet]
        public IActionResult EditItem(EditItemViewModel model, int plug)  //Костыль
        {
            if (!CommonHelpers.HasAccess(model.UserId, userManager.GetUserId(User), User))
                return StatusCode(403);

            Collection col = FindCollection(model.CollectionId);
            Item item = FindItem(model.ItemId);
            if (item == null)
                return StatusCode(404);
            string[] NumericFieldsNames = AdditionalFieldsNames.GetNumericFieldsArray(appContext, col.AddFieldsNamesId);            
            string[] NumericFieldsValues = AdditionalFieldsValues.GetNumericValuesArray(appContext, item.AddFieldsValuesId);
            string[] SLFieldsNames = AdditionalFieldsNames.GetSingleLineFieldsArray(appContext, col.AddFieldsNamesId);
            string[] SLFieldsValues = AdditionalFieldsValues.GetSingleLineValuesArray(appContext, item.AddFieldsValuesId);
            string[] MLFieldsNames = AdditionalFieldsNames.GetMultiLineFieldsArray(appContext, col.AddFieldsNamesId);
            string[] MLFieldsValues = AdditionalFieldsValues.GetMultiLineValuesArray(appContext, item.AddFieldsValuesId);
            string[] DateFieldsNames = AdditionalFieldsNames.GetDateFieldsArray(appContext, col.AddFieldsNamesId);
            string[] DateFieldsValues =AdditionalFieldsValues.GetDateValuesArray(appContext, item.AddFieldsValuesId);
            string[] BoolFieldsNames = AdditionalFieldsNames.GetBooleanFieldsArray(appContext, col.AddFieldsNamesId);
            string[] BoolFieldsValues = AdditionalFieldsValues.GetBooleanValuesArray(appContext, item.AddFieldsValuesId);
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
                SLFieldsNames = SLFieldsNames,
                SLFieldsValues = SLFieldsValues,
                MLFieldsNames = MLFieldsNames,
                MLFieldsValues = MLFieldsValues,
                DateFieldsNames= DateFieldsNames,
                DateFieldsValues = DateFieldsValues,
                BoolFieldsNames = BoolFieldsNames,
                BoolFieldsValues = BoolFieldsValues,
                JsonTags = JsonConvert.SerializeObject(itemTags),
                JsonInitialTags = CommonHelpers.GetInitialTagsJson(appContext)
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(EditItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                Item item = FindItem(model.ItemId);
                item.Name = model.Name;
                UpdateAdditionalFieldsValues(
                    item,
                    model.NumericFieldsValues,
                    model.SLFieldsValues,
                    model.MLFieldsValues,
                    model.DateFieldsValues,
                    model.BoolFieldsValues,
                    model.BoolFieldsNames);
                List<string> tags = ParseJsonValues(model.JsonTags);
                UpdateTagsInDB(model.UserId, model.ItemId, tags);
                AddUniqueTags(tags);
                appContext.Items.Update(item);
                await appContext.SaveChangesAsync();
                return RedirectToAction("ViewCollection", "Collections", new { userId = model.UserId, collectionId = model.CollectionId });
            }
            else
                ModelState.AddModelError("", "Некорректно заполнены поля.");

            return View(model);
        }

        private void UpdateAdditionalFieldsValues(Item item,
            string[] numericFieldsValues,
            string[] SLFieldsValues,
            string[] MLFieldsvalues,
            string[] dateFieldsValues,
            string[] boolFieldsValues,
            string[] boolFieldsNames)
        {
            if (item.AddFieldsValuesId == Guid.Empty)
                return;

            AdditionalFieldsValues afv = appContext.AdditionalFieldsValues.Find(item.AddFieldsValuesId);
            if (numericFieldsValues != null)
            {
                afv.SetNumericFieldsValues(numericFieldsValues);
            }
            if (SLFieldsValues != null)
            {
                afv.SetSingleLineFieldsValues(SLFieldsValues);
            }
            if (MLFieldsvalues != null)
            {
                afv.SetMultiLineFieldsValues(MLFieldsvalues);
            }
            if (dateFieldsValues != null)
            {
                afv.SetDateFieldsValues(dateFieldsValues);
            }
            if (boolFieldsNames != null)
            {
                List<string> boolsStrs = new List<string>();
                foreach (string s in boolFieldsNames)
                {
                    bool? contains = boolFieldsValues?.Contains(s);
                    if (contains != null && (bool)contains)
                        boolsStrs.Add("True");
                    else
                        boolsStrs.Add("False");
                }
                afv.SetBooleanFieldsValues(boolsStrs.ToArray());
            }
            appContext.AdditionalFieldsValues.Update(afv);
        }

        [AllowAnonymous]
        public IActionResult TagFoundResult(string search)
        {
            List<Tag> foundTags = appContext.Tags.Where(t => t.TagValue.ToLower() == search.ToLower()).ToList();
            List<Item> foundItems = new List<Item>();
            foreach(Tag tag in foundTags)
            {
                Item item = FindItem(tag.ItemId);
                if (item != null)
                    foundItems.Add(item);
            }
            return View(MakeUpListFoundResultVM(foundItems));
        }

        [AllowAnonymous]
        public IActionResult FoundResult(string search)
        {

            var result1 = from i in appContext.Items
                         where EF.Functions.FreeText(i.Name, search)
                         select i;
            List<Item> result = new List<Item>();
            result = result1 != null ? result1.ToList() : result;

            var result2 = from c in appContext.Collections
                          where EF.Functions.FreeText(c.briefDesc, search)
                          select c.Id;
            foreach(Guid id in (result2 != null ? result2.ToList() : new List<Guid>()))
            {
                result.AddRange(appContext.Items.Where(i => i.CollectionId == id).ToList());
            }
            return View("TagFoundResult", MakeUpListFoundResultVM(result.ToList()));

        }

        private List<string> ParseJsonValues(string jsonStr)
        {
            var parsed = jsonStr != null ? JArray.Parse(jsonStr) : new JArray();
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
        
        private List<FoundResultViewModel> MakeUpListFoundResultVM(List<Item> items)
        {
            List<FoundResultViewModel> result = new List<FoundResultViewModel>();
            foreach(Item item in items)
            {
                FoundResultViewModel elem = new FoundResultViewModel();
                Collection itemCol = FindCollection(item.CollectionId);
                elem.Item = item;
                elem.CollectionName = itemCol.Name;
                elem.CollectionTheme = EnumHelper.GetEnumDisplayName(itemCol.Theme);
                if (itemCol.AddFieldsNamesId != Guid.Empty)
                    elem.AdditionalFields = appContext.AdditionalFieldsNames.Find(itemCol.AddFieldsNamesId).GetAllNames();
                else
                    elem.AdditionalFields = "";
                result.Add(elem);
            }
            return result;
        }

        private Collection FindCollection(Guid collectionId)
        {
            return appContext.Collections.Find(collectionId);
        }

        private Item FindItem(Guid itemId)
        {
            return appContext.Items.Find(itemId);
        }

        private List<string> FindTagsChanges(Guid itemId, List<string> newTags, out List<string> deletingTags)
        {
            List<Tag> oldTags = appContext.Tags.Where(t => t.ItemId == itemId).ToList();
            List<string> adding = newTags.Except(oldTags.Select(i => i.TagValue)).ToList();
            deletingTags = oldTags.Select(i => i.TagValue).Except(newTags).ToList();
            return adding;
        }

        private void UpdateTagsInDB(string userId, Guid itemId, List<string> newTags)
        {
            List<string> deleting = new List<string>();
            List<string> adding = FindTagsChanges(itemId, newTags, out deleting);
            List<Tag> addingTags = new List<Tag>();
            foreach (string value in adding)
                addingTags.Add(new Tag(userId, itemId, value));
            appContext.Tags.AddRange(addingTags);
            foreach (string value in deleting)
                appContext.Tags.Remove(appContext.Tags.Find(itemId, value));
        }
    }
}
