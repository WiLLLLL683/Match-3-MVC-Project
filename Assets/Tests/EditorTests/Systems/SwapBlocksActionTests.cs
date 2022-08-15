using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;

namespace Model.Systems.Tests
{
    public class SwapBlocksActionTests
    {
        [Test]
        public void SwapBlocks_ValidBlocks_ValidSwap()
        {
            Block blockA = new Block(new BlueBlockType(),new Vector2Int(0, 0));
            Block blockB = new Block(new RedBlockType(), new Vector2Int(0, 1));
            Cell cellA = new Cell(new BasicCellType());
            Cell cellB = new Cell(new BasicCellType());
            cellA.SetBlock(blockA);
            cellB.SetBlock(blockB);

            SwapBlocksAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(blockB, cellA.block);
            Assert.AreEqual(blockA, cellB.block);
        }

        [Test]
        public void SwapBlocks_EmptyBlockValidBlock_ValidSwap_OneEmptyEvent()
        {
            Block blockA = null;
            Block blockB = new Block(new RedBlockType(), new Vector2Int(0, 1));
            Cell cellA = new Cell(new BasicCellType());
            Cell cellB = new Cell(new BasicCellType());
            cellA.SetBlock(blockA);
            cellB.SetBlock(blockB);
            int eventCount = 0;
            cellA.OnEmptyEvent += (cell, eventArgs) => eventCount += 1;
            cellB.OnEmptyEvent += (cell, eventArgs) => eventCount += 1;

            SwapBlocksAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(blockB, cellA.block);
            Assert.AreEqual(null, cellB.block);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void SwapBlocks_EmptyBlockEmptyBlock_ValidSwap_TwoEmptyEvents()
        {
            Block blockA = null;
            Block blockB = null;
            Cell cellA = new Cell(new BasicCellType());
            Cell cellB = new Cell(new BasicCellType());
            cellA.SetBlock(blockA);
            cellB.SetBlock(blockB);
            int eventCount = 0;
            cellA.OnEmptyEvent += (cell, eventArgs) => eventCount += 1;
            cellB.OnEmptyEvent += (cell, eventArgs) => eventCount += 1;

            SwapBlocksAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(null, cellA.block);
            Assert.AreEqual(null, cellB.block);
            Assert.AreEqual(2, eventCount);
        }

        [Test]
        public void SwapBlocks_NullCellValidCell_NoSwap()
        {
            Block blockA = new Block(new BlueBlockType(), new Vector2Int(0, 0));
            Block blockB = new Block(new RedBlockType(), new Vector2Int(0, 1));
            Cell cellA = null;
            Cell cellB = new Cell(new BasicCellType());
            cellB.SetBlock(blockB);

            SwapBlocksAction action = new SwapBlocksAction(cellA, cellB);
            action.Execute();

            Assert.AreEqual(null, cellA);
            Assert.AreEqual(blockB, cellB.block);
        }
    }
}
