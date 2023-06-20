using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Linq;

namespace TestProject.Domain.Extensions;

public static class EnumExtensions
{
    public static string GetDescriptionFromEnumValue(this Enum value)
    {
        var attribute = value.GetType()
            .GetField(value.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .SingleOrDefault() as DescriptionAttribute;
        return attribute == null ? value.ToString() : attribute.Description;
    }
    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }
}
