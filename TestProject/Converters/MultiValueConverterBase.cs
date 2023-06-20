using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;

namespace TestProject.Converters;

[MarkupExtensionReturnType(typeof(IMultiValueConverter))]
public abstract class MultiValueConverterBase : MarkupExtension, IMultiValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    protected abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
    protected abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);

    object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            return Convert(values, targetType, parameter, culture);
        }
        catch
        {
            return DependencyProperty.UnsetValue;
        }
    }

    object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        try
        {
            return ConvertBack(value, targetTypes, parameter, culture);
        }
        catch
        {
            return null;
        }
    }
}
