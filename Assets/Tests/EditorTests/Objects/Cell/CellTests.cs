using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using Config;
using Model.Readonly;
using UnitTests;

namespace Model.Objects.UnitTests
{
    public class CellTests
    {
        [Test]
        public void CellConstructor_CreateCell_CellIsEmpty()
        {
            var cell = new Cell(TestUtils.BasicCellType, new Vector2Int(0,0));

            Assert.AreEqual(true, cell.IsEmpty);
        }

        [Test]
        public void CellConstructor_CreatePlayableCell_CellIsPlayable()
        {
            Cell cell = new Cell(TestUtils.BasicCellType, new Vector2Int(0,0));

            Assert.AreEqual(true, cell.IsPlayable);
        }

        [Test]
        public void CellConstructor_CreateNotPlayableCell_CellIsNotPlayable()
        {
            Cell cell = new Cell(TestUtils.NotPlayableCellType, new Vector2Int(0,0));

            Assert.AreEqual(false, cell.IsPlayable);
        }

        [Test]
        public void SetBlock_Block_CellHasBlock()
        {
            Cell cell = new Cell(TestUtils.BasicCellType, new Vector2Int(0,0));
            Block block = new Block(TestUtils.DefaultBlockType, null);

            cell.SetBlock(block);

            Assert.AreEqual(block, cell.Block);
        }

        [Test]
        public void SetBlock_Null_EmptyCell()
        {
            Cell cell = new Cell(TestUtils.BasicCellType, new Vector2Int(0, 0));
            Block block = new Block(TestUtils.DefaultBlockType, cell);
            int eventCount = 0;
            cell.OnEmpty += (cell) => eventCount += 1;

            cell.SetBlock(block);
            cell.SetBlock(null);

            Assert.AreEqual(null, cell.Block);
            Assert.AreEqual(true, cell.IsEmpty);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void SetBlock_NotPlayableCell_Nothing()
        {
            Cell cell = new Cell(TestUtils.NotPlayableCellType, new Vector2Int(0, 0));
            Block block = new Block(TestUtils.DefaultBlockType, cell);

            cell.SetBlock(block);

            Assert.AreEqual(null, cell.Block);
        }

        [Test]
        public void DestroyBlock_Block_CellEmpty()
        {
            Cell cell = new Cell(TestUtils.BasicCellType, new Vector2Int(0, 0));
            Block block = new Block(TestUtils.DefaultBlockType, null);

            cell.SetBlock(block);
            cell.DestroyBlock();

            Assert.AreEqual(true, cell.IsEmpty);
        }

        [Test]
        public void DestroyBlock_Block_EmptyEvent()
        {
            Cell cell = new Cell(TestUtils.BasicCellType, new Vector2Int(0, 0));
            Block block = new Block(TestUtils.DefaultBlockType, null);
            bool test = false;
            void TestFunc(ICell_Readonly cell)
            {
                test = true;
            }

            cell.OnEmpty += TestFunc;
            cell.SetBlock(block);
            cell.DestroyBlock();
            cell.OnEmpty -= TestFunc;

            Assert.AreEqual(true, test);
        }

        [Test]
        public void DestroyBlock_Empty_Nothing()
        {
            Cell cell = new Cell(TestUtils.BasicCellType, new Vector2Int(0, 0));
            bool test = false;
            void TestFunc(ICell_Readonly cell)
            {
                test = true;
            }

            cell.OnEmpty += TestFunc;
            cell.DestroyBlock();
            cell.OnEmpty -= TestFunc;

            Assert.AreEqual(false, test);
        }

        [Test]
        public void DestroyBlock_NotPlayableCell_Nothing()
        {
            Cell cell = new Cell(TestUtils.NotPlayableCellType, new Vector2Int(0, 0));
            bool test = false;
            void TestFunc(ICell_Readonly cell)
            {
                test = true;
            }

            cell.OnEmpty += TestFunc;
            cell.DestroyBlock();
            cell.OnEmpty -= TestFunc;

            Assert.AreEqual(false, test);
        }

        [Test]
        public void DestroyCell_CorrectCell_OnCellDestroyEvent()
        {
            Cell cell = new Cell(TestUtils.BasicCellType, new Vector2Int(0, 0));
            int eventRised = 0;
            cell.OnDestroy += (_) => eventRised += 1;

            cell.DestroyCell();

            Assert.AreEqual(1, eventRised);
        }

        [Test]
        public void DestroyCell_NoCellType_Nothing()
        {
            Cell cell = new Cell(null, new Vector2Int(0, 0));
            int eventRised = 0;
            cell.OnDestroy += (ICell_Readonly cell) => eventRised += 1;

            cell.DestroyCell();

            Assert.AreEqual(0, eventRised);
        }
    }
}