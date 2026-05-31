using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using ShoesApp.Models;

namespace ShoesApp.Converters;

public class RawBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Product p)
        {
            return Brushes.Transparent;
        }

        if (p.Quantity == 0)
        {
            return new SolidColorBrush(Color.FromRgb(0xAD, 0xD8, 0xE6));
        }

        if (p.Discount > 17)
        {
            return new SolidColorBrush(Color.FromRgb(0xFF, 0xDE, 0xAD));
        }

        return Brushes.Transparent;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}