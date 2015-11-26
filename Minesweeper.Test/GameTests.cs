using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.Utils;
using Minesweeper.Gamelogic;

namespace Minesweeper.Test
{
    [TestClass]
    public class GameGridFactoryTests
    {
        [TestMethod]
        public void CreateGame()
        {
            Cell[,] game = GameGridFactory.CreateGame(10, 10, 10);
        }
    }
}
