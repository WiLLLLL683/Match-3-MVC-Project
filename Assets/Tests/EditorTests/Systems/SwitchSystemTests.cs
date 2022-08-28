using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Systems.Tests
{
    public class SwitchSystemTests
    {
        [Test]
        public void Switch_ValidTurn_BlocksSwaped()
        {
            GameBoard gameBoard = new GameBoard(2,1);
            Block blockA = gameBoard.cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = gameBoard.cells[1, 0].SpawnBlock(new RedBlockType());
            SwitchSystem switchSystem = new SwitchSystem(gameBoard);

            IAction action = switchSystem.Switch(new Vector2Int(0, 0), Directions.Right);
            if (action != null) 
                action.Execute();

            Assert.AreSame(blockB, gameBoard.cells[0,0].block);
            Assert.AreSame(blockA, gameBoard.cells[1,0].block);
        }

        [Test]
        public void Switch_InValidStartPos_NoChange()
        {
            GameBoard gameBoard = new GameBoard(2,1);
            Block blockA = gameBoard.cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = gameBoard.cells[1, 0].SpawnBlock(new RedBlockType());
            SwitchSystem switchSystem = new SwitchSystem(gameBoard);

            IAction action = switchSystem.Switch(new Vector2Int(100, 100), Directions.Right);
            if (action != null)
                action.Execute();

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            Assert.AreSame(blockA, gameBoard.cells[0,0].block);
            Assert.AreSame(blockB, gameBoard.cells[1,0].block);
        }

        [Test]
        public void Switch_InValidTargetPos_NoChange()
        {
            GameBoard gameBoard = new GameBoard(2,1);
            Block blockA = gameBoard.cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = gameBoard.cells[1, 0].SpawnBlock(new RedBlockType());
            SwitchSystem switchSystem = new SwitchSystem(gameBoard);

            IAction action = switchSystem.Switch(new Vector2Int(0, 0), Directions.Up);
            if (action != null)
                action.Execute();

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            Assert.AreSame(blockA, gameBoard.cells[0,0].block);
            Assert.AreSame(blockB, gameBoard.cells[1,0].block);
        }
    }
}