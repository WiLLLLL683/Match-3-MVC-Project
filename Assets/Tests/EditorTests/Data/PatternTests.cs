using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using UnitTests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Data.UnitTests
{
    public class PatternTests
    {
        private IValidationService validation = Substitute.For<IValidationService>();


        //Базовые тесты с 1 клеткой
        [Test]
        public void _FindPattern_1MatchingBlock_ListWith1Cell()
        {
            var gameBoard = TestUtils.CreateGameBoard(1, 1, TestUtils.RED_BLOCK);
            var pattern = TestUtils.CreatePattern(1, 1, true);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);

            List<Cell> cells = pattern.Match(gameBoard,new Vector2Int(0,0), validation).ToList();

            Assert.AreEqual(1, cells.Count);
            Assert.AreEqual(gameBoard.cells[0,0], cells[0]);
        }

        [Test]
        public void _FindPattern_EmptyPattern_Null()
        {
            var gameBoard = TestUtils.CreateGameBoard(1, 1, TestUtils.RED_BLOCK);
            var pattern = TestUtils.CreatePattern(1, 1, false);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(0, 0), validation).ToList();

            Assert.AreEqual(0, cells.Count);
        }

        [Test]
        public void _FindPattern_NotValidCell_Null()
        {
            var gameBoard = TestUtils.CreateGameBoard(1, 1, TestUtils.RED_BLOCK);
            var pattern = TestUtils.CreatePattern(1, 1, true);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(10, 10), validation).ToList();

            Assert.AreEqual(0, cells.Count);
            LogAssert.Expect(LogType.Warning, "Cell position out of GameBoards range");
        }

        [Test]
        public void _FindPattern_NotPlayableCell_Null()
        {
            var gameBoard = TestUtils.CreateGameBoard(1, 1, TestUtils.RED_BLOCK);
            gameBoard.cells[0, 0].ChangeType(TestUtils.NotPlayableCellType);
            var pattern = TestUtils.CreatePattern(1, 1, true);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(0, 0), validation).ToList();

            Assert.AreEqual(0, cells.Count);
            LogAssert.Expect(LogType.Warning, "Tried to get Block but Cell was notPlayable");
        }

        [Test]
        public void _FindPattern_EmptyCell_Null()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(1, 1);
            bool[,] grid = new bool[1, 1];
            grid[0, 0] = true;
            Pattern pattern = new Pattern(grid);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(0, 0), validation).ToList();

            Assert.AreEqual(0, cells.Count);
            LogAssert.Expect(LogType.Warning, "Tried to get Block but Cell was empty");
        }

        //тесты с несколькими клетками в разных положениях

        [Test]
        public void FindPattern_2MatchingBlock_ListWith2Cell()
        {
            var gameBoard = TestUtils.CreateGameBoard(2, 1,
                TestUtils.RED_BLOCK, TestUtils.RED_BLOCK,
                TestUtils.BLUE_BLOCK, TestUtils.GREEN_BLOCK);
            var pattern = TestUtils.CreatePattern(2, 1, true, true);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(0,0), validation).ToList();

            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(gameBoard.cells[0, 0], cells[0]);
            Assert.AreEqual(gameBoard.cells[1, 0], cells[1]);
        }

        [Test]
        public void FindPattern_NoMatchingBlocks_Null()
        {
            var gameBoard = TestUtils.CreateGameBoard(2, 2,
                TestUtils.BLUE_BLOCK, TestUtils.GREEN_BLOCK,
                TestUtils.RED_BLOCK, TestUtils.YELLOW_BLOCK);
            var pattern = TestUtils.CreatePattern(2, 1, true, true);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(0, 0), validation).ToList();

            Assert.AreEqual(0, cells.Count);
        }

        [Test]
        public void FindPattern_2MatchingBlockShiftedDown_ListWith2Cell()
        {
            var gameBoard = TestUtils.CreateGameBoard(2, 2,
                TestUtils.BLUE_BLOCK, TestUtils.GREEN_BLOCK,
                TestUtils.RED_BLOCK, TestUtils.RED_BLOCK);
            var pattern = TestUtils.CreatePattern(2, 1, true, true);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(0,1), validation).ToList();

            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(gameBoard.cells[0,1], cells[0]);
            Assert.AreEqual(gameBoard.cells[1,1], cells[1]);
        }

        [Test]
        public void FindPattern_2MatchingBlockShiftedRight_ListWith2Cell()
        {
            var gameBoard = TestUtils.CreateGameBoard(3, 2,
                TestUtils.YELLOW_BLOCK, TestUtils.RED_BLOCK, TestUtils.RED_BLOCK,
                TestUtils.BLUE_BLOCK, TestUtils.GREEN_BLOCK, TestUtils.YELLOW_BLOCK);
            var pattern = TestUtils.CreatePattern(2, 1, true, true);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(1,0), validation).ToList();

            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(gameBoard.cells[1,0], cells[0]);
            Assert.AreEqual(gameBoard.cells[2,0], cells[1]);
        }
    }
}