using ITransitionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.ViewModels
{
    public class FoundResultViewModel
    {
        public Item Item { get; set; }
        public string AdditionalFields { get; set; }
        public string CollectionName { get; set; }
        public string CollectionTheme { get; set; }
    }
}
