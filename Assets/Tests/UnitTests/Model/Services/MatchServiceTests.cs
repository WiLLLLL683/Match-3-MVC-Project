using Model.Objects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TestUtils;

namespace Model.Services.UnitTests
{
    public class MatchServiceTests
    {
        private (BlockMatchService service, GameBoard gameBoard) Setup(
            int xLength,
            int yLength,
            params MatchPattern[] matchPatterns)
        {
            var game = TestLevelFactory.CreateGame(xLength, yLength);
            game.CurrentLevel.gameBoard = TestLevelFactory.CreateGameBoard(xLength, yLength, 0);
            game.CurrentLevel.matchPatterns = matchPatterns;

            var validation = new ValidationService(game);
            var matcher = new Matcher(validation);
            var service = new BlockMatchService(game, matcher);
            return (service, game.CurrentLevel.gameBoard);
        }

        [Test]
        public void FindAllMatches_EmptyCells_EmptyList()
        {
            var (service, gameBoard) = Setup(1, 1, TestPatternFactory.DotPattern1x1());

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_NotPlayableCells_EmptyList()
        {
            var (service, gameBoard) = Setup(1, 1, TestPatternFactory.DotPattern1x1());
            gameBoard.Cells[0,0].Type = TestCellFactory.NotPlayableCellType;

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_MatchingBlock_ListWith1Cell()
        {
            var (service, gameBoard) = Setup(1, 1, TestPatternFactory.DotPattern1x1());
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            HashSet<Cell> matchedCells = service.FindAllMatches();

            Assert.AreEqual(1, matchedCells.Count);
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[0, 0]));
        }

        [Test]
        public void FindAllMatches_MatchingLine_ListWithLineOfCells()
        {
            var (service, gameBoard) = Setup(1, 3, TestPatternFactory.VertLinePattern1x3());
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 2], gameBoard);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 1], gameBoard);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            HashSet<Cell> matchedCells = service.FindAllMatches();

            Assert.AreEqual(3, matchedCells.Count);
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[0, 2]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[0, 1]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[0, 0]));
        }

        [Test]
        public void FindAllMatches_MatchingLineShifted_ListWithLineOfCells()
        {
            var (service, gameBoard) = Setup(3, 3, TestPatternFactory.VertLinePattern1x3());
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 2]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 1]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 0]);

            HashSet<Cell> matchedCells = service.FindAllMatches();

            Assert.AreEqual(3, matchedCells.Count);
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[2, 2]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[2, 1]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[2, 0]));
        }

        [Test]
        public void FindAllMatches_MatchingCross_CrossWithLineOfCells()
        {
            var (service, gameBoard) = Setup(3, 3, TestPatternFactory.CrossPattern3x3());
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1, 2]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 1]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1, 1]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 1]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1, 0]);

            HashSet<Cell> matchedCells = service.FindAllMatches();

            Assert.AreEqual(5, matchedCells.Count);
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[1, 2]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[0, 1]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[1, 1]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[2, 1]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[1, 0]));
        }

        [Test]
        public void FindAllMatches_NoMatch_EmptyList()
        {
            var (service, gameBoard) = Setup(3, 3, TestPatternFactory.CrossPattern3x3());

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }
    }
}