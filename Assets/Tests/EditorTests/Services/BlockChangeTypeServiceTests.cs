using Model.Objects;
using Model.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestUtils;
using UnityEngine;

namespace Model.Services.UnitTests
{
    public class BlockChangeTypeServiceTests
    {
        private int eventCount = 0;

        private (GameBoard gameBoard, BlockChangeTypeService service) Setup()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(1, 1, 0);
            var validation = new ValidationService();
            var service = new BlockChangeTypeService(validation);
            validation.SetLevel(gameBoard);
            service.SetLevel(gameBoard);
            eventCount = 0;
            service.OnTypeChange += (_) => eventCount++;

            return (gameBoard, service);
        }

        [Test]
        public void ChangeBlockType_ValidBlock_TypeChanged()
        {
            var (gameBoard, service) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            var initialType = block.Type;
            var targetType = TestBlockFactory.RedBlockType;

            service.ChangeBlockType(new Vector2Int(0, 0), targetType);

            Assert.AreEqual(targetType, gameBoard.Cells[0, 0].Block.Type);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void ChangeBlockType_NoBlock_NoChange()
        {
            var (gameBoard, service) = Setup();
            var targetType = TestBlockFactory.RedBlockType;

            service.ChangeBlockType(new Vector2Int(0, 0), targetType);

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void ChangeBlockType_NoCell_NoChange()
        {
            var (gameBoard, service) = Setup();
            gameBoard.Cells[0, 0] = null;
            var targetType = TestBlockFactory.RedBlockType;

            service.ChangeBlockType(new Vector2Int(0, 0), targetType);

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void ChangeBlockType_InValidPosition_NoChange()
        {
            var (gameBoard, service) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            var initialType = block.Type;
            var targetType = TestBlockFactory.RedBlockType;

            service.ChangeBlockType(new Vector2Int(999, 999), targetType);

            Assert.AreEqual(initialType, gameBoard.Cells[0, 0].Block.Type);
            Assert.AreEqual(0, eventCount);
        }
    }
}