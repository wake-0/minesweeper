using Minesweeper.ViewModels;
using System.ComponentModel;
using System.Windows;
namespace Minesweeper
{
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void WindowClosing(object sender, CancelEventArgs e)
        {
            var model = DataContext as MainViewModel;
            if (model != null)
            {
                model.Shutdown();
            }
        }
        #endregion
    }
}
