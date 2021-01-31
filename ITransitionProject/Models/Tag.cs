using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    public class Tag
    {
        [Column(Order = 0)]
        public string ItemCollectionUserId {get;set;}
        [Column(Order = 1)]
        public Guid ItemId { get; set; }
        [MinLength(1)]
        [MaxLength(30)]
        [Column(Order = 2)]
        public string TagValue { get; set; }

        public Tag() { }
        public Tag(string tag) => this.TagValue = tag;
        public Tag(string userId, Guid itemId, string tag)
        {
            this.ItemCollectionUserId = userId;
            this.ItemId = itemId;
            this.TagValue = tag;
        }
    }
}
