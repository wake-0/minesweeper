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

        private SolidColorBrush BrushOne = Brushes.Aqua;
        private SolidColorBrush BrushTwo = Brushes.Green;
        private SolidColorBrush BrushThree = Brushes.Black;

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
            if (button == null || Cell.IsToggled) { return; }

            var background = BrushThree;
            if (Cell.Type == CellType.Number)
            {
                switch (Cell.Number)
                {
                    case 3:
                        background = BrushOne;
                        break;
                    case 5:
                        background = BrushTwo;
                        break;
                }
            }
            else if (Cell.Type == CellType.Mine)
            {
                background = Brushes.Red;
            }
            else
            {
                background = Brushes.White;
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
