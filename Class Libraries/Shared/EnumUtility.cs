using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shared
{
    public static class EnumUtility
    {
        public static T Parse<T>(string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<T> GetValues<T>()
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(typeof(T), fi.Name, false));
            }
            return enumValues;
        }

        public static IList<string> GetNames<T>() where T : Enum
        {
            return typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues<T>() where T : Enum
        {
            return GetNames<T>().Select(obj => GetDisplayValue(Parse<T>(obj))).ToList();
        }

        public static string GetDisplayValue(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null)
                return value.ToString();

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;

            if (descriptionAttributes.Length > 0 && descriptionAttributes[0].ResourceType != null)
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey, DefaultSettings.CultureInfo);
                }
            }

            return resourceKey; // Fallback with the key name
        }
    }
}
