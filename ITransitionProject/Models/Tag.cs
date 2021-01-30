using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    public class Tag
    {
        public string ItemCollectionUserId {get;set;}
        public int ItemId { get; set; }
        [MinLength(1)]
        [MaxLength(30)]
        public string TagValue { get; set; }

        public Tag() { }
        public Tag(string tag) => this.TagValue = tag;
    }
}
