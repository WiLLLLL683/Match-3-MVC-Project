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
            Level level = new Level(1,1);
            level.gameBoard.cells[0, 0].SetBlock(new Block(new BlueBlockType(),new Vector2Int(0,0)));
            IAction action = new ChangeBlockTypeAction(level, new RedBlockType(), new Vector2Int(0,0));

            action.Execute();

            Assert.AreEqual(typeof(RedBlockType), level.gameBoard.cells[0, 0].block.type.GetType());
        }

        [Test]
        public void ChangeType_InvalidFinalType_NoChange()
        {
            Level level = new Level(1,1);
            level.gameBoard.cells[0, 0].SetBlock(new Block(new BlueBlockType(), new Vector2Int(0, 0)));
            IAction action = new ChangeBlockTypeAction(level, null, new Vector2Int(0,0));

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
            Assert.AreEqual(typeof(BlueBlockType), level.gameBoard.cells[0, 0].block.type.GetType());
        }

        [Test]
        public void ChangeType_InvalidPosition_LogError()
        {
            Level level = new Level(1,1);
            level.gameBoard.cells[0, 0].SetBlock(new Block(new BlueBlockType(), new Vector2Int(0, 0)));
            IAction action = new ChangeBlockTypeAction(level, new RedBlockType(), new Vector2Int(50,50));

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid position");
        }

        [Test]
        public void ChangeType_InvalidLevel_LogError()
        {
            Level level = null;
            IAction action = new ChangeBlockTypeAction(level, new RedBlockType(), new Vector2Int(50,50));

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
        }
    }
}