using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Systems.UnitTests
{
    public class SwitchSystemTests
    {
        [Test]
        public void Switch_ValidTurn_BlocksSwaped()
        {
            Level level = new(2, 1);
            Block blockA = level.gameBoard.cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = level.gameBoard.cells[1, 0].SpawnBlock(new RedBlockType());
            MoveSystem switchSystem = new MoveSystem(level);

            IAction action = switchSystem.Move(new Vector2Int(0, 0), Directions.Right);
            if (action != null) 
                action.Execute();

            Assert.AreSame(blockB, level.gameBoard.cells[0,0].block);
            Assert.AreSame(blockA, level.gameBoard.cells[1,0].block);
        }

        [Test]
        public void Switch_InValidStartPos_NoChange()
        {
            Level level = new(2,1);
            Block blockA = level.gameBoard.cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = level.gameBoard.cells[1, 0].SpawnBlock(new RedBlockType());
            MoveSystem switchSystem = new MoveSystem(level);

            IAction action = switchSystem.Move(new Vector2Int(100, 100), Directions.Right);
            if (action != null)
                action.Execute();

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            Assert.AreSame(blockA, level.gameBoard.cells[0,0].block);
            Assert.AreSame(blockB, level.gameBoard.cells[1,0].block);
        }

        [Test]
        public void Switch_InValidTargetPos_NoChange()
        {
            Level level = new(2, 1);
            Block blockA = level.gameBoard.cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = level.gameBoard.cells[1, 0].SpawnBlock(new RedBlockType());
            MoveSystem switchSystem = new MoveSystem(level);

            IAction action = switchSystem.Move(new Vector2Int(0, 0), Directions.Up);
            if (action != null)
                action.Execute();

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            Assert.AreSame(blockA, level.gameBoard.cells[0,0].block);
            Assert.AreSame(blockB, level.gameBoard.cells[1,0].block);
        }
    }
}