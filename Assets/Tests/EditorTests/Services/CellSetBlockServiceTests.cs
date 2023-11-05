using UnityEngine;
using NUnit.Framework;
using Model.Objects;
using TestUtils;
using Model.Services;

namespace Model.Services.UnitTests
{
    public class CellSetBlockServiceTests
    {
        [Test]
        public void SetBlock_ValidBlock_CellHasBlock()
        {
            var initialBlockPosition = new Vector2Int(0, 0);
            var cellPosition = new Vector2Int(100, 100);
            Cell cell = TestCellFactory.CreateCell(TestCellFactory.BasicCellType, cellPosition);
            Block block = TestBlockFactory.CreateBlock(TestBlockFactory.DEFAULT_BLOCK, initialBlockPosition);
            var service = new CellSetBlockService();

            service.SetBlock(cell, block);

            Assert.AreEqual(block, cell.Block);
            Assert.AreEqual(cellPosition, block.Position);
        }

        [Test]
        public void SetBlock_NotPlayableCell_Nothing()
        {
            var initialBlockPosition = new Vector2Int(0, 0);
            var cellPosition = new Vector2Int(100, 100);
            Cell cell = TestCellFactory.CreateCell(TestCellFactory.NotPlayableCellType, cellPosition);
            Block block = TestBlockFactory.CreateBlock(TestBlockFactory.DEFAULT_BLOCK, initialBlockPosition);
            var service = new CellSetBlockService();

            service.SetBlock(cell, block);

            Assert.AreEqual(null, cell.Block);
            Assert.AreEqual(initialBlockPosition, block.Position);
        }

        [Test]
        public void SetBlock_Null_EmptyCell()
        {
            var cellPosition = new Vector2Int(100, 100);
            Cell cell = TestCellFactory.CreateCell(TestCellFactory.BasicCellType, cellPosition);
            Block block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, cell);
            var service = new CellSetBlockService();
            int eventCount = 0;
            service.OnEmpty += (_) => ++eventCount;

            service.SetBlock(cell, null);

            Assert.AreEqual(null, cell.Block);
            Assert.AreEqual(1, eventCount);
        }
    }
}