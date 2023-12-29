using Model.Objects;
using NUnit.Framework;
using System.Threading.Tasks;
using TestUtils;
using UnityEngine;

namespace Model.Services.UnitTests
{
    public class GravityServiceTests
    {
        private (GameBoard gameBoard, BlockGravityService service) Setup(int xLength, int yLength)
        {
            var game = TestLevelFactory.CreateGame(xLength, yLength);
            var validation = new ValidationService(game);
            var setBlockService = new CellSetBlockService();
            var moveService = new BlockMoveService(game, validation, setBlockService);

            var service = new BlockGravityService(game, validation, moveService);

            return (game.CurrentLevel.gameBoard, service);
        }

        [Test]
        public async Task Execute_OneBlockOneEmptyCellUnder_BlockMovesDown()
        {
            var (gameBoard, service) = Setup(1, 2);
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 1], gameBoard);

            await service.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 1].Block);
            Assert.AreEqual(block, gameBoard.Cells[0, 0].Block);
        }

        [Test]
        public async Task Execute_TwoBlocksNoEmptyCellUnder_NoChange()
        {
            var (gameBoard, service) = Setup(1, 2);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 1], gameBoard);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            await service.Execute();

            Assert.AreEqual(blockA, gameBoard.Cells[0, 1].Block);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 0].Block);
        }

        [Test]
        public async Task Execute_OneBlockNotPlayableCellUnder_NoChange()
        {
            var (gameBoard, service) = Setup(1, 2);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            gameBoard.Cells[0, 1].Type = TestCellFactory.NotPlayableCellType;

            await service.Execute();

            Assert.AreEqual(blockA, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public async Task Execute_TwoBlocksEmptyCellUnder_TwoBlocksMoveDown()
        {
            var (gameBoard, service) = Setup(1, 3);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 2], gameBoard);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 1], gameBoard);

            await service.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 2].Block);
            Assert.AreEqual(blockA, gameBoard.Cells[0, 1].Block);
            Assert.AreEqual(blockB, gameBoard.Cells[0, 0].Block);
        }

        [Test]
        public async Task Execute_AllEmptyCells_NoChange()
        {
            var (gameBoard, service) = Setup(1, 2);

            await service.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(null, gameBoard.Cells[0, 1].Block);
        }

        [Test]
        public async Task Execute_3Blocks2EmptyCellUnder_3BlocksMoveDown()
        {
            var (gameBoard, service) = Setup(3, 3);
            var block1 = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 2], gameBoard);
            var block2 = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1, 2], gameBoard);
            var block3 = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[2, 2], gameBoard);

            await service.Execute();

            Assert.AreEqual(block1, gameBoard.Cells[0, 0].Block);
            Assert.AreEqual(block2, gameBoard.Cells[1, 0].Block);
            Assert.AreEqual(block3, gameBoard.Cells[2, 0].Block);
        }
    }
}
