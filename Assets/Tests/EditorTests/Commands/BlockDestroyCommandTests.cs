﻿using Model.Factories;
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

        private (GameBoard gameBoard, IBlockDestroyService destroy, IBlockSpawnService spawn) Setup()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(1, 1, 0);
            var validation = new ValidationService();
            var setBlock = new CellSetBlockService();
            var random = TestServicesFactory.CreateRandomBlockTypeService();
            var changeType = new BlockChangeTypeService(validation);
            var factory = new BlockFactory();
            var spawn = new BlockSpawnService(factory, validation, random, changeType, setBlock);
            var destroy = new BlockDestroyService(validation, setBlock);
            validation.SetLevel(gameBoard);
            destroy.SetLevel(gameBoard);
            changeType.SetLevel(gameBoard);
            spawn.SetLevel(gameBoard);

            eventCount = 0;
            destroy.OnDestroy += (_) => eventCount++;

            return (gameBoard, destroy, spawn);
        }

        [Test]
        public void Execute_ValidBlock_BlockDestroyed()
        {
            var (gameBoard, destroy, spawn) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0,0], gameBoard);
            var command = new BlockDestroyCommand(gameBoard.Cells[0, 0], destroy, spawn);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0,0].Block);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void Undo_ValidBlock_BlockSpawned()
        {
            var (gameBoard, destroy, spawn) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            var command = new BlockDestroyCommand(gameBoard.Cells[0, 0], destroy, spawn);

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
            var (gameBoard, destroy, spawn) = Setup();
            var command = new BlockDestroyCommand(gameBoard.Cells[0, 0], destroy, spawn);
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            block.Type = null;

            command.Execute();

            Assert.AreEqual(block, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Undo_NullType_NoChange()
        {
            var (gameBoard, destroy, spawn) = Setup();
            var command = new BlockDestroyCommand(gameBoard.Cells[0, 0], destroy, spawn);
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
            var (gameBoard, destroy, spawn) = Setup();
            var command = new BlockDestroyCommand(gameBoard.Cells[0, 0], destroy, spawn);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Undo_NullBlock_NoChange()
        {
            var (gameBoard, destroy, spawn) = Setup();
            var command = new BlockDestroyCommand(gameBoard.Cells[0, 0], destroy, spawn);

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
            var (gameBoard, destroy, spawn) = Setup();
            var command = new BlockDestroyCommand(gameBoard.Cells[0, 0], destroy, spawn);
            gameBoard.Cells[0, 0] = null;

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Undo_NullCell_NoChange()
        {
            var (gameBoard, destroy, spawn) = Setup();
            var command = new BlockDestroyCommand(gameBoard.Cells[0, 0], destroy, spawn);
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