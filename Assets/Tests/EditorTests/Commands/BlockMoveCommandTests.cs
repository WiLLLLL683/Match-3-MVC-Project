using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using TestUtils;
using Model.Services;

namespace Model.Infrastructure.Commands.UnitTests
{
    public class BlockMoveCommandTests
    {
        private (GameBoard gameBoard, BlockMoveCommand command) Setup()
        {
            var game = TestLevelFactory.CreateGame(1, 2);
            var validation = new ValidationService(game);
            var setBlock = new CellSetBlockService();
            var service = new BlockMoveService(game, validation, setBlock);
            var command = new BlockMoveCommand(new(0, 0), new(0, 1), service);

            return (game.CurrentLevel.gameBoard, command);
        }

        [Test]
        public void Execute_BlockToBlock_BlocksSwapped()
        {
            var (gameBoard, command) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0,0], gameBoard);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.BLUE_BLOCK, gameBoard.Cells[0,1], gameBoard);

            command.Execute();

            Assert.AreEqual(blockB, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(blockA, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Undo_BlockToBlock_BlocksSwappedBack()
        {
            var (gameBoard, command) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.BLUE_BLOCK, gameBoard.Cells[0, 1], gameBoard);

            command.Execute();

            Assert.AreEqual(blockB, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(blockA, gameBoard.Cells[0, 1].Block);

            command.Undo();

            Assert.AreEqual(blockA, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Execute_BlockToEmpty_BlockMoved()
        {
            var (gameBoard, command) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(blockA, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Undo_BlockToEmpty_BlockMovedBack()
        {
            var (gameBoard, command) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(blockA, gameBoard.Cells[0, 1].Block);

            command.Undo();

            Assert.AreEqual(blockA, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Execute_EmptyToBlock_NoChange()
        {
            var (gameBoard, command) = Setup();
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 1], gameBoard);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Undo_EmptyToBlock_NoChange()
        {
            var (gameBoard, command) = Setup();
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 1], gameBoard);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 1].Block);

            command.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Execute_EmptyToEmpty_NoChange()
        {
            var (gameBoard, command) = Setup();

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Undo_EmptyToEmpty_NoChange()
        {
            var (gameBoard, command) = Setup();

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1].Block);

            command.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Execute_NullCellToBlock_NoChange()
        {
            var (gameBoard, command) = Setup();
            gameBoard.Cells[0, 0] = null;
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 1], gameBoard);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Undo_NullCellToBlock_NoChange()
        {
            var (gameBoard, command) = Setup();
            gameBoard.Cells[0, 0] = null;
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 1], gameBoard);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 1].Block);

            command.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Execute_BlockToNullCell_NoChange()
        {
            var (gameBoard, command) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            gameBoard.Cells[0, 1] = null;

            command.Execute();

            Assert.AreEqual(blockA, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1]);
        }

        [Test]
        public void Undo_BlockToNullCell_NoChange()
        {
            var (gameBoard, command) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            gameBoard.Cells[0, 1] = null;

            command.Execute();

            Assert.AreEqual(blockA, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1]);

            command.Undo();

            Assert.AreEqual(blockA, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1]);
        }
    }
}
