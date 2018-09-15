using System;
using System.Collections.Generic;
using System.Linq;

namespace SrvCornet.Utils
{
    public static class ObjectUtils
    {
        public static List<string> GetProperyNames(Type type)
        {
            var properties = type.GetProperties();
            var propertyNames = properties.Select(s => s.Name).ToList();
            return propertyNames;
        }
        public static Dictionary<string, Type> GetPropertyTypeMap(Type type)
        {
            Dictionary<string, Type> result = new Dictionary<string, Type>();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                if (property.DeclaringType.Equals(type))
                    result.Add(property.Name, property.PropertyType);
                else if (!result.ContainsKey(propertyName))
                    result.Add(property.Name, property.PropertyType);
            }
            return result;
        }
    }
}
