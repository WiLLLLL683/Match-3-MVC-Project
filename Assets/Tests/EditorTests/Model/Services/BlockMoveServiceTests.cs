using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using TestUtils;
using UnityEngine;
using UnityEngine.TestTools;
using Utils;

namespace Model.Services.UnitTests
{
    public class BlockMoveServiceTests
    {
        private int eventCount = 0;

        private (BlockMoveService service, GameBoard gameBoard) Setup()
        {
            var game = TestLevelFactory.CreateGame(2, 1);
            var validation = new ValidationService(game);
            var setBlock = new CellSetBlockService();
            var service = new BlockMoveService(game, validation, setBlock);
            eventCount = 0;
            service.OnPositionChange += (_) => eventCount++;

            return (service, game.CurrentLevel.gameBoard);
        }

        [Test]
        public void Move_BlockToBlock_BlocksSwapped()
        {
            var (service, gameBoard) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0]);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[1, 0]);

            service.Move(new Vector2Int(0, 0), Directions.Right);

            Assert.AreEqual(blockB.Type.Id, gameBoard.Cells[0,0].Block.Type.Id);
            Assert.AreEqual(blockA.Type.Id, gameBoard.Cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_BlockToEmpty_BlockMoved()
        {
            var (service, gameBoard) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0]);

            service.Move(new Vector2Int(0, 0), Directions.Right);

            Assert.AreEqual(blockA.Type.Id, gameBoard.Cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_StartPosOutOfGameBoard_NoChange()
        {
            var (service, gameBoard) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0]);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[1, 0]);

            service.Move(new Vector2Int(100, 100), Directions.Right);

            Assert.AreEqual(blockA.Type.Id, gameBoard.Cells[0,0].Block.Type.Id);
            Assert.AreEqual(blockB.Type.Id, gameBoard.Cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_TargetPosOutOfGameBoard_NoChange()
        {
            var (service, gameBoard) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0]);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[1, 0]);

            service.Move(new Vector2Int(0, 0), Directions.Up);

            Assert.AreEqual(blockA.Type.Id, gameBoard.Cells[0,0].Block.Type.Id);
            Assert.AreEqual(blockB.Type.Id, gameBoard.Cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_EmptyStartCell_NoChange()
        {
            var (service, gameBoard) = Setup();
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[1, 0]);

            service.Move(new Vector2Int(0, 0), Directions.Up);

            Assert.IsTrue(gameBoard.Cells[0, 0].Block == null);
            Assert.AreEqual(blockB.Type.Id, gameBoard.Cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_StartCellIsNull_NoChange()
        {
            var (service, gameBoard) = Setup();
            gameBoard.Cells[0, 0] = null;
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[1, 0]);

            service.Move(new Vector2Int(0, 0), Directions.Up);

            Assert.IsTrue(gameBoard.Cells[0, 0] == null);
            Assert.AreEqual(blockB.Type.Id, gameBoard.Cells[1, 0].Block.Type.Id);
        }

        [Test]
        public void Move_TargetCellIsNull_NoChange()
        {
            var (service, gameBoard) = Setup();
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0]);
            gameBoard.Cells[1, 0] = null;

            service.Move(new Vector2Int(0, 0), Directions.Up);

            Assert.AreEqual(blockA.Type.Id, gameBoard.Cells[0, 0].Block.Type.Id);
            Assert.IsTrue(gameBoard.Cells[1, 0] == null);
        }
    }
}