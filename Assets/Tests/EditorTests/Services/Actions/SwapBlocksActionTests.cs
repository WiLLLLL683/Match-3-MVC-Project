using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using TestUtils;

namespace Model.Services.Actions.UnitTests
{
    public class SwapBlocksActionTests
    {
        [Test]
        public void SwapBlocks_ValidBlocks_ValidSwap()
        {
            Cell cellA = new Cell(new BasicCellType(), new Vector2Int(0,0));
            Cell cellB = new Cell(new BasicCellType(), new Vector2Int(0,1));
            Block blockA = TestUtils.TestBlockFactory.CreateBlockInCell(TestUtils.TestBlockFactory.BLUE_BLOCK, cellA);
            Block blockB = TestUtils.TestBlockFactory.CreateBlockInCell(TestUtils.TestBlockFactory.RED_BLOCK, cellB);

            IAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(blockB.Type.Id, cellA.Block.Type.Id);
            Assert.AreEqual(blockA.Type.Id, cellB.Block.Type.Id);
        }

        [Test]
        public void SwapBlocks_EmptyBlockValidBlock_ValidSwap_OneEmptyEvent()
        {
            Cell cellA = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Cell cellB = new Cell(new BasicCellType(), new Vector2Int(0, 1));
            Block blockB = TestUtils.TestBlockFactory.CreateBlockInCell(TestUtils.TestBlockFactory.RED_BLOCK, cellB);
            int eventCount = 0;
            cellA.OnEmpty += (_) => ++eventCount;
            cellB.OnEmpty += (_) => ++eventCount;

            IAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(blockB.Type.Id, cellA.Block.Type.Id);
            Assert.AreEqual(null, cellB.Block);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void SwapBlocks_EmptyBlockEmptyBlock_ValidSwap_TwoEmptyEvents()
        {
            Cell cellA = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Cell cellB = new Cell(new BasicCellType(), new Vector2Int(0, 1));
            int eventCount = 0;
            cellA.OnEmpty += (_) => ++eventCount;
            cellB.OnEmpty += (_) => ++eventCount;

            IAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(null, cellA.Block);
            Assert.AreEqual(null, cellB.Block);
            Assert.AreEqual(2, eventCount);
        }

        [Test]
        public void SwapBlocks_NullCellValidCell_NoSwap()
        {
            Cell cellA = null;
            Cell cellB = new Cell(new BasicCellType(), new Vector2Int(0, 1));
            Block blockB = TestUtils.TestBlockFactory.CreateBlockInCell(TestUtils.TestBlockFactory.RED_BLOCK, cellB);

            IAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(null, cellA);
            Assert.AreEqual(blockB.Type.Id, cellB.Block.Type.Id);
        }
    }
}
