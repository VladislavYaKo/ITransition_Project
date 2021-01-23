using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ITransitionProject.Helpers
{
    public class EnumHelper
    {
        public static string GetEnumDisplayName(Enum item)
        {
            Type type = item.GetType();
            MemberInfo member = type.GetMember(item.ToString()).FirstOrDefault();
            DisplayAttribute displayAttribute = (DisplayAttribute)member.GetCustomAttribute(typeof(DisplayAttribute));
            if (displayAttribute != null)
                return displayAttribute.Name;
            return null;
        }
    }
}
