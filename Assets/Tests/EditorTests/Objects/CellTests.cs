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
            Cell cell = new Cell(true, new BasicCellType());
            Block block = new Block(new RedBlockType(),new Vector2Int(0,0));

            cell.SetBlock(block);

            Assert.AreEqual(block, cell.block);
        }

        [Test]
        public void SetBlock_Null_Nothing()
        {
            Cell cell = new Cell(true, new BasicCellType());
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));

            cell.SetBlock(block);
            cell.SetBlock(null);

            Assert.AreEqual(block, cell.block);
        }

        [Test]
        public void SetBlock_NotPlayableCell_Nothing()
        {
            Cell cell = new Cell(false, new BasicCellType());
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));

            cell.SetBlock(block);

            Assert.AreEqual(null, cell.block);
        }

        [Test]
        public void DestroyBlock_Block_CellEmpty()
        {
            Cell cell = new Cell(true, new BasicCellType());
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));

            cell.SetBlock(block);
            cell.DestroyBlock();

            Assert.AreEqual(true, cell.isEmpty);
        }

        [Test]
        public void DestroyBlock_Block_EmptyEvent()
        {
            Cell cell = new Cell(true, new BasicCellType());
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));
            bool test = false;
            void TestFunc(Cell cell, System.EventArgs eventArgs)
            {
                test = true;
            }

            cell.emptyEvent += TestFunc;
            cell.SetBlock(block);
            cell.DestroyBlock();
            cell.emptyEvent -= TestFunc;

            Assert.AreEqual(true, test);
        }

        [Test]
        public void DestroyBlock_Empty_Nothing()
        {
            Cell cell = new Cell(true, new BasicCellType());
            bool test = false;
            void TestFunc(Cell cell, System.EventArgs eventArgs)
            {
                test = true;
            }

            cell.emptyEvent += TestFunc;
            cell.DestroyBlock();
            cell.emptyEvent -= TestFunc;

            Assert.AreEqual(false, test);
        }

        [Test]
        public void DestroyBlock_NotPlayableCell_Nothing()
        {
            Cell cell = new Cell(false, new BasicCellType());
            bool test = false;
            void TestFunc(Cell cell, System.EventArgs eventArgs)
            {
                test = true;
            }

            cell.emptyEvent += TestFunc;
            cell.DestroyBlock();
            cell.emptyEvent -= TestFunc;

            Assert.AreEqual(false, test);
        }

    }
}