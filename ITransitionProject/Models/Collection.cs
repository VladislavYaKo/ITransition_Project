using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    public class Collection
    {
        public enum Themes
        {
            [Display(Name = "Книги")]
            Books,
            [Display(Name = "Машины")]
            Cars,
            [Display(Name = "Алкоголь")]
            Alcohol,
            [Display(Name = "Телефоны")]
            Phones,
            [Display(Name = "Одежда")]
            Clothes,
        }
        public int Id { get; set; }
        //public int ForChangeId { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public Themes Theme { get; set; }
        [MaxLength(100)]
        public string imgUrl { get; set; }
        [Required]
        [MaxLength(128)]
        public string briefDesc { get; set; }
        public List<Item> Items { get; set; }        
        //public AdditionalFieldsNames AddFieldsNames { get; set; }

    }
}
