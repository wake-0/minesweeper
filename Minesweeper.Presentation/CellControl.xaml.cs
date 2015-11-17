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
            var button = sender as Button;
            if (button == null || Cell.IsToggled || Cell.IsMarked) { return; }

            var background = DefaultBrush;
           
            if (Cell.Type == CellType.Mine)
            {
                background = Brushes.Red;
            }

            Controller.OpenCell(Cell);

            button.Background = background;
            e.Handled = true;
        }

        private void MarkCell(object sender, MouseButtonEventArgs e)
        {
            Cell.IsMarked = !Cell.IsMarked;
        }
    }
}
