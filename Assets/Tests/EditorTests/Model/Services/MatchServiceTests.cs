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
            MatchPattern[] matchPatterns = default,
            params int[] preSpawnedBlocks)
        {
            var game = TestLevelFactory.CreateGame(xLength, yLength);
            game.CurrentLevel.gameBoard = TestLevelFactory.CreateGameBoard(xLength, yLength, 0, preSpawnedBlocks);

            var validation = new ValidationService(game);
            var matcher = new Matcher(validation);
            var service = new BlockMatchService(game, matcher);
            return (service, game.CurrentLevel.gameBoard);
        }

        [Test]
        public void FindAllMatches_EmptyCells_EmptyList()
        {
            MatchPattern[] patterns = new MatchPattern[1] { TestPatternFactory.DotPattern1x1() };
            var (service, gameBoard) = Setup(1, 1, patterns);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_NotPlayableCells_EmptyList()
        {
            MatchPattern[] patterns = new MatchPattern[1] { TestPatternFactory.DotPattern1x1() };
            var (service, gameBoard) = Setup(1, 1, patterns);
            gameBoard.Cells[0,0].Type = TestCellFactory.NotPlayableCellType;

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_MatchingBlock_ListWith1Cell()
        {
            MatchPattern[] patterns = new MatchPattern[1] { TestPatternFactory.DotPattern1x1() };
            var (service, gameBoard) = Setup(1, 1, patterns, default, TestBlockFactory.DEFAULT_BLOCK);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(gameBoard.Cells[0, 0], matchedCells[0]);
            Assert.AreEqual(1, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_MatchingLine_ListWithLineOfCells()
        {
            MatchPattern[] patterns = new MatchPattern[1] { TestPatternFactory.VertLinePattern1x3() };
            var (service, gameBoard) = Setup(1, 3, patterns, default, TestBlockFactory.DEFAULT_BLOCK, TestBlockFactory.DEFAULT_BLOCK, TestBlockFactory.DEFAULT_BLOCK);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(gameBoard.Cells[0, 0], matchedCells[0]);
            Assert.AreEqual(gameBoard.Cells[0, 1], matchedCells[1]);
            Assert.AreEqual(gameBoard.Cells[0, 2], matchedCells[2]);
            Assert.AreEqual(3, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_MatchingLineShifted_ListWithLineOfCells()
        {
            MatchPattern[] patterns = new MatchPattern[1] { TestPatternFactory.VertLinePattern1x3() };
            var (service, gameBoard) = Setup(3, 3, patterns);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 0]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 1]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 2]);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(gameBoard.Cells[2, 0], matchedCells[0]);
            Assert.AreEqual(gameBoard.Cells[2, 1], matchedCells[1]);
            Assert.AreEqual(gameBoard.Cells[2, 2], matchedCells[2]);
            Assert.AreEqual(3, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_MatchingCross_CrossWithLineOfCells()
        {
            MatchPattern[] patterns = new MatchPattern[1] { TestPatternFactory.CrossPattern3x3() };
            var (service, gameBoard) = Setup(3, 3, patterns);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 1]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1, 0]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1, 1]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1, 2]);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 1]);

            HashSet<Cell> matchedCells = service.FindAllMatches();

            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[0, 1]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[1, 0]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[1, 1]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[1, 2]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.Cells[2, 1]));
            Assert.AreEqual(5, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_NoMatch_EmptyList()
        {
            MatchPattern[] patterns = new MatchPattern[1] { TestPatternFactory.CrossPattern3x3() };
            var (service, gameBoard) = Setup(3, 3, patterns);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }
    }
}