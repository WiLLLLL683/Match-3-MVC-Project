using System.Collections;
using System.Collections.Generic;
using Data;
using Model.Objects;
using NUnit.Framework;
using Tests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Systems.UnitTests
{
    public class GravitySystemTests
    {
        [Test]
        public void Execute_OneBlockOneEmptyCellUnder_BlockMovesDown()
        {
            var gameBoard = TestUtils.CreateGameBoard(1, 2, TestUtils.DEFAULT_BLOCK);
            var block = gameBoard.cells[0, 0].Block;
            var gravitySystem = new GravitySystem();

            gravitySystem.Execute(gameBoard);

            Assert.AreEqual(null, gameBoard.cells[0, 0].Block);
            Assert.AreEqual(block, gameBoard.cells[0, 1].Block);
        }

        [Test]
        public void Execute_TwoBlocksNoEmptyCellUnder_NoChange()
        {
            var gameBoard = TestUtils.CreateGameBoard(1, 2, TestUtils.DEFAULT_BLOCK, TestUtils.DEFAULT_BLOCK);
            var blockA = gameBoard.cells[0, 0].Block;
            var blockB = gameBoard.cells[0, 1].Block;
            var gravitySystem = new GravitySystem();

            gravitySystem.Execute(gameBoard);

            Assert.AreEqual(blockA, gameBoard.cells[0, 0].Block);
            Assert.AreEqual(blockB, gameBoard.cells[0, 1].Block);
        }

        [Test]
        public void Execute_OneBlockNotPlayableCellUnder_NoChange()
        {
            var gameBoard = TestUtils.CreateGameBoard(1, 2, TestUtils.DEFAULT_BLOCK);
            gameBoard.cells[0, 1].ChangeType(TestUtils.NotPlayableCellType);
            var blockA = gameBoard.cells[0, 0].Block;
            var gravitySystem = new GravitySystem();

            gravitySystem.Execute(gameBoard);

            Assert.AreEqual(blockA, gameBoard.cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.cells[0, 1].Block);
        }

        [Test]
        public void Execute_TwoBlocksEmptyCellUnder_TwoBlocksMoveDown()
        {
            var gameBoard = TestUtils.CreateGameBoard(1, 3, TestUtils.DEFAULT_BLOCK, TestUtils.DEFAULT_BLOCK);
            var blockA = gameBoard.cells[0, 0].Block;
            var blockB = gameBoard.cells[0, 1].Block;
            var gravitySystem = new GravitySystem();

            gravitySystem.Execute(gameBoard);

            Assert.AreEqual(null, gameBoard.cells[0, 0].Block);
            Assert.AreEqual(blockA, gameBoard.cells[0, 1].Block);
            Assert.AreEqual(blockB, gameBoard.cells[0, 2].Block);
        }

        [Test]
        public void Execute_AllEmptyCells_NoChange()
        {
            var gameBoard = TestUtils.CreateGameBoard(1, 2);
            var gravitySystem = new GravitySystem();

            gravitySystem.Execute(gameBoard);

            Assert.AreEqual(null, gameBoard.cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.cells[0, 1].Block);
        }

        [Test]
        public void Execute_3Blocks2EmptyCellUnder_3BlocksMoveDown()
        {
            var gameBoard = TestUtils.CreateGameBoard(3, 3, TestUtils.DEFAULT_BLOCK, TestUtils.DEFAULT_BLOCK, TestUtils.DEFAULT_BLOCK);
            var block1 = gameBoard.cells[0, 0].Block;
            var block2 = gameBoard.cells[1, 0].Block;
            var block3 = gameBoard.cells[2, 0].Block;
            var gravitySystem = new GravitySystem();

            gravitySystem.Execute(gameBoard);

            Assert.AreEqual(block1, gameBoard.cells[0, 2].Block);
            Assert.AreEqual(block2, gameBoard.cells[1, 2].Block);
            Assert.AreEqual(block3, gameBoard.cells[2, 2].Block);
        }

        //тесты отдельных функций

        //[Test]
        //public void TryMoveBlockDown_1Blocks1EmptyCellUnder_1BlocksMoveDown()
        //{
        //    var gameBoard = TestUtils.CreateGameBoard(1, 2, TestUtils.DEFAULT_BLOCK);
        //    var block = gameBoard.Cells[0, 0].Block;
        //    var gravitySystem = new GravitySystem();

        //    gravitySystem.SetGameBoard(gameBoard);
        //    gravitySystem.TryMoveBlockDown(0, 0);

        //    Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
        //    Assert.AreEqual(block, gameBoard.Cells[0, 1].Block);
        //}

        //[Test]
        //public void TryMoveBlockDown_1Blocks3EmptyCellUnder_1BlocksMoveDown()
        //{
        //    var gameBoard = TestUtils.CreateGameBoard(1, 4, TestUtils.DEFAULT_BLOCK);
        //    var block = gameBoard.Cells[0, 0].Block;
        //    var gravitySystem = new GravitySystem();

        //    Debug.Log($"Before 0 = {gameBoard.Cells[0, 0].Block}");
        //    Debug.Log($"Before 1 = {gameBoard.Cells[0, 1].Block}");
        //    Debug.Log($"Before 2 = {gameBoard.Cells[0, 2].Block}");
        //    Debug.Log($"Before 3 = {gameBoard.Cells[0, 3].Block}");

        //    gravitySystem.SetGameBoard(gameBoard);
        //    gravitySystem.TryMoveBlockDown(0, 0);

        //    Debug.Log($"After 0 = {gameBoard.Cells[0, 0].Block}");
        //    Debug.Log($"After 1 = {gameBoard.Cells[0, 1].Block}");
        //    Debug.Log($"After 2 = {gameBoard.Cells[0, 2].Block}");
        //    Debug.Log($"After 3 = {gameBoard.Cells[0, 3].Block}");

        //    Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
        //    Assert.AreEqual(null, gameBoard.Cells[0, 1].Block);
        //    Assert.AreEqual(null, gameBoard.Cells[0, 2].Block);
        //    Assert.AreEqual(block, gameBoard.Cells[0, 3].Block);
        //}
    }
}