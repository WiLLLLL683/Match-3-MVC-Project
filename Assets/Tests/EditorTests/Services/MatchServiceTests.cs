using Data;
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
            params int[] preSpawnedBlocks)
        {

            var gameBoard = TestUtils.CreateGameBoard(xLength, yLength, preSpawnedBlocks);

            var validation = new ValidationService();
            validation.SetLevel(gameBoard);
            //var validation = Substitute.For<IValidationService>();
            //validation.BlockExistsAt(default).ReturnsForAnyArgs(validationBlockReturn);

            var service = new MatchService(validation);
            service.SetLevel(gameBoard);
            return (service, gameBoard);
        }

        [Test]
        public void Match_EmptyCells_EmptyList()
        {
            Pattern pattern = TestUtils.DotPattern1x1();
            var (service, gameBoard) = Setup(1, 1, true);

            List<Cell> matchedCells = service.Match(pattern).ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }

        [Test]
        public void Match_NotPlayableCells_EmptyList()
        {
            Pattern pattern = TestUtils.DotPattern1x1();
            var (service, gameBoard) = Setup(1, 1, true);
            gameBoard.cells[0,0].ChangeType(TestUtils.NotPlayableCellType);

            List<Cell> matchedCells = service.Match(pattern).ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }

        [Test]
        public void Match_MatchingBlock_ListWith1Cell()
        {
            Pattern pattern = TestUtils.DotPattern1x1();
            var (service, gameBoard) = Setup(1, 1, true, TestUtils.DEFAULT_BLOCK);

            List<Cell> matchedCells = service.Match(pattern).ToList();

            Assert.AreEqual(gameBoard.cells[0, 0], matchedCells[0]);
            Assert.AreEqual(1, matchedCells.Count);
        }

        [Test]
        public void Match_MatchingLine_ListWithLineOfCells()
        {
            Pattern pattern = TestUtils.VertLinePattern1x3();
            var (service, gameBoard) = Setup(1, 3, true, TestUtils.DEFAULT_BLOCK, TestUtils.DEFAULT_BLOCK, TestUtils.DEFAULT_BLOCK);

            List<Cell> matchedCells = service.Match(pattern).ToList();

            Assert.AreEqual(gameBoard.cells[0, 0], matchedCells[0]);
            Assert.AreEqual(gameBoard.cells[0, 1], matchedCells[1]);
            Assert.AreEqual(gameBoard.cells[0, 2], matchedCells[2]);
            Assert.AreEqual(3, matchedCells.Count);
        }

        [Test]
        public void Match_MatchingLineShifted_ListWithLineOfCells()
        {
            Pattern pattern = TestUtils.VertLinePattern1x3();
            var (service, gameBoard) = Setup(3, 3, true);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.cells[2, 0]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.cells[2, 1]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.cells[2, 2]);

            List<Cell> matchedCells = service.Match(pattern).ToList();

            Assert.AreEqual(gameBoard.cells[2, 0], matchedCells[0]);
            Assert.AreEqual(gameBoard.cells[2, 1], matchedCells[1]);
            Assert.AreEqual(gameBoard.cells[2, 2], matchedCells[2]);
            Assert.AreEqual(3, matchedCells.Count);
        }

        [Test]
        public void Match_MatchingCross_CrossWithLineOfCells()
        {
            Pattern pattern = TestUtils.CrossPattern3x3();
            var (service, gameBoard) = Setup(3, 3, true);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.cells[0, 1]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.cells[1, 0]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.cells[1, 1]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.cells[1, 2]);
            TestUtils.CreateBlockInCell(TestUtils.DEFAULT_BLOCK, gameBoard.cells[2, 1]);

            HashSet<Cell> matchedCells = service.Match(pattern);

            Assert.IsTrue(matchedCells.Contains(gameBoard.cells[0, 1]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.cells[1, 0]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.cells[1, 1]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.cells[1, 2]));
            Assert.IsTrue(matchedCells.Contains(gameBoard.cells[2, 1]));
            Assert.AreEqual(5, matchedCells.Count);
        }

        [Test]
        public void Match_NoMatch_EmptyList()
        {
            Pattern pattern = TestUtils.CrossPattern3x3();
            var (service, gameBoard) = Setup(3, 3, true);

            List<Cell> matchedCells = service.Match(pattern).ToList();

            Assert.AreEqual(0, matchedCells.Count);
        }
    }
}