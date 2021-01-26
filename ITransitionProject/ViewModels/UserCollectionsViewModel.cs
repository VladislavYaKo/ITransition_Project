using ITransitionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.ViewModels
{
    public class UserCollectionsViewModel
    {
        public UserCollectionsViewModel () { }
        public UserCollectionsViewModel(Collection collection, string theme)
        {
            this.Collection = collection;
            this.Theme = theme;
        }
        public Collection Collection { get; set; }
        public string Theme { get; set; }
    }
}
