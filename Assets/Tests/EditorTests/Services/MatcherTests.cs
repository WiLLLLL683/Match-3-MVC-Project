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

namespace Model.Services.UnitTests
{
    public class MatcherTests
    {
        private IValidationService validation = Substitute.For<IValidationService>();


        //Базовые тесты с 1 клеткой
        [Test]
        public void _FindPattern_1MatchingBlock_ListWith1Cell()
        {
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            var gameBoard = TestUtils.CreateGameBoard(1, 1, TestUtils.RED_BLOCK);
            var pattern = TestUtils.CreatePattern(1, 1, true);
            var matcher = new Matcher(validation);

            List<Cell> cells = matcher.MatchAt(new(0,0), pattern, gameBoard).ToList();

            Assert.AreEqual(1, cells.Count);
            Assert.AreEqual(gameBoard.cells[0,0], cells[0]);
        }

        [Test]
        public void _FindPattern_EmptyPattern_Null()
        {
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            var gameBoard = TestUtils.CreateGameBoard(1, 1, TestUtils.RED_BLOCK);
            var pattern = TestUtils.CreatePattern(1, 1, false);
            var matcher = new Matcher(validation);

            List<Cell> cells = matcher.MatchAt(new(0, 0), pattern, gameBoard).ToList();

            Assert.AreEqual(0, cells.Count);
        }

        [Test]
        public void _FindPattern_NotValidCell_Null()
        {
            validation.BlockExistsAt(default).ReturnsForAnyArgs(false);
            var gameBoard = TestUtils.CreateGameBoard(1, 1, TestUtils.RED_BLOCK);
            var pattern = TestUtils.CreatePattern(1, 1, true);
            var matcher = new Matcher(validation);

            List<Cell> cells = matcher.MatchAt(new(10, 10), pattern, gameBoard).ToList();

            Assert.AreEqual(0, cells.Count);
        }

        [Test]
        public void _FindPattern_NotPlayableCell_Null()
        {
            validation.BlockExistsAt(default).ReturnsForAnyArgs(false);
            var gameBoard = TestUtils.CreateGameBoard(1, 1, TestUtils.RED_BLOCK);
            gameBoard.cells[0, 0].ChangeType(TestUtils.NotPlayableCellType);
            var pattern = TestUtils.CreatePattern(1, 1, true);
            var matcher = new Matcher(validation);

            List<Cell> cells = matcher.MatchAt(new(0, 0), pattern, gameBoard).ToList();

            Assert.AreEqual(0, cells.Count);
        }

        [Test]
        public void _FindPattern_EmptyCell_Null()
        {
            validation.BlockExistsAt(default).ReturnsForAnyArgs(false);
            var gameBoard = TestUtils.CreateGameBoard(1, 1);
            var pattern = TestUtils.CreatePattern(1, 1, true);
            var matcher = new Matcher(validation);

            List<Cell> cells = matcher.MatchAt(new(0, 0), pattern, gameBoard).ToList();

            Assert.AreEqual(0, cells.Count);
        }

        //тесты с несколькими клетками в разных положениях

        [Test]
        public void FindPattern_2MatchingBlock_ListWith2Cell()
        {
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            var gameBoard = TestUtils.CreateGameBoard(2, 1,
                TestUtils.RED_BLOCK, TestUtils.RED_BLOCK,
                TestUtils.BLUE_BLOCK, TestUtils.GREEN_BLOCK);
            var pattern = TestUtils.CreatePattern(2, 1, true, true);
            var matcher = new Matcher(validation);

            List<Cell> cells = matcher.MatchAt(new(0,0), pattern, gameBoard).ToList();

            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(gameBoard.cells[0, 0], cells[0]);
            Assert.AreEqual(gameBoard.cells[1, 0], cells[1]);
        }

        [Test]
        public void FindPattern_NoMatchingBlocks_Null()
        {
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            var gameBoard = TestUtils.CreateGameBoard(2, 2,
                TestUtils.BLUE_BLOCK, TestUtils.GREEN_BLOCK,
                TestUtils.RED_BLOCK, TestUtils.YELLOW_BLOCK);
            var pattern = TestUtils.CreatePattern(2, 1, true, true);
            var matcher = new Matcher(validation);

            List<Cell> cells = matcher.MatchAt(new(0, 0), pattern, gameBoard).ToList();

            Assert.AreEqual(0, cells.Count);
        }

        [Test]
        public void FindPattern_2MatchingBlockShiftedDown_ListWith2Cell()
        {
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            var gameBoard = TestUtils.CreateGameBoard(2, 2,
                TestUtils.BLUE_BLOCK, TestUtils.GREEN_BLOCK,
                TestUtils.RED_BLOCK, TestUtils.RED_BLOCK);
            var pattern = TestUtils.CreatePattern(2, 1, true, true);
            var matcher = new Matcher(validation);

            List<Cell> cells = matcher.MatchAt(new(0,1), pattern, gameBoard).ToList();

            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(gameBoard.cells[0,1], cells[0]);
            Assert.AreEqual(gameBoard.cells[1,1], cells[1]);
        }

        [Test]
        public void FindPattern_2MatchingBlockShiftedRight_ListWith2Cell()
        {
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            var gameBoard = TestUtils.CreateGameBoard(3, 2,
                TestUtils.YELLOW_BLOCK, TestUtils.RED_BLOCK, TestUtils.RED_BLOCK,
                TestUtils.BLUE_BLOCK, TestUtils.GREEN_BLOCK, TestUtils.YELLOW_BLOCK);
            var pattern = TestUtils.CreatePattern(2, 1, true, true);
            var matcher = new Matcher(validation);

            List<Cell> cells = matcher.MatchAt(new(1,0), pattern, gameBoard).ToList();

            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(gameBoard.cells[1,0], cells[0]);
            Assert.AreEqual(gameBoard.cells[2,0], cells[1]);
        }
    }
}