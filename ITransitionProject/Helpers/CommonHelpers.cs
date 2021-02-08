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
using System.Security.Claims;
using Newtonsoft.Json;

namespace ITransitionProject.Helpers
{
    public class CommonHelpers
    {
        private const int selectRange = 100000;
        public static bool HasAccess(string userId, string myUserId, ClaimsPrincipal User)
        {
            if (userId != myUserId && !User.IsInRole("admin"))
                return false;
            else
                return true;
        }

        public static IEnumerable<UserCollectionsViewModel> MakeUpUserCollectionsVM(string userId, ApplicationContext appContext)
        {
            List<UserCollectionsViewModel> model = new List<UserCollectionsViewModel>();
            foreach (Collection col in appContext.Collections.Where(col => col.UserId == userId))
            {
                model.Add(new UserCollectionsViewModel(col, EnumHelper.GetEnumDisplayName(col.Theme)));
            }
            return model;
        }

        public static string GetInitialTagsJson(ApplicationContext appContext)
        {
            string[] tags = appContext.UniqueTags.Select(t => t.TagValue).ToArray();
            return JsonConvert.SerializeObject(tags);
        }

        public static string GetInitialTagsJson(ApplicationContext appContext, int tagsNum)
        {            
            return JsonConvert.SerializeObject(FindSeveralMaxUniqueTag(appContext, tagsNum).Select(i => i.TagValue));  //При десериализации предполагается, что передан массив строк
        }

        private static List<UniqueTag> FindSeveralMaxUniqueTag(ApplicationContext appContext, int tagsNum)
        {
            int i = 0;
            List<UniqueTag> result = new List<UniqueTag>();
            List<UniqueTag> buf = new List<UniqueTag>();
            int elemCount = appContext.UniqueTags.Count();
            while (i < elemCount)
            {
                int range = selectRange;
                if (i + selectRange > elemCount)
                    range = elemCount - i;
                buf = FindSeveralMax<UniqueTag, uint>(appContext.UniqueTags.ToList().GetRange(i, range), range, i => i.Usage);
                buf.AddRange(result);
                result = buf.OrderByDescending(i => i.Usage).ToList().GetRange(0, range);
                i += range;
            }
            return result;
        }

        private static List<T> FindSeveralMax<T, TKey>(List<T> src, int itemsNum, Func<T, TKey> keySelector)
        {
            return src.OrderByDescending(keySelector).ToList().GetRange(0, itemsNum);
        }

        public static List<Collection> GetBiggestCollections(ApplicationContext appContext, int collectionsNum)
        {
            int i = 0;
            int elemCount = appContext.Collections.Count();
            List<Collection> result = new List<Collection>();
            List<Collection> buf = new List<Collection>();
            while (i < elemCount)
            {
                int range = selectRange;
                if (i + selectRange > elemCount)
                    range = elemCount - i;
                buf = FindSeveralMax<Collection, int>(appContext.Collections.ToList().GetRange(i, range), range, c => c.ItemsCount);
                buf.AddRange(result);
                result = buf.OrderByDescending(c => c.ItemsCount).ToList().GetRange(0, range);
                i += range;
            }
            return result;
        }
    }
}
