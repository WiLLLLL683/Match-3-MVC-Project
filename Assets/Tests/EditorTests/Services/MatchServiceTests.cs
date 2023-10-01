using Config;
using Model.Factories;
using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnitTests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Services.UnitTests
{
    public class MatchServiceTests
    {
        private (MatchService service, GameBoard gameBoard) Setup(
            int xLength,
            int yLength,
            bool validationBlockReturn = true,
            Pattern[] matchPatterns = default,
            HintPattern[] hintPatterns = default,
            params int[] preSpawnedBlocks)
        {
            var gameBoard = TestUtils.CreateGameBoard(xLength, yLength, 0, preSpawnedBlocks);

            var validation = new ValidationService();
            validation.SetLevel(gameBoard);
            //var validation = Substitute.For<IValidationService>();
            //validation.BlockExistsAt(default).ReturnsForAnyArgs(validationBlockReturn);

            var service = new MatchService(validation);
            service.SetLevel(gameBoard, matchPatterns, hintPatterns);
            return (service, gameBoard);
        }

        [Test]
        public void FindAllMatches_EmptyCells_EmptyList()
        {
            Pattern[] patterns = new Pattern[1] { TestUtils.DotPattern1x1() };
            var (service, gameBoard) = Setup(1, 1, true, patterns);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_NotPlayableCells_EmptyList()
        {
            Pattern[] patterns = new Pattern[1] { TestUtils.DotPattern1x1() };
            var (service, gameBoard) = Setup(1, 1, true, patterns);
            gameBoard.Cells[0,0].SetType(TestUtils.NotPlayableCellType);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_MatchingBlock_ListWith1Cell()
        {
            Pattern[] patterns = new Pattern[1] { TestUtils.DotPattern1x1() };
            var (service, gameBoard) = Setup(1, 1, true, patterns, default, TestUtils.DEFAULT_BLOCK);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(gameBoard.Cells[0, 0], matchedCells[0]);
            Assert.AreEqual(1, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_MatchingLine_ListWithLineOfCells()
        {
            Pattern[] patterns = new Pattern[1] { TestUtils.VertLinePattern1x3() };
            var (service, gameBoard) = Setup(1, 3, true, patterns, default, TestUtils.DEFAULT_BLOCK, TestUtils.DEFAULT_BLOCK, TestUtils.DEFAULT_BLOCK);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(gameBoard.Cells[0, 0], matchedCells[0]);
            Assert.AreEqual(gameBoard.Cells[0, 1], matchedCells[1]);
            Assert.AreEqual(gameBoard.Cells[0, 2], matchedCells[2]);
            Assert.AreEqual(3, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_MatchingLineShifted_ListWithLineOfCells()
        {
            Pattern[] patterns = new Pattern[1] { TestUtils.VertLinePattern1x3() };
            var (service, gameBoard) = Setup(3, 3, true, patterns);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.Cells[2, 0]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.Cells[2, 1]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.Cells[2, 2]);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(gameBoard.Cells[2, 0], matchedCells[0]);
            Assert.AreEqual(gameBoard.Cells[2, 1], matchedCells[1]);
            Assert.AreEqual(gameBoard.Cells[2, 2], matchedCells[2]);
            Assert.AreEqual(3, matchedCells.Count);
        }

        [Test]
        public void FindAllMatches_MatchingCross_CrossWithLineOfCells()
        {
            Pattern[] patterns = new Pattern[1] { TestUtils.CrossPattern3x3() };
            var (service, gameBoard) = Setup(3, 3, true, patterns);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.Cells[0, 1]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.Cells[1, 0]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.Cells[1, 1]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.Cells[1, 2]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.Cells[2, 1]);

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
            Pattern[] patterns = new Pattern[1] { TestUtils.CrossPattern3x3() };
            var (service, gameBoard) = Setup(3, 3, true, patterns);

            List<Cell> matchedCells = service.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }
    }
}