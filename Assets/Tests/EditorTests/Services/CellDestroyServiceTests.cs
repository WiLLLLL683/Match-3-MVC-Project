using Model.Objects;
using NUnit.Framework;
using TestUtils;
using UnityEngine;

namespace Model.Services.UnitTests
{
    public class CellDestroyServiceTests
    {
        [Test]
        public void Destroy_ValidCell_DestroyEvent()
        {
            var service = new CellDestroyService();
            var cell = new Cell(TestCellFactory.BasicCellType, new Vector2Int(0, 0));
            int eventRised = 0;
            service.OnDestroy += (_) => ++eventRised;

            service.Destroy(cell);

            Assert.AreEqual(1, eventRised);
        }

        [Test]
        public void Destroy_NullCellType_NoChange()
        {
            var service = new CellDestroyService();
            var cell = new Cell(null, new Vector2Int(0, 0));
            int eventRised = 0;
            service.OnDestroy += (_) => ++eventRised;

            service.Destroy(cell);

            Assert.AreEqual(0, eventRised);
        }

        [Test]
        public void Destroy_NullCell_NoChange()
        {
            var service = new CellDestroyService();
            int eventRised = 0;
            service.OnDestroy += (_) => ++eventRised;

            service.Destroy(null);

            Assert.AreEqual(0, eventRised);
        }
    }
}