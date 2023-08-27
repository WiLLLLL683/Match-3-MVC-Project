using Data;
using Model.Factories;
using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using UnitTests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Services.UnitTests
{
    public class BlockSpawnServiceTests
    {
        private (BlockSpawnService service, GameBoard gameBoard) Setup(int xLength,
            int yLength,
            int invisibleRows,
            int factoryReturnBlockType = TestUtils.DEFAULT_BLOCK,
            bool validationCellReturn = true,
            bool validationBlockReturn = true,
            params int[] preSpawnedBlocks)
        {
            var gameBoard = TestUtils.CreateGameBoard(xLength, yLength, preSpawnedBlocks);
            gameBoard.rowsOfInvisibleCells = invisibleRows;

            var balance = TestUtils.CreateBalance(TestUtils.DEFAULT_BLOCK);

            var blockFactory = Substitute.For<IBlockFactory>();
            blockFactory.Create(Arg.Any<IBlockType>(), Arg.Any<Cell>()).Returns(x => TestUtils.CreateBlock(factoryReturnBlockType, x.Arg<Cell>()));

            var validation = Substitute.For<IValidationService>();
            validation.CellExistsAt(default).ReturnsForAnyArgs(validationCellReturn);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(validationBlockReturn);

            var service = new BlockSpawnService(blockFactory, validation);
            service.SetLevel(gameBoard, balance);
            return (service, gameBoard);
        }

        [Test]
        public void FillInvisibleRows_2cellsGameBoard_OnlyTopLineSpawned()
        {
            var tuple = Setup(1, 2, 1);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;

            service.FillInvisibleRows();

            Assert.IsFalse(gameBoard.cells[0, 0].IsEmpty);
            Assert.IsTrue(gameBoard.cells[0, 1].IsEmpty);
        }

        [Test]
        public void FillInvisibleRows_1cellGameBoard_OnlyTopLineSpawned()
        {
            var tuple = Setup(1, 1, 1);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;

            service.FillInvisibleRows();

            Assert.IsFalse(gameBoard.cells[0, 0].IsEmpty);
        }

        [Test]
        public void FillInvisibleRows_9cellsGameBoard_OnlyTopLineSpawned()
        {
            var tuple = Setup(3, 3, 1);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;

            service.FillInvisibleRows();

            for (int x = 0; x < gameBoard.cells.GetLength(0); x++) //первая полоса заполнена
            {
                Assert.IsFalse(gameBoard.cells[x, 0].IsEmpty);
            }

            for (int y = 1; y < gameBoard.cells.GetLength(1); y++) //остольные полосы пусты
            {
                for (int x = 0; x < gameBoard.cells.GetLength(0); x++)
                {
                    Assert.IsTrue(gameBoard.cells[0, 1].IsEmpty);
                }
            }
        }

        [Test]
        public void SpawnBlock_EmptyCell_BonusBlockSpawned()
        {
            var tuple = Setup(1, 1, 0, TestUtils.RED_BLOCK);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;

            service.SpawnBlock_WithOverride(TestUtils.RedBlockType, gameBoard.cells[0,0]);
            
            Assert.IsFalse(gameBoard.cells[0, 0].IsEmpty);
            Assert.That(gameBoard.cells[0, 0].Block.Type.Id == TestUtils.RED_BLOCK);
        }

        [Test]
        public void SpawnBlock_FullCell_BlockTypeChanged()
        {
            var tuple = Setup(xLength: 1,
                              yLength: 1,
                              invisibleRows: 1,
                              factoryReturnBlockType: TestUtils.RED_BLOCK,
                              validationCellReturn: true,
                              validationBlockReturn: true,
                              preSpawnedBlocks: TestUtils.DEFAULT_BLOCK);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;
            var blockTypeBefore = gameBoard.cells[0, 0].Block.Type;

            service.SpawnBlock_WithOverride(TestUtils.RedBlockType, gameBoard.cells[0, 0]);

            Assert.IsFalse(gameBoard.cells[0, 0].IsEmpty);
            Assert.That(blockTypeBefore.Id == TestUtils.DEFAULT_BLOCK);
            Assert.That(gameBoard.cells[0, 0].Block.Type.Id == TestUtils.RED_BLOCK);
        }

        [Test]
        public void SpawnBlock_NotPlayableCell_Nothing()
        {
            var tuple = Setup(1, 1, 1, TestUtils.RED_BLOCK, false);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;
            gameBoard.cells[0, 0].ChangeType(TestUtils.NotPlayableCellType);

            service.SpawnBlock_WithOverride(TestUtils.RedBlockType, gameBoard.cells[0, 0]);

            Assert.IsTrue(gameBoard.cells[0, 0].IsEmpty);
            Assert.IsFalse(gameBoard.cells[0, 0].IsPlayable);
            Assert.IsFalse(gameBoard.cells[0, 0].CanContainBlock);
            Assert.That(gameBoard.cells[0, 0].Block == null);
        }
    }
}