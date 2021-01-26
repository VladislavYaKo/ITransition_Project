using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    public class Item
    {
        [Column(Order = 0)]
        public int Id { get; set; }
        [Column(Order = 1)]
        public string CollectionUserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        //Tags
        public int CollectionId { get; set; }        
        public AdditionalFieldsValues AddFieldsValues { get; set; }
        [ForeignKey("AddFieldsValues")]
        public Guid AddFieldsValuesId { get; set; }
    }
}
