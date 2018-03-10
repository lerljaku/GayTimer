using System;
using System.Globalization;
using Xamarin.Forms;

namespace TestApp
{
    public class IsRunningToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.Black;

            var running = (bool) value;

            return running ? Color.White : Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}