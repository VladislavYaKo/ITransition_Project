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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Column(Order = 1)]
        public string CollectionUserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }        
        public Guid CollectionId { get; set; }        
        public AdditionalFieldsValues AddFieldsValues { get; set; }
        [ForeignKey("AddFieldsValues")]
        public Guid AddFieldsValuesId { get; set; }
        public List<Tag> Tags { get; set; }

        public void SetTags(List<string> newTags)
        {
            if (Tags == null)
                Tags = new List<Tag>();
            foreach(string tag in newTags)
            {
                Tags.Add(new Tag(tag));
            }
        }
    }
}
