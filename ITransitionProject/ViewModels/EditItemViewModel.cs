using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.ViewModels
{
    public class EditItemViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int CollectionId { get; set; }
        public int ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        public string CollectionName { get; set; }
        public string CollectionTheme { get; set; }
        public string[] NumericFieldsNames { get; set; }
        public string[] NumericFieldsValues { get; set; }       
    }
}
