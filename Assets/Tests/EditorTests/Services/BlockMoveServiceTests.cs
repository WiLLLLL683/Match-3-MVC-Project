using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using TestUtils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Services.UnitTests
{
    public class BlockMoveServiceTests
    {
        private IValidationService validation = Substitute.For<IValidationService>();

        [Test]
        public void Move_BlockToBlock_BlocksSwapped()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(2, 1, 0);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0]);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[1, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            validation.CellExistsAt(default).ReturnsForAnyArgs(true);
            var moveSystem = new BlockMoveService(validation);
            moveSystem.SetLevel(gameBoard);

            moveSystem.Move(new Vector2Int(0, 0), Directions.Right);

            Assert.AreEqual(blockB.Type.Id, gameBoard.Cells[0,0].Block.Type.Id);
            Assert.AreEqual(blockA.Type.Id, gameBoard.Cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_BlockToEmpty_BlockMoved()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(2, 1, 0);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            validation.CellExistsAt(default).ReturnsForAnyArgs(true);
            var moveSystem = new BlockMoveService(validation);
            moveSystem.SetLevel(gameBoard);

            moveSystem.Move(new Vector2Int(0, 0), Directions.Right);

            Assert.AreEqual(blockA.Type.Id, gameBoard.Cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_StartPosOutOfGameBoard_NoChange()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(2, 1, 0);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0]);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[1, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            validation.CellExistsAt(default).ReturnsForAnyArgs(false);
            var moveSystem = new BlockMoveService(validation);
            moveSystem.SetLevel(gameBoard);

            moveSystem.Move(new Vector2Int(100, 100), Directions.Right);

            Assert.AreEqual(blockA.Type.Id, gameBoard.Cells[0,0].Block.Type.Id);
            Assert.AreEqual(blockB.Type.Id, gameBoard.Cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_TargetPosOutOfGameBoard_NoChange()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(2, 1, 0);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0]);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[1, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            validation.CellExistsAt(default).ReturnsForAnyArgs(false);
            var moveSystem = new BlockMoveService(validation);
            moveSystem.SetLevel(gameBoard);

            moveSystem.Move(new Vector2Int(0, 0), Directions.Up);

            Assert.AreEqual(blockA.Type.Id, gameBoard.Cells[0,0].Block.Type.Id);
            Assert.AreEqual(blockB.Type.Id, gameBoard.Cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_EmptyStartCell_NoChange()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(2, 1, 0);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[1, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(false);
            validation.CellExistsAt(default).ReturnsForAnyArgs(true);
            var moveSystem = new BlockMoveService(validation);
            moveSystem.SetLevel(gameBoard);

            moveSystem.Move(new Vector2Int(0, 0), Directions.Up);

            Assert.IsTrue(gameBoard.Cells[0, 0].Block == null);
            Assert.AreEqual(blockB.Type.Id, gameBoard.Cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_StartCellIsNull_NoChange()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(2, 1, 0);
            gameBoard.Cells[0, 0] = null;
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[1, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(false);
            validation.CellExistsAt(default).ReturnsForAnyArgs(false);
            var moveSystem = new BlockMoveService(validation);
            moveSystem.SetLevel(gameBoard);

            moveSystem.Move(new Vector2Int(0, 0), Directions.Up);

            Assert.IsTrue(gameBoard.Cells[0, 0] == null);
            Assert.AreEqual(blockB.Type.Id, gameBoard.Cells[1, 0].Block.Type.Id);
        }

        [Test]
        public void Move_TargetCellIsNull_NoChange()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(2, 1, 0);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.RED_BLOCK, gameBoard.Cells[0, 0]);
            gameBoard.Cells[1, 0] = null;
            validation.BlockExistsAt(default).ReturnsForAnyArgs(false);
            validation.CellExistsAt(default).ReturnsForAnyArgs(false);
            var moveSystem = new BlockMoveService(validation);
            moveSystem.SetLevel(gameBoard);

            moveSystem.Move(new Vector2Int(0, 0), Directions.Up);

            Assert.AreEqual(blockA.Type.Id, gameBoard.Cells[0, 0].Block.Type.Id);
            Assert.IsTrue(gameBoard.Cells[1, 0] == null);
        }
    }
}