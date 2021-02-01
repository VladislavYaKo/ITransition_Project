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
        public Guid CollectionId { get; set; }
        public Guid ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        public string CollectionName { get; set; }
        public string CollectionTheme { get; set; }
        public string[] NumericFieldsNames { get; set; }
        public string[] NumericFieldsValues { get; set; }
        public string[] SLFieldsNames { get; set; }
        public string[] SLFieldsValues { get; set; }
        public string[] MLFieldsNames { get; set; }
        public string[] MLFieldsValues { get; set; }
        public string[] DateFieldsNames { get; set; }
        public string[] DateFieldsValues { get; set; }
        public string[] BoolFieldsNames { get; set; }
        public string[] BoolsFieldsValues { get; set; }

        public string JsonInitialTags { get; set; }
        public string JsonTags { get; set; }
    }
}
