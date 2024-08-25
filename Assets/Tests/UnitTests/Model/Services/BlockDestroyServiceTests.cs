using Cysharp.Threading.Tasks;
using Model.Factories;
using Model.Objects;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestUtils;
using UnityEngine;

namespace Model.Services.UnitTests
{
    public class BlockDestroyServiceTests
    {
        private int destroyEventCount;

        private (GameBoard gameBoard, BlockDestroyService service) Setup()
        {
            var game = TestLevelFactory.CreateGame(1, 1);
            var setBlock = new CellSetBlockService();
            var validation = new ValidationService(game);
            var service = new BlockDestroyService(game, validation, setBlock);
            destroyEventCount = 0;
            service.OnDestroy += (_) => destroyEventCount++;

            return (game.CurrentLevel.gameBoard, service);
        }

        [Test]
        public void FindMarkedBlocks_MarkedBlock_BlockReturned()
        {
            var (gameBoard, service) = Setup();
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            service.MarkToDestroy(new Vector2Int(0, 0));

            List<Block> markedBlocks = service.FindMarkedBlocks();

            Assert.AreEqual(1, markedBlocks.Count);
            Assert.AreEqual(gameBoard.Cells[0, 0].Block, markedBlocks[0]);
        }

        [Test]
        public void FindMarkedBlocks_NoMarkedBlock_EmptyList()
        {
            var (gameBoard, service) = Setup();

            List<Block> markedBlocks = service.FindMarkedBlocks();

            Assert.AreEqual(0, markedBlocks.Count);
        }

        [Test]
        public void MarkToDestroy_ValidBlock_BlockIsMarked()
        {
            var (gameBoard, service) = Setup();
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0,0], gameBoard);

            service.MarkToDestroy(new Vector2Int(0, 0));

            Assert.AreEqual(true, gameBoard.Cells[0, 0].Block.isMarkedToDestroy);
        }

        [Test]
        public void MarkToDestroy_EmptyCell_NoChange()
        {
            var (gameBoard, service) = Setup();

            service.MarkToDestroy(new Vector2Int(0, 0));

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
        }

        [Test]
        public void MarkToDestroy_InvalidPosition_NoChange()
        {
            var (gameBoard, service) = Setup();

            service.MarkToDestroy(new Vector2Int(99, 99));

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
        }

        [Test]
        public void DestroyAllMarkedBlocks_MarkedBlock_BlockDestroyed()
        {
            var (gameBoard, service) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            service.MarkToDestroy(new Vector2Int(0, 0));

            service.DestroyAllMarkedBlocks();

            Assert.AreEqual(1, destroyEventCount);
            Assert.IsTrue(gameBoard.Cells[0, 0].Block == null);
        }

        [Test]
        public void DestroyAllMarkedBlocks_NoMarkedBlock_NoChange()
        {
            var (gameBoard, service) = Setup();

            service.DestroyAllMarkedBlocks();

            Assert.AreEqual(0, destroyEventCount);
            Assert.IsTrue(gameBoard.Cells[0, 0].Block == null);
        }
    }
}