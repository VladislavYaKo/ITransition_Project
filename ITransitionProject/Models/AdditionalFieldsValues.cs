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

        public void SetNumericFieldsValues(string[] nfv)
        {
            this.NumericFieldsValues = nfv != null ? String.Join(',', nfv) : null;
        }

        public void SetSingleLineFieldsValues(string[] slfv)
        {
            this.SingleLineFieldsValues = slfv != null ? String.Join(',', slfv) : null;
        }

        public void SetMultiLineFieldsValues(string[] mlfv)
        {
            this.MultiLineFieldsValues = mlfv != null ? String.Join(',', mlfv) : null;
        }

        public void SetDateFieldsValues(string[] dfv)
        {
            this.DateFieldsValues = dfv != null ? String.Join(',', dfv) : null;
        }

        public void SetBooleanFieldsValues(string[] bfv)
        {
            this.BooleanFieldsValues = bfv != null ? String.Join(',', bfv) : null;
        }

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

        public string[] GetSingleLineValuesArray()
        {
            return SingleLineFieldsValues != null ? SingleLineFieldsValues.Split(',') : null;
        }

        public static string[] GetSingleLineValuesArray(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsValues afv = appContext.AdditionalFieldsValues.Find(id);
                return afv.GetSingleLineValuesArray();
            }

            return null;
        }

        public string[] GetMultiLineValuesArray()
        {
            return MultiLineFieldsValues != null ? MultiLineFieldsValues.Split(',') : null;
        }

        public static string[] GetMultiLineValuesArray(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsValues afv = appContext.AdditionalFieldsValues.Find(id);
                return afv.GetMultiLineValuesArray();
            }

            return null;
        }

        public string[] GetDateValuesArray()
        {
            return DateFieldsValues != null ? DateFieldsValues.Split(',') : null;
        }

        public static string[] GetDateValuesArray(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsValues afv = appContext.AdditionalFieldsValues.Find(id);
                return afv.GetDateValuesArray();
            }

            return null;
        }

        public string[] GetBooleanValuesArray()
        {
            return BooleanFieldsValues != null ? BooleanFieldsValues.Split(',') : null;
        }

        public static string[] GetBooleanValuesArray(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsValues afv = appContext.AdditionalFieldsValues.Find(id);
                return afv.GetBooleanValuesArray();
            }

            return null;
        }
    }
}
