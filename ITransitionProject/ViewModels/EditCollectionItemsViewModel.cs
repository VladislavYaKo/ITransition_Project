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
        public EditCollectionItemsViewModel(string userId, int colId, string colName, string colTheme, List<Item> items)
        {
            this.userId = userId;
            this.colId = colId;
            this.CollectionName = colName;
            this.CollectionTheme = colTheme;
            this.Items = items;
        }
        public string userId { get; set; }
        public int colId { get; set; }
        public string CollectionName { get; set; }
        public string CollectionTheme { get; set; }
        public List<Item> Items { get; set; }
    }
}
