using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using Config;
using Model.Readonly;
using TestUtils;

namespace Model.Objects.UnitTests
{
    public class CellTests
    {
        [Test]
        public void ChangeType_ValidType_TypeChanged()
        {
            CellType oldType = TestCellFactory.BasicCellType;
            CellType newType = TestCellFactory.NotPlayableCellType;
            Cell cell = new Cell(oldType, new Vector2Int(0, 0));
            int eventCount = 0;
            cell.OnTypeChange += (_) => ++eventCount;

            cell.SetType(newType);

            Assert.AreEqual(newType, cell.Type);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void ChangeType_NullType_NoChange()
        {
            CellType oldType = TestCellFactory.BasicCellType;
            CellType newType = null;
            Cell cell = new Cell(oldType, new Vector2Int(0, 0));
            int eventCount = 0;
            cell.OnTypeChange += (_) => ++eventCount;

            cell.SetType(newType);

            Assert.AreEqual(oldType, cell.Type);
            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void SetBlock_ValidBlock_CellHasBlock()
        {
            Cell cell = new Cell(TestCellFactory.BasicCellType, new Vector2Int(0,0));
            Block block = new Block(TestBlockFactory.DefaultBlockType, default);

            cell.SetBlock(block);

            Assert.AreEqual(block, cell.Block);
        }

        [Test]
        public void SetBlock_NotPlayableCell_Nothing()
        {
            Cell cell = new Cell(TestCellFactory.NotPlayableCellType, new Vector2Int(0, 0));
            Block block = new Block(TestBlockFactory.DefaultBlockType, cell.Position);

            cell.SetBlock(block);

            Assert.AreEqual(null, cell.Block);
        }

        [Test]
        public void SetBlock_Null_EmptyCell()
        {
            Cell cell = new Cell(TestCellFactory.BasicCellType, new Vector2Int(0, 0));
            Block block = new Block(TestBlockFactory.DefaultBlockType, cell.Position);
            int eventCount = 0;
            cell.OnEmpty += (_) => ++eventCount;
            cell.SetBlock(block);

            cell.SetBlock(null);

            Assert.AreEqual(null, cell.Block);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void DestroyCell_CorrectCell_OnCellDestroyEvent()
        {
            Cell cell = new Cell(TestCellFactory.BasicCellType, new Vector2Int(0, 0));
            int eventRised = 0;
            cell.OnDestroy += (_) => ++eventRised;

            cell.Destroy();

            Assert.AreEqual(1, eventRised);
        }

        [Test]
        public void DestroyCell_NoCellType_Nothing()
        {
            Cell cell = new Cell(null, new Vector2Int(0, 0));
            int eventRised = 0;
            cell.OnDestroy += (_) => ++eventRised;

            cell.Destroy();

            Assert.AreEqual(0, eventRised);
        }
    }
}