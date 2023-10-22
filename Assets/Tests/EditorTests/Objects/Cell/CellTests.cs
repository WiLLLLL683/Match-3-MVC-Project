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