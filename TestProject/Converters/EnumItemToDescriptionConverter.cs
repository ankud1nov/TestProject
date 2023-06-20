using System;
using System.Globalization;
using System.Windows;
using TestProject.Domain.Extensions;

namespace TestProject.Converters
{
    public class EnumItemToDescriptionConverter : ConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Enum) return null;

            var enumValue = value as Enum;

            return enumValue == null ? DependencyProperty.UnsetValue : enumValue.GetDescriptionFromEnumValue();
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

