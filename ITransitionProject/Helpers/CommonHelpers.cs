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
    }
}
