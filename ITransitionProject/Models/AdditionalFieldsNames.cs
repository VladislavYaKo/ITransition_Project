﻿using System;
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
        public AdditionalFieldsNames (string[] intFieldsNames)
        {
            this.NumericFieldsNames = intFieldsNames != null ? String.Join(",", intFieldsNames) : null;
        }
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

        public string GetAllNames()
        {
            return NumericFieldsNames ?? ""
                + SingleLineFieldsNames ?? ""
                + MultiLineFieldsNames ?? "" 
                + DateFieldsNames ?? ""
                + BooleanFieldsNames ?? "";
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
    }
}
