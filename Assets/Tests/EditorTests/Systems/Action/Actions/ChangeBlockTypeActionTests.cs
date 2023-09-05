using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using Config;

namespace Model.Systems.UnitTests
{
    public class ChangeBlockTypeActionTests
    {
        [Test]
        public void ChangeType_BlueToRed_Red()
        {
            Block block = new Cell(new BasicCellType(), new Vector2Int(0,0)).SpawnBlock(new BasicBlockType());
            IAction action = new ChangeBlockTypeAction(new BasicBlockType(), block);

            action.Execute();

            Assert.AreEqual(typeof(BasicBlockType), block.Type.GetType());
        }

        [Test]
        public void ChangeType_InvalidFinalType_NoChange()
        {
            Block block = new Cell(new BasicCellType(), new Vector2Int(0, 0)).SpawnBlock(new BasicBlockType());
            IAction action = new ChangeBlockTypeAction(null, block);

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
            Assert.AreEqual(typeof(BasicBlockType), block.Type.GetType());
        }


        [Test]
        public void ChangeType_InvalidLevel_LogError()
        {
            IAction action = new ChangeBlockTypeAction(new BasicBlockType(), null);

            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
        }
    }
}