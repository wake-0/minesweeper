using System;
using System.Windows;
using Minesweeper.Utils;

namespace Minesweeper
{
    public partial class SettingsWindow : Window
    {
        #region Events
        public event EventHandler ApplyClicked;
        #endregion

        #region Constructor
        public SettingsWindow(GameSettings settings)
        {
            InitializeComponent();
            DataContext = settings;
        }
        #endregion

        #region Methods
        private void OnApplyClicked()
        {
            if (ApplyClicked != null)
            {
                ApplyClicked.Invoke(this, new EventArgs());
            }
        }

        private void ApplyButtonClicked(object sender, RoutedEventArgs e)
        {
            OnApplyClicked();
            Close();
        }
        #endregion
    }
}
