using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Vesuv.Editor.ValueConverters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool isVisible || !targetType.Equals(typeof(Visibility))) {
                throw new InvalidOperationException("Invalid convert request");
            }
            return isVisible
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Visibility visibility || !targetType.Equals(typeof(bool))) {
                throw new InvalidOperationException("Invalid convert back request");
            }
            return visibility == Visibility.Visible;
        }
    }
}
