using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Minesweeper.Utils;

namespace Minesweeper
{
    /// <summary>
    /// Interaktionslogik für SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public event EventHandler ApplyClicked;

        public SettingsWindow(GameSettings settings)
        {
            InitializeComponent();
            DataContext = settings;
        }

        private void OnApplyClicked()
        {
            if (ApplyClicked != null)
            {
                ApplyClicked.Invoke(this, new EventArgs());
            }
        }

        private void ApplyButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
            OnApplyClicked();
        }
    }
}
