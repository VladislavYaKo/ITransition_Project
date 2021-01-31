using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    public class UniqueTag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [MaxLength(25)]
        public string TagValue { get; set; }
        public uint Usage { get; set; }

        public UniqueTag() { }
        public UniqueTag(string tag) => this.TagValue = tag;
    }
}
