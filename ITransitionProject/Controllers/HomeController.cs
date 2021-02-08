using ITransitionProject.Helpers;
using ITransitionProject.Models;
using ITransitionProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext appContext;
        public HomeController(ILogger<HomeController> logger, ApplicationContext appContext)
        {
            _logger = logger;
            this.appContext = appContext;
        }

        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.JsonTagsCloud = CommonHelpers.GetInitialTagsJson(appContext, 20);
            List<Collection> collections = CommonHelpers.GetBiggestCollections(appContext, 20);
            model.CollectionVMs = new List<UserCollectionsViewModel>();
            foreach(Collection col in collections)
            {
                model.CollectionVMs.Add(new UserCollectionsViewModel(col, EnumHelper.GetEnumDisplayName(col.Theme)));
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
