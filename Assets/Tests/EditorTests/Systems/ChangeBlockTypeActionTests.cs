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
            Cell cell = gameBoard.cells[0, 0];
            cell.SpawnBlock(new BlueBlockType());
            IAction action = new ChangeBlockTypeAction(gameBoard, new RedBlockType(), cell);

            action.Execute();

            Assert.AreEqual(typeof(RedBlockType), gameBoard.cells[0, 0].block.type.GetType());
        }

        [Test]
        public void ChangeType_InvalidFinalType_NoChange()
        {
            GameBoard gameBoard = new GameBoard(1, 1);
            Cell cell = gameBoard.cells[0, 0];
            cell.SpawnBlock(new BlueBlockType());
            IAction action = new ChangeBlockTypeAction(gameBoard, null, cell);

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
            Assert.AreEqual(typeof(BlueBlockType), gameBoard.cells[0, 0].block.type.GetType());
        }

        [Test]
        public void ChangeType_InvalidPosition_LogError()
        {
            GameBoard gameBoard = new GameBoard(1, 1);
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(50, 50));
            cell.SpawnBlock(new BlueBlockType());
            IAction action = new ChangeBlockTypeAction(gameBoard, new RedBlockType(), cell);

            action.Execute();

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
        }

        [Test]
        public void ChangeType_InvalidLevel_LogError()
        {
            IAction action = new ChangeBlockTypeAction(null, new RedBlockType(), null);

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
        }
    }
}