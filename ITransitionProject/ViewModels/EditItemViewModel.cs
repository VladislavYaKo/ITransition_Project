using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.ViewModels
{
    public class EditItemViewModel
    {
        public string UserId { get; set; }
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }
        public string CollectionTheme { get; set; }
        public string[] NumericFieldsNames { get; set; }
        public string[] NumericFieldsValues { get; set; }
    }
}
