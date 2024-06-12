using System.Globalization;


namespace Emre.Servisler
{
        public class IconConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var iconCode = value.ToString();
                return $"https://openweathermap.org/img/w/{iconCode}.png";
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

        }

}