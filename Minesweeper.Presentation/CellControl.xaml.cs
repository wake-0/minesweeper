using Minesweeper.Gamelogic;
using System.Windows.Controls;
using PostSharp.Patterns.Model;
using System.Windows.Media;
using System.Windows;

namespace Minesweeper.Presentation
{
    /// <summary>
    /// Interaktionslogik für CellControl.xaml
    /// </summary>
    [NotifyPropertyChanged]
    public partial class CellControl : UserControl
    {
        public Cell Cell { get; private set; }

        private SolidColorBrush BrushOne = Brushes.Aqua;
        private SolidColorBrush BrushTwo = Brushes.Green;
        private SolidColorBrush BrushThree = Brushes.Black;

        public CellControl(Cell cell)
        {
            InitializeComponent();

            Cell = cell;
            DataContext = this;
        }

        private void ToggleCell(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null || Cell.IsToggled) { return; }

            var background = BrushThree;
            //if (Cell.Type == CellType.Number)
            //{
                switch(Cell.Number)
                {
                    case 3:
                        background = BrushOne;
                        break;
                    case 5:
                        background = BrushTwo;
                        break;
                }
            //}
            //else if (Cell.Type == CellType.Mine)
            //{
            //    background = Brushes.Red;
            //}
            //else
            //{
            //    background = Brushes.White;
            //}

            button.Background = background;
            Cell.IsToggled = true;
            e.Handled = true;
        }
    }
}
