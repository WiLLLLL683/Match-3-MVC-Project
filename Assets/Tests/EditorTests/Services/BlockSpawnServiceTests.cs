using Config;
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
            params int[] preSpawnedBlocks)
        {
            var gameBoard = TestUtils.CreateGameBoard(xLength, yLength, invisibleRows, preSpawnedBlocks);

            var balance = TestUtils.CreateBalance(TestUtils.DEFAULT_BLOCK);

            var blockFactory = Substitute.For<IBlockFactory>();
            blockFactory.Create(Arg.Any<IBlockType>(), Arg.Any<Vector2Int>()).Returns(x => TestUtils.CreateBlock(factoryReturnBlockType, x.Arg<Vector2Int>()));

            var validation = new ValidationService();
            validation.SetLevel(gameBoard);

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

            Assert.IsFalse(gameBoard.Cells[0, 0].Block == null);
            Assert.IsTrue(gameBoard.Cells[0, 1].Block == null);
        }

        [Test]
        public void FillInvisibleRows_1cellGameBoard_OnlyTopLineSpawned()
        {
            var tuple = Setup(1, 1, 1);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;

            service.FillInvisibleRows();

            Assert.IsFalse(gameBoard.Cells[0, 0].Block == null);
        }

        [Test]
        public void FillInvisibleRows_9cellsGameBoard_OnlyTopLineSpawned()
        {
            var tuple = Setup(3, 3, 1);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;

            service.FillInvisibleRows();

            for (int x = 0; x < gameBoard.Cells.GetLength(0); x++) //первая полоса заполнена
            {
                Assert.IsFalse(gameBoard.Cells[x, 0].Block == null);
            }

            for (int y = 1; y < gameBoard.Cells.GetLength(1); y++) //остольные полосы пусты
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    Assert.IsTrue(gameBoard.Cells[0, 1].Block == null);
                }
            }
        }

        [Test]
        public void SpawnBlock_EmptyCell_BonusBlockSpawned()
        {
            var tuple = Setup(1, 1, 0, TestUtils.RED_BLOCK);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;

            service.SpawnBlock_WithOverride(TestUtils.RedBlockType, gameBoard.Cells[0,0]);
            
            Assert.IsFalse(gameBoard.Cells[0, 0].Block == null);
            Assert.That(gameBoard.Cells[0, 0].Block.Type.Id == TestUtils.RED_BLOCK);
        }

        [Test]
        public void SpawnBlock_FullCell_BlockTypeChanged()
        {
            var tuple = Setup(xLength: 1,
                              yLength: 1,
                              invisibleRows: 1,
                              factoryReturnBlockType: TestUtils.RED_BLOCK,
                              preSpawnedBlocks: TestUtils.DEFAULT_BLOCK);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;
            var blockTypeBefore = gameBoard.Cells[0, 0].Block.Type;

            service.SpawnBlock_WithOverride(TestUtils.RedBlockType, gameBoard.Cells[0, 0]);

            Assert.IsFalse(gameBoard.Cells[0, 0].Block == null);
            Assert.That(blockTypeBefore.Id == TestUtils.DEFAULT_BLOCK);
            Assert.That(gameBoard.Cells[0, 0].Block.Type.Id == TestUtils.RED_BLOCK);
        }

        [Test]
        public void SpawnBlock_NotPlayableCell_Nothing()
        {
            var tuple = Setup(1, 1, 1, TestUtils.RED_BLOCK);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;
            gameBoard.Cells[0, 0].SetType(TestUtils.NotPlayableCellType);

            service.SpawnBlock_WithOverride(TestUtils.RedBlockType, gameBoard.Cells[0, 0]);

            Assert.IsTrue(gameBoard.Cells[0, 0].Block == null);
            Assert.IsFalse(gameBoard.Cells[0, 0].Type.IsPlayable);
            Assert.IsFalse(gameBoard.Cells[0, 0].Type.CanContainBlock);
            Assert.That(gameBoard.Cells[0, 0].Block == null);
        }
    }
}