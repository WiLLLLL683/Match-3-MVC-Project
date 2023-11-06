using Model.Factories;
using Model.Objects;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestUtils;
using UnityEngine;

namespace Model.Services.UnitTests
{
    public class BlockDestroyServiceTests
    {
        private int eventCount;

        private (GameBoard gameBoard, BlockDestroyService service) Setup()
        {
            var game = TestLevelFactory.CreateGame(1, 1);
            var setBlock = new CellSetBlockService();
            var validation = new ValidationService(game);
            var service = new BlockDestroyService(game, validation, setBlock);
            eventCount = 0;
            service.OnDestroy += (_) => eventCount++;

            return (game.CurrentLevel.gameBoard, service);
        }

        [Test]
        public void Destroy_ValidBlock_BlocksDestroyed()
        {
            var (gameBoard, service) = Setup();
            Block block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            service.DestroyAt(gameBoard.Cells[0, 0]);

            Assert.AreEqual(0, gameBoard.Blocks.Count);
            Assert.AreEqual(1, eventCount);
            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
        }

        [Test]
        public void Destroy_InValidBlock_BlocksDestroyed()
        {
            var (gameBoard, service) = Setup();

            service.DestroyAt(gameBoard.Cells[0, 0]);

            Assert.AreEqual(0, gameBoard.Blocks.Count);
            Assert.AreEqual(0, eventCount);
            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
        }

        [Test]
        public void Destroy_InValidCell_BlocksDestroyed()
        {
            var (gameBoard, service) = Setup();
            Block block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            service.DestroyAt(new Vector2Int(100, 100));

            Assert.AreEqual(1, gameBoard.Blocks.Count);
            Assert.AreEqual(0, eventCount);
            Assert.AreEqual(block, gameBoard.Cells[0, 0].Block);
        }
    }
}