using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using UnitTests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Services.UnitTests
{
    public class MoveServiceTests
    {
        private IValidationService validation = Substitute.For<IValidationService>();

        [Test]
        public void Move_BlockToBlock_BlocksSwapped()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(2, 1);
            Block blockA = TestUtils.CreateBlockInCell(TestUtils.RED_BLOCK, gameBoard.cells[0, 0]);
            Block blockB = TestUtils.CreateBlockInCell(TestUtils.RED_BLOCK, gameBoard.cells[1, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            validation.CellExistsAt(default).ReturnsForAnyArgs(true);
            MoveService moveSystem = new MoveService(validation);

            moveSystem.Move(gameBoard, new Vector2Int(0, 0), Directions.Right)?.Execute();

            Assert.AreEqual(blockB.Type.Id, gameBoard.cells[0,0].Block.Type.Id);
            Assert.AreEqual(blockA.Type.Id, gameBoard.cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_BlockToEmpty_BlockMoved()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(2, 1);
            Block blockA = TestUtils.CreateBlockInCell(TestUtils.RED_BLOCK, gameBoard.cells[0, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            validation.CellExistsAt(default).ReturnsForAnyArgs(true);
            MoveService moveSystem = new MoveService(validation);

            moveSystem.Move(gameBoard, new Vector2Int(0, 0), Directions.Right)?.Execute();

            Assert.AreEqual(blockA.Type.Id, gameBoard.cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_StartPosOutOfGameBoard_NoChange()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(2,1);
            Block blockA = TestUtils.CreateBlockInCell(TestUtils.RED_BLOCK, gameBoard.cells[0, 0]);
            Block blockB = TestUtils.CreateBlockInCell(TestUtils.RED_BLOCK, gameBoard.cells[1, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            validation.CellExistsAt(default).ReturnsForAnyArgs(false);
            MoveService moveSystem = new MoveService(validation);

            moveSystem.Move(gameBoard, new Vector2Int(100, 100), Directions.Right)?.Execute();

            Assert.AreEqual(blockA.Type.Id, gameBoard.cells[0,0].Block.Type.Id);
            Assert.AreEqual(blockB.Type.Id, gameBoard.cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_TargetPosOutOfGameBoard_NoChange()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(2, 1);
            Block blockA = TestUtils.CreateBlockInCell(TestUtils.RED_BLOCK, gameBoard.cells[0, 0]);
            Block blockB = TestUtils.CreateBlockInCell(TestUtils.RED_BLOCK, gameBoard.cells[1, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            validation.CellExistsAt(default).ReturnsForAnyArgs(false);
            MoveService moveSystem = new MoveService(validation);

            moveSystem.Move(gameBoard, new Vector2Int(0, 0), Directions.Up)?.Execute();

            Assert.AreEqual(blockA.Type.Id, gameBoard.cells[0,0].Block.Type.Id);
            Assert.AreEqual(blockB.Type.Id, gameBoard.cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_EmptyStartCell_NoChange()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(2, 1);
            Block blockB = TestUtils.CreateBlockInCell(TestUtils.RED_BLOCK, gameBoard.cells[1, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(false);
            validation.CellExistsAt(default).ReturnsForAnyArgs(true);
            MoveService moveSystem = new MoveService(validation);

            moveSystem.Move(gameBoard, new Vector2Int(0, 0), Directions.Up)?.Execute();

            Assert.IsTrue(gameBoard.cells[0, 0].IsEmpty);
            Assert.AreEqual(blockB.Type.Id, gameBoard.cells[1,0].Block.Type.Id);
        }

        [Test]
        public void Move_StartCellIsNull_NoChange()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(2, 1);
            gameBoard.cells[0, 0] = null;
            Block blockB = TestUtils.CreateBlockInCell(TestUtils.RED_BLOCK, gameBoard.cells[1, 0]);
            validation.BlockExistsAt(default).ReturnsForAnyArgs(false);
            validation.CellExistsAt(default).ReturnsForAnyArgs(false);
            MoveService moveSystem = new MoveService(validation);

            moveSystem.Move(gameBoard, new Vector2Int(0, 0), Directions.Up)?.Execute();

            Assert.IsTrue(gameBoard.cells[0, 0] == null);
            Assert.AreEqual(blockB.Type.Id, gameBoard.cells[1, 0].Block.Type.Id);
        }

        [Test]
        public void Move_TargetCellIsNull_NoChange()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(2, 1);
            Block blockA = TestUtils.CreateBlockInCell(TestUtils.RED_BLOCK, gameBoard.cells[0, 0]);
            gameBoard.cells[1, 0] = null;
            validation.BlockExistsAt(default).ReturnsForAnyArgs(false);
            validation.CellExistsAt(default).ReturnsForAnyArgs(false);
            MoveService moveSystem = new MoveService(validation);

            moveSystem.Move(gameBoard, new Vector2Int(0, 0), Directions.Up)?.Execute();

            Assert.AreEqual(blockA.Type.Id, gameBoard.cells[0, 0].Block.Type.Id);
            Assert.IsTrue(gameBoard.cells[1, 0] == null);
        }
    }
}