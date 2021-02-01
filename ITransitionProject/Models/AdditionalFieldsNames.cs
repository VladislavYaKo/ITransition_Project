using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    //Названия полей отделяются через знак вопроса
    public class AdditionalFieldsNames
    {
        public AdditionalFieldsNames() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256)]
        public string NumericFieldsNames { get; set; }
        [MaxLength(256)]
        public string SingleLineFieldsNames { get; set; }
        [MaxLength(256)]
        public string MultiLineFieldsNames { get; set; }
        [MaxLength(256)]
        public string DateFieldsNames { get; set; }
        [MaxLength(256)]
        public string BooleanFieldsNames { get; set; }

        public void SetNumericFieldsNames(string[] nfn)
        {
            this.NumericFieldsNames = nfn != null ? String.Join(",", nfn) : null;
        }

        public void SetSingleLineFieldsNames(string[] slfn)
        {
            this.SingleLineFieldsNames = slfn != null ? String.Join(",", slfn) : null;
        }

        public void SetMultiLineFieldsNames(string[] mlfn)
        {
            this.MultiLineFieldsNames = mlfn != null ? String.Join(",", mlfn) : null;
        }

        public void SetDateFieldsNames(string[] dfn)
        {
            this.DateFieldsNames = dfn != null ? String.Join(",", dfn) : null;
        }

        public void SetBoolFieldsNames(string[] bfn)
        {
            this.BooleanFieldsNames = bfn != null ? String.Join(",", bfn) : null;
        }

        public string GetAllNames()
        {
            string result = NumericFieldsNames != null ? NumericFieldsNames + ", " : "";
            result += SingleLineFieldsNames != null ? SingleLineFieldsNames + ", " : "";
            result += MultiLineFieldsNames != null ? MultiLineFieldsNames + ", " : "";
            result += DateFieldsNames != null ? DateFieldsNames + ", " : "";
            result += BooleanFieldsNames != null ? BooleanFieldsNames : "";
            return result;
        }

        public static string GetAllNames(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.Find(id);
                return afn.GetAllNames();
            }

            return "";
        }

        public string[] GetNumericFieldsArray()
        {
            return NumericFieldsNames != null ? NumericFieldsNames.Split(',') : null;
        }

        public static string[] GetNumericFieldsArray(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.Find(id);
                return afn.GetNumericFieldsArray();
            }

            return null;
        }

        public string[] GetSingleLineFieldsArray()
        {
            return SingleLineFieldsNames != null ? SingleLineFieldsNames.Split(',') : null;
        }

        public static string[] GetSingleLineFieldsArray(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.Find(id);
                return afn.GetSingleLineFieldsArray();
            }

            return null;
        }

        public string[] GetMultiLineFieldsArray()
        {
            return MultiLineFieldsNames != null ? MultiLineFieldsNames.Split(',') : null;
        }

        public static string[] GetMultiLineFieldsArray(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.Find(id);
                return afn.GetMultiLineFieldsArray();
            }

            return null;
        }

        public string[] GetDateFieldsArray()
        {
            return DateFieldsNames != null ? DateFieldsNames.Split(',') : null;
        }

        public static string[] GetDateFieldsArray(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.Find(id);
                return afn.GetDateFieldsArray();
            }

            return null;
        }

        public string[] GetBooleanFieldsArray()
        {
            return BooleanFieldsNames != null ? BooleanFieldsNames.Split(',') : null;
        }

        public static string[] GetBooleanFieldsArray(ApplicationContext appContext, Guid id)
        {
            if (id != Guid.Empty)
            {
                AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.Find(id);
                return afn.GetBooleanFieldsArray();
            }

            return null;
        }
    }
}
