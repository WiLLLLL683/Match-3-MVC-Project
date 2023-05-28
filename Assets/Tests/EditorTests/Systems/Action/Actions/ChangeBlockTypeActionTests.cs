using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using Data;

namespace Model.Systems.UnitTests
{
    public class ChangeBlockTypeActionTests
    {
        [Test]
        public void ChangeType_BlueToRed_Red()
        {
            Block block = new Cell(new BasicCellType(), new Vector2Int(0,0)).SpawnBlock(new BlueBlockType());
            IAction action = new ChangeBlockTypeAction(new RedBlockType(), block);

            action.Execute();

            Assert.AreEqual(typeof(RedBlockType), block.Type.GetType());
        }

        [Test]
        public void ChangeType_InvalidFinalType_NoChange()
        {
            Block block = new Cell(new BasicCellType(), new Vector2Int(0, 0)).SpawnBlock(new BlueBlockType());
            IAction action = new ChangeBlockTypeAction(null, block);

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
            Assert.AreEqual(typeof(BlueBlockType), block.Type.GetType());
        }


        [Test]
        public void ChangeType_InvalidLevel_LogError()
        {
            IAction action = new ChangeBlockTypeAction(new RedBlockType(), null);

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
        }
    }
}