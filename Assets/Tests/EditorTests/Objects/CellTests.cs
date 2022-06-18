using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;

namespace Model.Objects.Tests
{
    public class CellTests
    {
        [Test]
        public void SetBlock_Block_CellHasBlock()
        {
            Cell cell = new Cell(new BasicCellType());
            Block block = new Block(new RedBlockType(),new Vector2Int(0,0));

            cell.SetBlock(block);

            Assert.AreEqual(block, cell.block);
        }

        [Test]
        public void SetBlock_Null_Nothing()
        {
            Cell cell = new Cell(new BasicCellType());
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));

            cell.SetBlock(block);
            cell.SetBlock(null);

            Assert.AreEqual(block, cell.block);
        }

        [Test]
        public void SetBlock_NotPlayableCell_Nothing()
        {
            Cell cell = new Cell(new NotPlayableCellType());
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));

            cell.SetBlock(block);

            Assert.AreEqual(null, cell.block);
        }

        [Test]
        public void DestroyBlock_Block_CellEmpty()
        {
            Cell cell = new Cell(new BasicCellType());
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));

            cell.SetBlock(block);
            cell.DestroyBlock();

            Assert.AreEqual(true, cell.isEmpty);
        }

        [Test]
        public void DestroyBlock_Block_EmptyEvent()
        {
            Cell cell = new Cell(new BasicCellType());
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));
            bool test = false;
            void TestFunc(Cell cell, System.EventArgs eventArgs)
            {
                test = true;
            }

            cell.OnEmptyEvent += TestFunc;
            cell.SetBlock(block);
            cell.DestroyBlock();
            cell.OnEmptyEvent -= TestFunc;

            Assert.AreEqual(true, test);
        }

        [Test]
        public void DestroyBlock_Empty_Nothing()
        {
            Cell cell = new Cell(new BasicCellType());
            bool test = false;
            void TestFunc(Cell cell, System.EventArgs eventArgs)
            {
                test = true;
            }

            cell.OnEmptyEvent += TestFunc;
            cell.DestroyBlock();
            cell.OnEmptyEvent -= TestFunc;

            Assert.AreEqual(false, test);
        }

        [Test]
        public void DestroyBlock_NotPlayableCell_Nothing()
        {
            Cell cell = new Cell(new BasicCellType());
            bool test = false;
            void TestFunc(Cell cell, System.EventArgs eventArgs)
            {
                test = true;
            }

            cell.OnEmptyEvent += TestFunc;
            cell.DestroyBlock();
            cell.OnEmptyEvent -= TestFunc;

            Assert.AreEqual(false, test);
        }

        [Test]
        public void DestroyCell_CorrectCell_OnCellDestroyEvent()
        {
            Cell cell = new Cell(new BasicCellType());
            int eventRised = 0;
            cell.OnCellDestroyEvent += (Cell cell, System.EventArgs eventArgs) => eventRised += 1;

            cell.DestroyCell();

            Assert.AreEqual(1, eventRised);
        }

        [Test]
        public void DestroyCell_NoCellType_Nothing()
        {
            Cell cell = new Cell(null);
            int eventRised = 0;
            cell.OnCellDestroyEvent += (Cell cell, System.EventArgs eventArgs) => eventRised += 1;

            cell.DestroyCell();

            Assert.AreEqual(0, eventRised);
        }
    }
}