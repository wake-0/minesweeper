using System.Linq;
using System.Windows;

namespace Minesweeper.Shared
{
    public static class WPFHelper
    {
        #region Methods
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
        #endregion
    }
}
