using ITransitionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.ViewModels
{
    public class EditCollectionViewModel
    {
        public string CollectionName { get; set; }
        public string CollectionTheme { get; set; }
        public List<Item> Items { get; set; }
    }
}
