using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Vesuv.Editor.ValueConverters
{
    internal class BooleanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool boolean || !(targetType.Equals(typeof(string)) || targetType.Equals(typeof(Object)))) {
                throw new InvalidOperationException("Invalid convert request");
            }
            if (parameter is string format) {
                return String.Format(culture, format, boolean);
            }
            return boolean
                ? "True"
                : "False";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
