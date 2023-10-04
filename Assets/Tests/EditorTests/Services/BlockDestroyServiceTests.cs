using Model.Factories;
using Model.Objects;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnitTests;
using UnityEngine;

namespace Model.Services.UnitTests
{
    public class BlockDestroyServiceTests
    {
        private IValidationService validation = Substitute.For<IValidationService>();
        private int eventCount;

        private (GameBoard gameBoard, BlockDestroyService service) Setup(Block block, bool validationReturn = true)
        {
            validation.BlockExistsAt(default).ReturnsForAnyArgs(validationReturn);
            var gameBoard = TestUtils.CreateGameBoard(1, 1, 0);
            var blockFactory = new BlockFactory();
            var service = new BlockDestroyService(validation, blockFactory);
            eventCount = 0;

            if (block != null)
            {
                gameBoard.Cells[block.Position.x, block.Position.y].SetBlock(block);
                gameBoard.RegisterBlock(block);
                block.OnDestroy += (_) => eventCount++;
            }

            return (gameBoard, service);
        }

        [Test]
        public void Destroy_ValidBlock_BlocksDestroyed()
        {
            Block block = TestUtils.CreateBlock(TestUtils.RED_BLOCK);
            var (gameBoard, service) = Setup(block);

            service.Destroy(gameBoard, gameBoard.Cells[0, 0]);

            Assert.AreEqual(0, gameBoard.Blocks.Count);
            Assert.AreEqual(1, eventCount);
            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
        }

        [Test]
        public void Destroy_InValidBlock_BlocksDestroyed()
        {
            Block block = null;
            var (gameBoard, service) = Setup(block);

            service.Destroy(gameBoard, gameBoard.Cells[0, 0]);

            Assert.AreEqual(0, gameBoard.Blocks.Count);
            Assert.AreEqual(0, eventCount);
            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
        }

        [Test]
        public void Destroy_InValidCell_BlocksDestroyed()
        {
            Block block = TestUtils.CreateBlock(TestUtils.RED_BLOCK);
            var (gameBoard, service) = Setup(block, false);

            service.Destroy(gameBoard, new Vector2Int(100, 100));

            Assert.AreEqual(1, gameBoard.Blocks.Count);
            Assert.AreEqual(0, eventCount);
            Assert.AreEqual(block, gameBoard.Cells[0, 0].Block);
        }
    }
}