using Model.Objects;
using Model.Services;
using NSubstitute;
using NUnit.Framework;
using UnitTests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Systems.UnitTests
{
    public class MoveSystemTests
    {
        private IValidationService validation = Substitute.For<IValidationService>();

        [Test]
        public void Move_ValidTurn_BlocksSwapped()
        {
            Level level = TestUtils.CreateLevel(2, 1);
            Block blockA = level.gameBoard.cells[0, 0].SpawnBlock(new BasicBlockType());
            Block blockB = level.gameBoard.cells[1, 0].SpawnBlock(new BasicBlockType());
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            MoveSystem moveSystem = new MoveSystem(validation);
            moveSystem.SetLevel(level);

            IAction action = moveSystem.Move(new Vector2Int(0, 0), Directions.Right);
            if (action != null) 
                action.Execute();

            Assert.AreSame(blockB, level.gameBoard.cells[0,0].Block);
            Assert.AreSame(blockA, level.gameBoard.cells[1,0].Block);
        }

        [Test]
        public void Move_InValidStartPos_NoChange()
        {
            Level level = TestUtils.CreateLevel(2,1);
            Block blockA = level.gameBoard.cells[0, 0].SpawnBlock(new BasicBlockType());
            Block blockB = level.gameBoard.cells[1, 0].SpawnBlock(new BasicBlockType());
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            MoveSystem moveSystem = new MoveSystem(validation);
            moveSystem.SetLevel(level);

            IAction action = moveSystem.Move(new Vector2Int(100, 100), Directions.Right);
            if (action != null)
                action.Execute();

            LogAssert.Expect(LogType.Warning, "Cell position out of GameBoards range");
            Assert.AreSame(blockA, level.gameBoard.cells[0,0].Block);
            Assert.AreSame(blockB, level.gameBoard.cells[1,0].Block);
        }

        [Test]
        public void Move_InValidTargetPos_NoChange()
        {
            Level level = TestUtils.CreateLevel(2, 1);
            Block blockA = level.gameBoard.cells[0, 0].SpawnBlock(new BasicBlockType());
            Block blockB = level.gameBoard.cells[1, 0].SpawnBlock(new BasicBlockType());
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
            MoveSystem moveSystem = new MoveSystem(validation);
            moveSystem.SetLevel(level);

            IAction action = moveSystem.Move(new Vector2Int(0, 0), Directions.Up);
            if (action != null)
                action.Execute();

            LogAssert.Expect(LogType.Warning, "Cell position out of GameBoards range");
            Assert.AreSame(blockA, level.gameBoard.cells[0,0].Block);
            Assert.AreSame(blockB, level.gameBoard.cells[1,0].Block);
        }
    }
}