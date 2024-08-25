using Model.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestUtils;
using UnityEngine;

namespace Model.Objects.UnitTests
{
    public class DestroyOneBlockBoosterTests
    {
        private (GameBoard gameBoard, IBlockDestroyService destroyService, IBlockMoveService moveService, IBooster booster) Setup()
        {
            var game = TestLevelFactory.CreateGame(3, 1);
            var gameBoard = game.CurrentLevel.gameBoard;
            var validationService = new ValidationService(game);
            var setBlockService = new CellSetBlockService();
            var destroyService = new BlockDestroyService(game, validationService, setBlockService);
            var moveService = Substitute.For<IBlockMoveService>();
            var booster = new DestroyOneBlockBooster();

            return (gameBoard, destroyService, moveService, booster);
        }

        [Test]
        public void Execute_ValidPosition_OneBlockReturned()
        {
            var (gameBoard, destroyService, moveService, booster) = Setup();
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0,0], gameBoard);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1,0], gameBoard);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2,0], gameBoard);

            booster.Execute(new(0, 0), destroyService, moveService);
            List<Block> blocks = destroyService.FindMarkedBlocks();

            Assert.AreEqual(1, blocks.Count);
            Assert.AreEqual(true, blocks.Contains(gameBoard.Cells[0, 0].Block));
        }

        [Test]
        public void Execute_NoBlock_EmptySetReturned()
        {
            var (gameBoard, destroyService, moveService, booster) = Setup();

            booster.Execute(new(0, 0), destroyService, moveService);
            List<Block> blocks = destroyService.FindMarkedBlocks();

            Assert.AreEqual(0, blocks.Count);
            Assert.AreEqual(false, blocks.Contains(gameBoard.Cells[0, 0].Block));
        }

        [Test]
        public void Execute_NoCell_EmptySetReturned()
        {
            var (gameBoard, destroyService, moveService, booster) = Setup();

            booster.Execute(new(99, 99), destroyService, moveService);
            List<Block> blocks = destroyService.FindMarkedBlocks();

            Assert.AreEqual(0, blocks.Count);
            Assert.AreEqual(false, blocks.Contains(gameBoard.Cells[0, 0].Block));
        }
    }
}