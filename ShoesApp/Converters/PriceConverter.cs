using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ShoesApp.Converters;

public class PriceConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var hasDiscount = value is int d && d > 0;
        var mode = parameter as string;

        return mode switch
        {
            "discount" => hasDiscount ? Visibility.Visible : Visibility.Collapsed,
            "normal" => hasDiscount ? Visibility.Collapsed : Visibility.Visible,
            _ => Visibility.Visible
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}