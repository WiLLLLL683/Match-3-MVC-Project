using Model.Factories;
using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using TestUtils;

namespace Infrastructure.Commands.UnitTests
{
    public class BlockSpawnCommandTests
    {
        private int spawnEventCount = 0;
        private int destroyEventCount = 0;

        private (GameBoard gameBoard, BlockSpawnService spawn, BlockDestroyService destroy, IBlockType type) Setup()
        {
            var game = TestLevelFactory.CreateGame(1, 1);
            var validation = new ValidationService(game);
            var setBlock = new CellSetBlockService();
            var destroy = new BlockDestroyService(game, validation, setBlock);
            var blockTypeFactory = Substitute.For<IBlockTypeFactory>();
            blockTypeFactory.Create(Arg.Any<int>()).ReturnsForAnyArgs(TestBlockFactory.CreateBlockType(TestBlockFactory.DEFAULT_BLOCK));
            var changeType = new BlockChangeTypeService(game, validation);
            var blockFactory = Substitute.For<IBlockFactory>();
            blockFactory.Create(default, default).ReturnsForAnyArgs(TestBlockFactory.CreateBlock(TestBlockFactory.DEFAULT_BLOCK));
            var spawn = new BlockSpawnService(game, blockFactory, blockTypeFactory, validation, changeType, setBlock);

            spawnEventCount = 0;
            destroyEventCount = 0;
            spawn.OnBlockSpawn += (_) => spawnEventCount++;
            destroy.OnDestroy += (_) => destroyEventCount++;
            var type = TestBlockFactory.RedBlockType;

            return (game.CurrentLevel.gameBoard, spawn, destroy, type);
        }

        [Test]
        public void Execute_ValidInput_BlockSpawned()
        {
            var (gameBoard, spawn, destroy, type) = Setup();
            var command = new BlockSpawnCommand(gameBoard.Cells[0, 0], type, spawn, destroy);

            command.Execute();

            Assert.AreNotEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(type, gameBoard.Cells[0, 0].Block.Type);
            Assert.AreEqual(1, spawnEventCount);
        }

        [Test]
        public void Undo_ValidInput_BlockDestroyed()
        {
            var (gameBoard, spawn, destroy, type) = Setup();
            var command = new BlockSpawnCommand(gameBoard.Cells[0, 0], type, spawn, destroy);

            command.Execute();

            Assert.AreNotEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(type, gameBoard.Cells[0, 0].Block.Type);
            Assert.AreEqual(1, spawnEventCount);

            command.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(1, destroyEventCount);
        }

        [Test]
        public void Execute_NullCell_NoChange()
        {
            var (gameBoard, spawn, destroy, type) = Setup();
            gameBoard.Cells[0, 0] = null;
            var command = new BlockSpawnCommand(gameBoard.Cells[0, 0], type, spawn, destroy);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(0, spawnEventCount);
        }

        [Test]
        public void Undo_NullCell_NoChange()
        {
            var (gameBoard, spawn, destroy, type) = Setup();
            gameBoard.Cells[0, 0] = null;
            var command = new BlockSpawnCommand(gameBoard.Cells[0, 0], type, spawn, destroy);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(0, spawnEventCount);

            command.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0]);
            Assert.AreEqual(0, spawnEventCount);
        }

        [Test]
        public void Execute_NullType_NoChange()
        {
            var (gameBoard, spawn, destroy, type) = Setup();
            var command = new BlockSpawnCommand(gameBoard.Cells[0, 0], null, spawn, destroy);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, spawnEventCount);
        }

        [Test]
        public void Undo_NullType_NoChange()
        {
            var (gameBoard, spawn, destroy, type) = Setup();
            var command = new BlockSpawnCommand(gameBoard.Cells[0, 0], null, spawn, destroy);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, spawnEventCount);

            command.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, spawnEventCount);
        }

        [Test]
        public void Execute_NullDestroyService_NoChange()
        {
            var (gameBoard, spawn, destroy, type) = Setup();
            var command = new BlockSpawnCommand(gameBoard.Cells[0, 0], type, spawn, null);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, spawnEventCount);
        }

        [Test]
        public void Undo_NullDestroyService_NoChange()
        {
            var (gameBoard, spawn, destroy, type) = Setup();
            var command = new BlockSpawnCommand(gameBoard.Cells[0, 0], type, spawn, null);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, spawnEventCount);

            command.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, spawnEventCount);
        }

        [Test]
        public void Execute_NullSpawnService_NoChange()
        {
            var (gameBoard, spawn, destroy, type) = Setup();
            var command = new BlockSpawnCommand(gameBoard.Cells[0, 0], type, null, destroy);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, spawnEventCount);
        }

        [Test]
        public void Undo_NullSpawnService_NoChange()
        {
            var (gameBoard, spawn, destroy, type) = Setup();
            var command = new BlockSpawnCommand(gameBoard.Cells[0, 0], type, null, destroy);

            command.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, spawnEventCount);

            command.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(0, spawnEventCount);
        }
    }
}