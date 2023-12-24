using Model.Objects;
using NUnit.Framework;
using TestUtils;

namespace Model.Services.UnitTests
{
    public class GravityOptimizedServiceTests
    {
        private (GameBoard gameBoard, BlockGravityOptimizedService service) Setup(int xLength, int yLength)
        {
            var game = TestLevelFactory.CreateGame(xLength, yLength);
            var validation = new ValidationService(game);
            var setBlockService = new CellSetBlockService();
            var moveService = new BlockMoveService(game, validation, setBlockService);

            var service = new BlockGravityOptimizedService(game, validation, moveService);

            return (game.CurrentLevel.gameBoard, service);
        }

        [Test]
        public void Execute_OneBlockOneEmptyCellUnder_BlockMovesDown()
        {
            var (gameBoard, service) = Setup(1, 2);
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            service.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(block, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Execute_TwoBlocksNoEmptyCellUnder_NoChange()
        {
            var (gameBoard, service) = Setup(1, 2);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 1], gameBoard);

            service.Execute();

            Assert.AreEqual(blockA, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Execute_OneBlockNotPlayableCellUnder_NoChange()
        {
            var (gameBoard, service) = Setup(1, 2);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            gameBoard.Cells[0, 1].Type = TestCellFactory.NotPlayableCellType;

            service.Execute();

            Assert.AreEqual(blockA, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Execute_TwoBlocksEmptyCellUnder_TwoBlocksMoveDown()
        {
            var (gameBoard, service) = Setup(1, 3);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 1], gameBoard);

            service.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(blockA, gameBoard.Cells[0, 1].Block);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 2].Block);
        }

        [Test]
        public void Execute_AllEmptyCells_NoChange()
        {
            var (gameBoard, service) = Setup(1, 2);

            service.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public void Execute_3Blocks2EmptyCellUnder_3BlocksMoveDown()
        {
            var (gameBoard, service) = Setup(3, 3);
            var block1 = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            var block2 = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1, 0], gameBoard);
            var block3 = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 0], gameBoard);

            service.Execute();

            Assert.AreEqual(block1, gameBoard.Cells[0, 2].Block);
            Assert.AreEqual(block2, gameBoard.Cells[1, 2].Block);
            Assert.AreEqual(block3, gameBoard.Cells[2, 2].Block);
        }
    }
}
