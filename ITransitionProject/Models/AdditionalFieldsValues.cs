using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    public class AdditionalFieldsValues
    {
        public AdditionalFieldsValues() { }

        public AdditionalFieldsValues(string[] numericFieldsValues)
        {
            this.NumericFieldsValues = numericFieldsValues != null ? String.Join(',', numericFieldsValues) : null;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
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

        public string[] GetNumericValuesArray()
        {
            return NumericFieldsValues != null ? NumericFieldsValues.Split(',') : null;
        }

        public static string[] GetNumericValuesArray(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsValues afv = appContext.AdditionalFieldsValues.Find(id);
                return afv.GetNumericValuesArray();
            }

            return null;
        }
    }
}
