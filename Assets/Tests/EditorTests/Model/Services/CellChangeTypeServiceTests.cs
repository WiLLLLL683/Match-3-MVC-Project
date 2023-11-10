using UnityEngine;
using NUnit.Framework;
using Model.Objects;
using TestUtils;

namespace Model.Services.UnitTests
{
    public class CellChangeTypeServiceTests
    {
        [Test]
        public void ChangeType_ValidType_TypeChanged()
        {
            CellType oldType = TestCellFactory.BasicCellType;
            CellType newType = TestCellFactory.NotPlayableCellType;
            Cell cell = TestCellFactory.CreateCell(oldType);
            var service = new CellChangeTypeService();
            int eventCount = 0;
            service.OnTypeChange += (_) => ++eventCount;

            service.ChangeType(cell, newType);

            Assert.AreEqual(newType, cell.Type);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void ChangeType_NullType_NoChange()
        {
            CellType oldType = TestCellFactory.BasicCellType;
            CellType newType = null;
            Cell cell = TestCellFactory.CreateCell(oldType);
            var service = new CellChangeTypeService();
            int eventCount = 0;
            service.OnTypeChange += (_) => ++eventCount;

            service.ChangeType(cell, newType);

            Assert.AreEqual(oldType, cell.Type);
            Assert.AreEqual(0, eventCount);
        }
    }
}