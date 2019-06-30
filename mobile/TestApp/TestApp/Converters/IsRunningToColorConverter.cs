using System;
using System.Globalization;
using Xamarin.Forms;

namespace GayTimer.Converters
{
    public class IsRunningToColorConverter : BindableObject, IValueConverter
    {
        public static BindableProperty InactiveColorProperty = BindableProperty.Create(nameof(InactiveColor), typeof(Color), typeof(IsRunningToColorConverter));

        public Color InactiveColor
        {
            get => (Color)GetValue(InactiveColorProperty);
            set => SetValue(InactiveColorProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.Black;

            var running = (bool) value;

            return running ? Color.White : InactiveColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}