using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    public class Collection
    {
        public enum Themes
        {
            Books,
            Cars,
            Alcohol,
            Phones,
            Clothes,
        }
        public int Id { get; set; }
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

    }
}
