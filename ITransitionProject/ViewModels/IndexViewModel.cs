using ITransitionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.ViewModels
{
    public class IndexViewModel
    {
        public List<UserCollectionsViewModel> CollectionVMs { get; set; }
        public string JsonTagsCloud { get; set; }
    }
}
