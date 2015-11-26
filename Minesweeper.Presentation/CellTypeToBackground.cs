using Minesweeper.Gamelogic;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Minesweeper.Presentation
{
    public class CellTypeToBackground : IValueConverter
    {
        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CellType)
            {
                var type = (CellType)value;
                return type == CellType.Mine ? Brushes.Red : Brushes.LightGray;
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
