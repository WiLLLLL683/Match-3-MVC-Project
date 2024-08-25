using Config;
using Model.Factories;
using Model.Objects;
using NSubstitute;
using NUnit.Framework;
using TestUtils;
using Zenject;

namespace Model.Services.UnitTests
{
    public class BlockSpawnServiceTests
    {
        private (BlockSpawnService service, GameBoard gameBoard) Setup(
            int xLength,
            int yLength,
            int invisibleRows,
            int factoryReturnBlockType = TestBlockFactory.DEFAULT_BLOCK)
        {
            var game = TestLevelFactory.CreateGame(xLength, yLength);
            game.CurrentLevel.gameBoard = TestLevelFactory.CreateGameBoard(xLength, yLength, invisibleRows);
            var blockFactory = Substitute.For<IBlockFactory>();
            blockFactory.Create(default, default).ReturnsForAnyArgs(TestBlockFactory.CreateBlock(TestBlockFactory.DEFAULT_BLOCK));
            var validation = new ValidationService(game);
            var setBlockService = new CellSetBlockService();
            var blockTypeFactory = Substitute.For<IBlockTypeFactory>();
            blockTypeFactory.Create(Arg.Any<int>()).ReturnsForAnyArgs(TestBlockFactory.CreateBlockType(factoryReturnBlockType));
            var changeTypeService = new BlockChangeTypeService(game, validation);

            var service = new BlockSpawnService(game, blockFactory, blockTypeFactory, validation, changeTypeService, setBlockService);
            return (service, game.CurrentLevel.gameBoard);
        }

        [Test]
        public void FillInvisibleRows_2cellsGameBoard_OnlyTopLineSpawned()
        {
            var tuple = Setup(1, 2, 1);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;

            service.FillHiddenRows();

            Assert.IsFalse(gameBoard.Cells[0, 1].Block == null);
            Assert.IsTrue(gameBoard.Cells[0, 0].Block == null);
        }

        [Test]
        public void FillInvisibleRows_1cellGameBoard_OnlyTopLineSpawned()
        {
            var tuple = Setup(1, 1, 1);
            var service = tuple.service;
            var gameBoard = tuple.gameBoard;

            service.FillHiddenRows();

            Assert.IsFalse(gameBoard.Cells[0, 0].Block == null);
        }

        [Test]
        public void FillInvisibleRows_9cellsGameBoard_OnlyTopLineSpawned()
        {
            var (service, gameBoard) = Setup(3, 3, 1);

            service.FillHiddenRows();

            int hiddenCellStartIndex = gameBoard.Cells.GetLength(1) - 1;
            for (int x = 0; x < gameBoard.Cells.GetLength(0); x++) //первая полоса заполнена
            {
                Assert.IsFalse(gameBoard.Cells[x, hiddenCellStartIndex].Block == null);
            }
            for (int y = 0; y < hiddenCellStartIndex; y++) //остольные полосы пусты
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
            var (service, gameBoard) = Setup(1, 1, 0, TestBlockFactory.RED_BLOCK);

            service.SpawnBlock_WithOverride(gameBoard.Cells[0, 0], TestBlockFactory.RedBlockType);

            Assert.IsFalse(gameBoard.Cells[0, 0].Block == null);
            Assert.That(gameBoard.Cells[0, 0].Block.Type.Id == TestBlockFactory.RED_BLOCK);
        }

        [Test]
        public void SpawnBlock_FullCell_BlockTypeChanged()
        {
            var (service, gameBoard) = Setup(1, 1, 1, factoryReturnBlockType: TestBlockFactory.RED_BLOCK);
            TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            var blockTypeBefore = gameBoard.Cells[0, 0].Block.Type;

            service.SpawnBlock_WithOverride(gameBoard.Cells[0, 0], TestBlockFactory.RedBlockType);

            Assert.IsFalse(gameBoard.Cells[0, 0].Block == null);
            Assert.That(blockTypeBefore.Id == TestBlockFactory.DEFAULT_BLOCK);
            Assert.That(gameBoard.Cells[0, 0].Block.Type.Id == TestBlockFactory.RED_BLOCK);
        }

        [Test]
        public void SpawnBlock_NotPlayableCell_Nothing()
        {
            var (service, gameBoard) = Setup(1, 1, 1, TestBlockFactory.RED_BLOCK);
            gameBoard.Cells[0, 0].Type = TestCellFactory.NotPlayableCellType;

            service.SpawnBlock_WithOverride(gameBoard.Cells[0, 0], TestBlockFactory.RedBlockType);

            Assert.IsTrue(gameBoard.Cells[0, 0].Block == null);
            Assert.IsFalse(gameBoard.Cells[0, 0].Type.IsPlayable);
            Assert.IsFalse(gameBoard.Cells[0, 0].Type.CanContainBlock);
            Assert.That(gameBoard.Cells[0, 0].Block == null);
        }
    }
}