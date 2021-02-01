using ITransitionProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.ViewModels
{
    public class EditCollectionViewModel
    {
        public string UserId { get; set; }
        public Guid CollectionId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public Collection.Themes Theme { get; set; }
        [Required]
        [MaxLength(128)]
        public string BriefDesc { get; set; }
        [MaxLength(100)]
        public string ImgUrl { get; set; }
        public string[] NumericFieldName { get; set; }
        public string[] SingleLineFieldName { get; set; }
        public string[] MultiLineFieldName { get; set; }
        public string[] DateFieldName { get; set; }
        public string[] BoolFieldName { get; set; }
    }
}
