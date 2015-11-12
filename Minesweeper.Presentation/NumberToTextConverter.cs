using Minesweeper.Gamelogic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Minesweeper.Presentation
{
    public class NumberToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Cell)
            {
                var cell = (Cell)value;
                var number = cell.Number;

                return cell.Type == CellType.Mine || number == 0 ? "" : number.ToString();
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
