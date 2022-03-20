using System.Globalization;
using System.Windows.Data;

namespace Vesuv.Editor.ValueConverters
{
    public class VersionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Version version || !(targetType.Equals(typeof(string)) || targetType.Equals(typeof(Object)))) {
                throw new InvalidOperationException("Invalid convert request");
            }
            if (parameter is string format) {
                return String.Format(culture, format, version);
            }
            return version.ToString(3);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
