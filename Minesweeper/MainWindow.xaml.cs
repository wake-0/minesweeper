using Minesweeper.ViewModels;
using System.ComponentModel;
using System.Windows;
namespace Minesweeper
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            var model = DataContext as MainViewModel;
            if (model != null)
            {
                model.SaveStatistics();
            }
        }
    }
}
