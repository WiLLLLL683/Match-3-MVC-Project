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
        public void CellConstructor_CreateCell_CellIsEmpty()
        {
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(0,0));

            Assert.AreEqual(true, cell.isEmpty);
        }

        [Test]
        public void CellConstructor_CreatePlayableCell_CellIsPlayable()
        {
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(0,0));

            Assert.AreEqual(true, cell.IsPlayable);
        }

        [Test]
        public void CellConstructor_CreateNotPlayableCell_CellIsNotPlayable()
        {
            Cell cell = new Cell(new NotPlayableCellType(), new Vector2Int(0,0));

            Assert.AreEqual(false, cell.IsPlayable);
        }

        [Test]
        public void SetBlock_Block_CellHasBlock()
        {
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(0,0));
            Block block = new Block(new BasicBlockType(),null);

            cell.SetBlock(block);

            Assert.AreEqual(block, cell.block);
        }

        [Test]
        public void SetBlock_Null_EmptyCell()
        {
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Block block = new Block(new BasicBlockType(), cell);
            int eventCount = 0;
            cell.OnEmpty += (cell, eventArgs) => eventCount += 1;

            cell.SetBlock(block);
            cell.SetBlock(null);

            Assert.AreEqual(null, cell.block);
            Assert.AreEqual(true, cell.isEmpty);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void SetBlock_NotPlayableCell_Nothing()
        {
            Cell cell = new Cell(new NotPlayableCellType(), new Vector2Int(0, 0));
            Block block = new Block(new BasicBlockType(), cell);

            cell.SetBlock(block);

            Assert.AreEqual(null, cell.block);
        }

        [Test]
        public void DestroyBlock_Block_CellEmpty()
        {
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Block block = new Block(new BasicBlockType(), null);

            cell.SetBlock(block);
            cell.DestroyBlock();

            Assert.AreEqual(true, cell.isEmpty);
        }

        [Test]
        public void DestroyBlock_Block_EmptyEvent()
        {
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Block block = new Block(new BasicBlockType(), null);
            bool test = false;
            void TestFunc(Cell cell, System.EventArgs eventArgs)
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
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            bool test = false;
            void TestFunc(Cell cell, System.EventArgs eventArgs)
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
            Cell cell = new Cell(new NotPlayableCellType(), new Vector2Int(0, 0));
            bool test = false;
            void TestFunc(Cell cell, System.EventArgs eventArgs)
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
            Cell cell = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            int eventRised = 0;
            cell.OnDestroy += (Cell cell, System.EventArgs eventArgs) => eventRised += 1;

            cell.DestroyCell();

            Assert.AreEqual(1, eventRised);
        }

        [Test]
        public void DestroyCell_NoCellType_Nothing()
        {
            Cell cell = new Cell(null, new Vector2Int(0, 0));
            int eventRised = 0;
            cell.OnDestroy += (Cell cell, System.EventArgs eventArgs) => eventRised += 1;

            cell.DestroyCell();

            Assert.AreEqual(0, eventRised);
        }
    }
}