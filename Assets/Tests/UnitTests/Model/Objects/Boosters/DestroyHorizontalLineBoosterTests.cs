using Model.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestUtils;

namespace Model.Objects.UnitTests
{

    public class DestroyHorizontalLineBoosterTests
    {
        private (GameBoard gameBoard, IValidationService validationService, IBlockMoveService moveService, IBooster booster) Setup()
        {
            var game = TestLevelFactory.CreateGame(3, 1);
            var gameBoard = game.CurrentLevel.gameBoard;
            var validationService = new ValidationService(game);
            var moveService = Substitute.For<IBlockMoveService>();
            var booster = new DestroyHorizontalLineBooster();

            return (gameBoard, validationService, moveService, booster);
        }

        [Test]
        public void Execute_LineWith3Blocks_3CellsReturned()
        {
            var (gameBoard, validationService, moveService, booster) = Setup();
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1, 0], gameBoard);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 0], gameBoard);

            HashSet<Cell> cells = booster.Execute(new(0, 0), gameBoard, validationService, moveService);

            Assert.AreEqual(3, cells.Count);
            Assert.AreEqual(true, cells.Contains(gameBoard.Cells[0, 0]));
            Assert.AreEqual(true, cells.Contains(gameBoard.Cells[1, 0]));
            Assert.AreEqual(true, cells.Contains(gameBoard.Cells[2, 0]));
        }

        [Test]
        public void Execute_LineWith2Blocks_2CellsReturned()
        {
            var (gameBoard, validationService, moveService, booster) = Setup();
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 0], gameBoard);

            HashSet<Cell> cells = booster.Execute(new(0, 0), gameBoard, validationService, moveService);

            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(true, cells.Contains(gameBoard.Cells[0, 0]));
            Assert.AreEqual(true, cells.Contains(gameBoard.Cells[2, 0]));
        }

        [Test]
        public void Execute_LineWithNotPlayableCells_2CellsReturned()
        {
            var (gameBoard, validationService, moveService, booster) = Setup();
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            gameBoard.Cells[1,0] = TestCellFactory.CreateCell(TestCellFactory.NotPlayableCellType, new(1, 0));
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 0], gameBoard);

            HashSet<Cell> cells = booster.Execute(new(0, 0), gameBoard, validationService, moveService);

            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(true, cells.Contains(gameBoard.Cells[0, 0]));
            Assert.AreEqual(true, cells.Contains(gameBoard.Cells[2, 0]));
        }

        [Test]
        public void Execute_NoBlocks_EmptySetReturned()
        {
            var (gameBoard, validationService, moveService, booster) = Setup();

            HashSet<Cell> cells = booster.Execute(new(0, 0), gameBoard, validationService, moveService);

            Assert.AreEqual(0, cells.Count);
            Assert.AreEqual(false, cells.Contains(gameBoard.Cells[0, 0]));
        }

        [Test]
        public void Execute_NoCells_EmptySetReturned()
        {
            var (gameBoard, validationService, moveService, booster) = Setup();

            HashSet<Cell> cells = booster.Execute(new(99, 99), gameBoard, validationService, moveService);

            Assert.AreEqual(0, cells.Count);
            Assert.AreEqual(false, cells.Contains(gameBoard.Cells[0, 0]));
        }
    }
}