using Model.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestUtils;

namespace Model.Objects.UnitTests
{
    public class DestroyVerticalLineBoosterTests
    {
        private (GameBoard gameBoard, IBlockDestroyService destroyService, IBlockMoveService moveService, IBooster booster) Setup()
        {
            var game = TestLevelFactory.CreateGame(1, 3);
            var gameBoard = game.CurrentLevel.gameBoard;
            var validationService = new ValidationService(game);
            var setBlockService = new CellSetBlockService();
            var destroyService = new BlockDestroyService(game, validationService, setBlockService);
            var moveService = Substitute.For<IBlockMoveService>();
            var booster = new DestroyVerticalLineBooster();

            return (gameBoard, destroyService, moveService, booster);
        }

        [Test]
        public void Execute_LineWith3Blocks_3CellsReturned()
        {
            var (gameBoard, destroyService, moveService, booster) = Setup();
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 1], gameBoard);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 2], gameBoard);

            booster.Execute(new(0, 0), destroyService, moveService);
            List<Block> blocks = destroyService.FindMarkedBlocks();

            Assert.AreEqual(3, blocks.Count);
            Assert.AreEqual(true, blocks.Contains(gameBoard.Cells[0, 0].Block));
            Assert.AreEqual(true, blocks.Contains(gameBoard.Cells[0, 1].Block));
            Assert.AreEqual(true, blocks.Contains(gameBoard.Cells[0, 2].Block));
        }

        [Test]
        public void Execute_LineWith2Blocks_2CellsReturned()
        {
            var (gameBoard, destroyService, moveService, booster) = Setup();
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 2], gameBoard);

            booster.Execute(new(0, 0), destroyService, moveService);
            List<Block> blocks = destroyService.FindMarkedBlocks();

            Assert.AreEqual(2, blocks.Count);
            Assert.AreEqual(true, blocks.Contains(gameBoard.Cells[0, 0].Block));
            Assert.AreEqual(true, blocks.Contains(gameBoard.Cells[0, 2].Block));
        }

        [Test]
        public void Execute_LineWithNotPlayableCells_2CellsReturned()
        {
            var (gameBoard, destroyService, moveService, booster) = Setup();
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            gameBoard.Cells[0, 1] = TestCellFactory.CreateCell(TestCellFactory.NotPlayableCellType, new(0, 1));
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 2], gameBoard);

            booster.Execute(new(0, 0), destroyService, moveService);
            List<Block> blocks = destroyService.FindMarkedBlocks();

            Assert.AreEqual(2, blocks.Count);
            Assert.AreEqual(true, blocks.Contains(gameBoard.Cells[0, 0].Block));
            Assert.AreEqual(true, blocks.Contains(gameBoard.Cells[0, 2].Block));
        }

        [Test]
        public void Execute_NoBlocks_EmptySetReturned()
        {
            var (gameBoard, destroyService, moveService, booster) = Setup();

            booster.Execute(new(0, 0), destroyService, moveService);
            List<Block> blocks = destroyService.FindMarkedBlocks();

            Assert.AreEqual(0, blocks.Count);
            Assert.AreEqual(false, blocks.Contains(gameBoard.Cells[0, 0].Block));
        }

        [Test]
        public void Execute_NoCells_EmptySetReturned()
        {
            var (gameBoard, destroyService, moveService, booster) = Setup();

            booster.Execute(new(99, 99), destroyService, moveService);
            List<Block> blocks = destroyService.FindMarkedBlocks();

            Assert.AreEqual(0, blocks.Count);
            Assert.AreEqual(false, blocks.Contains(gameBoard.Cells[0, 0].Block));
        }
    }
}