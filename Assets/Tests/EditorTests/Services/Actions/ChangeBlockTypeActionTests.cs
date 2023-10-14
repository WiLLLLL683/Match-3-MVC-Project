using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using TestUtils;

namespace Model.Services.Actions.UnitTests
{
    public class ChangeBlockTypeActionTests
    {
        [Test]
        public void ChangeType_BlueToRed_Red()
        {
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Block block = TestUtils.TestBlockFactory.CreateBlockInCell(TestUtils.TestBlockFactory.BLUE_BLOCK, cell);

            IAction action = new ChangeBlockTypeAction(TestUtils.TestBlockFactory.RedBlockType, block);
            action.Execute();

            Assert.AreEqual(TestUtils.TestBlockFactory.RED_BLOCK, block.Type.Id);
        }

        [Test]
        public void ChangeType_InvalidFinalType_NoChange()
        {
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Block block = TestUtils.TestBlockFactory.CreateBlockInCell(TestUtils.TestBlockFactory.BLUE_BLOCK, cell);

            IAction action = new ChangeBlockTypeAction(null, block);
            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
            Assert.AreEqual(TestUtils.TestBlockFactory.BLUE_BLOCK, block.Type.Id);
        }


        [Test]
        public void ChangeType_InvalidBlock_LogError()
        {
            IAction action = new ChangeBlockTypeAction(TestUtils.TestBlockFactory.RedBlockType, null);
            action.Execute();

            LogAssert.Expect(LogType.Error, "Invalid input data");
        }
    }
}