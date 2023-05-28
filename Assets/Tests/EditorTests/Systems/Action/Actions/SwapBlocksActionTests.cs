using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using Data;

namespace Model.Systems.UnitTests
{
    public class SwapBlocksActionTests
    {
        [Test]
        public void SwapBlocks_ValidBlocks_ValidSwap()
        {
            Cell cellA = new Cell(new BasicCellType(), new Vector2Int(0,0));
            Cell cellB = new Cell(new BasicCellType(), new Vector2Int(0,1));
            Block blockA = cellA.SpawnBlock(new BlueBlockType());
            Block blockB = cellB.SpawnBlock(new RedBlockType());

            SwapBlocksAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(blockB, cellA.Block);
            Assert.AreEqual(blockA, cellB.Block);
        }

        [Test]
        public void SwapBlocks_EmptyBlockValidBlock_ValidSwap_OneEmptyEvent()
        {
            Cell cellA = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Cell cellB = new Cell(new BasicCellType(), new Vector2Int(0, 1));
            Block blockB = cellB.SpawnBlock(new RedBlockType());
            int eventCount = 0;
            cellA.OnEmpty += (cell, eventArgs) => eventCount += 1;
            cellB.OnEmpty += (cell, eventArgs) => eventCount += 1;

            SwapBlocksAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(blockB, cellA.Block);
            Assert.AreEqual(null, cellB.Block);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void SwapBlocks_EmptyBlockEmptyBlock_ValidSwap_TwoEmptyEvents()
        {
            Cell cellA = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Cell cellB = new Cell(new BasicCellType(), new Vector2Int(0, 1));
            int eventCount = 0;
            cellA.OnEmpty += (cell, eventArgs) => eventCount += 1;
            cellB.OnEmpty += (cell, eventArgs) => eventCount += 1;

            SwapBlocksAction action = new SwapBlocksAction(cellA, cellB);
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
            Block blockB = cellB.SpawnBlock(new RedBlockType());

            SwapBlocksAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(null, cellA);
            Assert.AreEqual(blockB, cellB.Block);
        }
    }
}
