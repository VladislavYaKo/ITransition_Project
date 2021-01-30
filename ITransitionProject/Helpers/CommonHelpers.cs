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
        public static bool HasAccess(string userId, string myUserId, ClaimsPrincipal User)
        {
            if (userId != myUserId && !User.IsInRole("admin"))
                return false;
            else
                return true;
        }

        public static IEnumerable<UserCollectionsViewModel> MakeUpUserCollectionsViewModel(string userId, ApplicationContext appContext)
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
    }
}
