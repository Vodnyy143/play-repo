using System.Globalization;
using System.Windows.Data;
using ShoesApp.Models;

namespace ShoesApp.Converters;

public class FinalPriceConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Product p)
        {
            return p.Price - p.Price * p.Discount / 100m;
        }

        return 0m;
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        => throw new NotImplementedException();
    
}