using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ShoesApp.Converters;

public class PhotoConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var fileName = value as string;

        string fullPath;
        if (!string.IsNullOrEmpty(fileName) && File.Exists(Path.Combine(AppContext.BaseDirectory, "Resources", fileName)))
        {
            fullPath = Path.Combine(AppContext.BaseDirectory, "Resources", fileName);
        }
        else
        {
            fullPath = Path.Combine(AppContext.BaseDirectory, "Resources", "picture.png");
        }

        var bmp = new BitmapImage();
        bmp.BeginInit();
        bmp.CacheOption = BitmapCacheOption.OnLoad;
        bmp.UriSource = new Uri(fullPath, UriKind.Absolute);
        bmp.EndInit();
        return bmp;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}