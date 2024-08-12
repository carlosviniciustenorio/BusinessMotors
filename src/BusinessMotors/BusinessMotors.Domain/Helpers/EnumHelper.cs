using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BusinessMotors.Domain.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayName(object value)
        {
            var enumType = value.GetType();
            var enumMemberInfo = enumType.GetMember(value.ToString());
            if (enumMemberInfo.Length > 0)
            {
                var displayAttribute = enumMemberInfo[0].GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }
            return value.ToString();
        }
    }
}