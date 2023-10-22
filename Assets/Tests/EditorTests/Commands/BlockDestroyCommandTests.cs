using Model.Objects;
using Model.Services;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestUtils;
using UnityEngine;

namespace Model.Commands.UnitTests
{
    public class BlockDestroyCommandTests
    {
        private int eventCount = 0;

        private (GameBoard gameBoard, BlockDestroyCommand command) Setup()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(1, 1, 0);
            var validation = new ValidationService();
            var setBlock = new CellSetBlockService();
            var spawn = Substitute.For<IBlockSpawnService>(); //TODO спавнить блок
            var service = new BlockDestroyService(validation, setBlock);
            validation.SetLevel(gameBoard);
            service.SetLevel(gameBoard);

            var command = new BlockDestroyCommand(gameBoard.Cells[0, 0], service, spawn);
            eventCount = 0;
            service.OnDestroy += (_) => eventCount++;

            return (gameBoard, command);
        }

        [Test]
        public void Execute_ValidBlock_BlockDestroyed()
        {
            var (gameBoard, command) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0,0], gameBoard);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0,0].Block);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void Undo_ValidBlock_BlockSpawned()
        {
            var (gameBoard, command) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(1, eventCount);

            command.Undo();

            Assert.AreEqual(block.Type.Id, gameBoard.Cells[0, 0].Block.Type.Id);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void Execute_NullType_NoChange()
        {
            var (gameBoard, command) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            block.Type = null;

            command.Execute();

            Assert.AreEqual(block, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Undo_NullType_NoChange()
        {
            var (gameBoard, command) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            block.Type = null;

            command.Execute();

            Assert.AreEqual(block, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, eventCount);

            command.Undo();

            Assert.AreEqual(block, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Execute_NullBlock_NoChange()
        {
            var (gameBoard, command) = Setup();

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Undo_NullBlock_NoChange()
        {
            var (gameBoard, command) = Setup();

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, eventCount);

            command.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Execute_NullCell_NoChange()
        {
            var (gameBoard, command) = Setup();
            gameBoard.Cells[0, 0] = null;

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Undo_NullCell_NoChange()
        {
            var (gameBoard, command) = Setup();
            gameBoard.Cells[0, 0] = null;

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(0, eventCount);

            command.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(0, eventCount);
        }
    }
}