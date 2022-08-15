using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;

namespace Model.Systems.Tests
{
    public class ChangeBlockTypeActionTests
    {
        [Test]
        public void ChangeType_BlueToRed_Red()
        {
            GameBoard gameBoard = new GameBoard(1,1);
            gameBoard.cells[0, 0].SetBlock(new Block(new BlueBlockType(),new Vector2Int(0,0)));
            IAction action = new ChangeBlockTypeAction(gameBoard, new RedBlockType(), new Vector2Int(0,0));

            action.Execute();

            Assert.AreEqual(typeof(RedBlockType), gameBoard.cells[0, 0].block.type.GetType());
        }

        [Test]
        public void ChangeType_InvalidFinalType_NoChange()
        {
            GameBoard gameBoard = new GameBoard(1, 1);
            gameBoard.cells[0, 0].SetBlock(new Block(new BlueBlockType(), new Vector2Int(0, 0)));
            IAction action = new ChangeBlockTypeAction(gameBoard, null, new Vector2Int(0,0));

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
            Assert.AreEqual(typeof(BlueBlockType), gameBoard.cells[0, 0].block.type.GetType());
        }

        [Test]
        public void ChangeType_InvalidPosition_LogError()
        {
            GameBoard gameBoard = new GameBoard(1, 1);
            gameBoard.cells[0, 0].SetBlock(new Block(new BlueBlockType(), new Vector2Int(0, 0)));
            IAction action = new ChangeBlockTypeAction(gameBoard, new RedBlockType(), new Vector2Int(50,50));

            action.Execute();

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
        }

        [Test]
        public void ChangeType_InvalidLevel_LogError()
        {
            GameBoard gameBoard = null;
            IAction action = new ChangeBlockTypeAction(gameBoard, new RedBlockType(), new Vector2Int(50,50));

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
        }
    }
}