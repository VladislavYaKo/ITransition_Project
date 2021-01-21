using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    public class AdditionalFieldsValues
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string NumericFieldsValues { get; set; }
        [MaxLength(800)]
        public string SingleLineFieldsValues { get; set; }
        [MaxLength(3100)]
        public string MultiLineFieldsValues { get; set; }
        [MaxLength(100)]
        public string DateFieldsValues { get; set; }
        [MaxLength(40)]
        public string BooleanFieldsValues { get; set; }
    }
}
