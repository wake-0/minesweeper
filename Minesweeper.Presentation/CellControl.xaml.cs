using Minesweeper.Gamelogic;
using System.Windows.Controls;
using PostSharp.Patterns.Model;
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

        public CellControl(GameController controller, Cell cell)
        {
            InitializeComponent();

            Cell = cell;
            Controller = controller;
            DataContext = this;
        }

        private void ToggleCell(object sender, RoutedEventArgs e)
        {
            Controller.OpenCell(Cell);
            e.Handled = true;
        }

        private void MarkCell(object sender, MouseButtonEventArgs e)
        {
            Controller.MarkCell(Cell);
            e.Handled = true;
        }

        private void ToggleNeighbours(object sender, MouseButtonEventArgs e)
        {
            Controller.OpenNeighbours(Cell);
            e.Handled = true;

        }
    }
}
