using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Minesweeper.Shared.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if (parameter != null)
                {
                    return (bool)value ? Visibility.Collapsed : Visibility.Visible;
                } 
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
