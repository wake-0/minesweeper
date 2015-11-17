using Minesweeper.Gamelogic;
using System.Windows.Controls;
using PostSharp.Patterns.Model;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace Minesweeper.Presentation
{
    /// <summary>
    /// Interaktionslogik für CellControl.xaml
    /// </summary>
    [NotifyPropertyChanged]
    public partial class CellControl : UserControl
    {
        public Cell Cell { get; private set; }
        public GameController Controller { get; private set; }

        private SolidColorBrush DefaultBrush = Brushes.Transparent;

        public CellControl(GameController controller, Cell cell)
        {
            InitializeComponent();

            Cell = cell;
            Controller = controller;
            DataContext = this;
        }

        private void ToggleCell(object sender, RoutedEventArgs e)
        {
            e.Handled = ToggleCell(sender);
        }

        private void MarkCell(object sender, MouseButtonEventArgs e)
        {
            if (Cell.IsToggled) { return; }

            Cell.IsMarked = !Cell.IsMarked;
        }

        private void OpenDependingCells(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            if (button == null || Cell.IsMarked) { return; }

            if (!Cell.IsToggled)
            {
                e.Handled = ToggleCell(sender);
            }
            else
            {
                Controller.OpenDependingCells(Cell);
                e.Handled = true;
            }

        }

        private bool ToggleCell(object sender)
        {
            var button = sender as Button;
            if (button == null || Cell.IsToggled || Cell.IsMarked) { return false; }

            var background = DefaultBrush;

            if (Cell.Type == CellType.Mine)
            {
                background = Brushes.Red;
            }

            Controller.OpenCell(Cell);

            button.Background = background;
            return true;
        }
    }
}
