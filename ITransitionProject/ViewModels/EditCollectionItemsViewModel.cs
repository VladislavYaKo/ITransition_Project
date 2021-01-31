using ITransitionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.ViewModels
{
    public class EditCollectionItemsViewModel
    {
        public EditCollectionItemsViewModel() { }
        public EditCollectionItemsViewModel(string userId, Guid colId, string colName, string colTheme, List<Item> items)
        {
            this.UserId = userId;
            this.CollectionId = colId;
            this.CollectionName = colName;
            this.CollectionTheme = colTheme;
            this.Items = items;
        }
        public string UserId { get; set; }
        public Guid CollectionId { get; set; }
        public string CollectionName { get; set; }
        public string CollectionTheme { get; set; }
        public List<Item> Items { get; set; }
    }
}
