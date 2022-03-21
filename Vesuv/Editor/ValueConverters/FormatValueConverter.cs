using System.Globalization;
using System.Windows.Data;

namespace Vesuv.Editor.ValueConverters
{
    internal class FormatValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string format) {
                return String.Format(culture, format, value);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
