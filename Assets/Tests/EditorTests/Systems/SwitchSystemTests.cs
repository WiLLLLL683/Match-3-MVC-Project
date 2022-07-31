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
            Block blockA = new Block(new BlueBlockType(), new Vector2Int(0, 0));
            Block blockB = new Block(new RedBlockType(), new Vector2Int(1, 0));
            gameBoard.cells[0, 0].SetBlock(blockA);
            gameBoard.cells[1, 0].SetBlock(blockB);
            SwitchSystem switchSystem = new SwitchSystem(gameBoard);

            switchSystem.Switch(new Vector2Int(0, 0), Directions.Right);

            Assert.AreSame(blockB, gameBoard.cells[0,0].block);
            Assert.AreSame(blockA, gameBoard.cells[1,0].block);
        }

        [Test]
        public void Switch_InValidStartPos_NoChange()
        {
            GameBoard gameBoard = new GameBoard(2,1);
            Block blockA = new Block(new BlueBlockType(), new Vector2Int(0, 0));
            Block blockB = new Block(new RedBlockType(), new Vector2Int(1, 0));
            gameBoard.cells[0, 0].SetBlock(blockA);
            gameBoard.cells[1, 0].SetBlock(blockB);
            SwitchSystem switchSystem = new SwitchSystem(gameBoard);

            switchSystem.Switch(new Vector2Int(100, 100), Directions.Right);

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            Assert.AreSame(blockA, gameBoard.cells[0,0].block);
            Assert.AreSame(blockB, gameBoard.cells[1,0].block);
        }

        [Test]
        public void Switch_InValidTargetPos_NoChange()
        {
            GameBoard gameBoard = new GameBoard(2,1);
            Block blockA = new Block(new BlueBlockType(), new Vector2Int(0, 0));
            Block blockB = new Block(new RedBlockType(), new Vector2Int(1, 0));
            gameBoard.cells[0, 0].SetBlock(blockA);
            gameBoard.cells[1, 0].SetBlock(blockB);
            SwitchSystem switchSystem = new SwitchSystem(gameBoard);

            switchSystem.Switch(new Vector2Int(0, 0), Directions.Up);

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            Assert.AreSame(blockA, gameBoard.cells[0,0].block);
            Assert.AreSame(blockB, gameBoard.cells[1,0].block);
        }

        [Test]
        public void Switch_TurnHaveNoResult_NoChange()
        {
            //GameBoard gameBoard = new GameBoard(2,1);
            //Block blockA = new Block(new BlueBlockType(), new Vector2Int(0, 0));
            //Block blockB = new Block(new RedBlockType(), new Vector2Int(1, 0));
            //gameBoard.cells[0, 0].SetBlock(blockA);
            //gameBoard.cells[1, 0].SetBlock(blockB);
            //SwitchSystem switchSystem = new SwitchSystem(gameBoard);

            //switchSystem.Switch(new Vector2Int(0, 0), Directions.Up);

            //LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            //Assert.AreSame(blockA, gameBoard.cells[0,0].block);
            //Assert.AreSame(blockB, gameBoard.cells[1,0].block);.

            //TODO тесты на результативность хода
            Assert.True(false);
        }
    }
}