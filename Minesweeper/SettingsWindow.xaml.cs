using System;
using System.Windows;
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
