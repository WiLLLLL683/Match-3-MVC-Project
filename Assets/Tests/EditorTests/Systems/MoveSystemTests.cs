using System.Collections;
using System.Collections.Generic;
using Data;
using Model.Objects;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Systems.UnitTests
{
    public class MoveSystemTests
    {
        [Test]
        public void Move_ValidTurn_BlocksSwaped()
        {
            Level level = new(2, 1);
            Block blockA = level.gameBoard.Cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = level.gameBoard.Cells[1, 0].SpawnBlock(new RedBlockType());
            MoveSystem moveSystem = new MoveSystem();
            moveSystem.SetLevel(level);

            IAction action = moveSystem.Move(new Vector2Int(0, 0), Directions.Right);
            if (action != null) 
                action.Execute();

            Assert.AreSame(blockB, level.gameBoard.Cells[0,0].Block);
            Assert.AreSame(blockA, level.gameBoard.Cells[1,0].Block);
        }

        [Test]
        public void Move_InValidStartPos_NoChange()
        {
            Level level = new(2,1);
            Block blockA = level.gameBoard.Cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = level.gameBoard.Cells[1, 0].SpawnBlock(new RedBlockType());
            MoveSystem moveSystem = new MoveSystem();
            moveSystem.SetLevel(level);

            IAction action = moveSystem.Move(new Vector2Int(100, 100), Directions.Right);
            if (action != null)
                action.Execute();

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            Assert.AreSame(blockA, level.gameBoard.Cells[0,0].Block);
            Assert.AreSame(blockB, level.gameBoard.Cells[1,0].Block);
        }

        [Test]
        public void Move_InValidTargetPos_NoChange()
        {
            Level level = new(2, 1);
            Block blockA = level.gameBoard.Cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = level.gameBoard.Cells[1, 0].SpawnBlock(new RedBlockType());
            MoveSystem moveSystem = new MoveSystem();
            moveSystem.SetLevel(level);

            IAction action = moveSystem.Move(new Vector2Int(0, 0), Directions.Up);
            if (action != null)
                action.Execute();

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            Assert.AreSame(blockA, level.gameBoard.Cells[0,0].Block);
            Assert.AreSame(blockB, level.gameBoard.Cells[1,0].Block);
        }
    }
}