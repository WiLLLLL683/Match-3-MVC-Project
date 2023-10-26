using Model.Objects;
using Model.Services;
using NUnit.Framework;
using TestUtils;

namespace Model.Infrastructure.Commands.UnitTests
{
    public class CellChangeTypeCommandTests
    {
        private int eventCount = 0;

        private (CellChangeTypeService service, CellType initialType, CellType targetType) Setup()
        {
            var service = new CellChangeTypeService();
            eventCount = 0;
            service.OnTypeChange += (_) => eventCount++;
            var initialType = TestCellFactory.BasicCellType;
            var targetType = TestCellFactory.NotPlayableCellType;

            return (service, initialType, targetType);
        }

        [Test]
        public void Execute_ValidInput_TypeChanged()
        {
            var (service, initialType, targetType) = Setup();
            var cell = TestCellFactory.CreateCell(initialType);
            var command = new CellChangeTypeCommand(cell, targetType, service);

            command.Execute();

            Assert.AreEqual(targetType, cell.Type);
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void Undo_ValidInput_TypeChanged()
        {
            var (service, initialType, targetType) = Setup();
            var cell = TestCellFactory.CreateCell(initialType);
            var command = new CellChangeTypeCommand(cell, targetType, service);

            command.Execute();

            Assert.AreEqual(targetType, cell.Type);
            Assert.AreEqual(1, eventCount);

            command.Undo();

            Assert.AreEqual(initialType, cell.Type);
            Assert.AreEqual(2, eventCount);
        }

        [Test]
        public void Execute_NullCell_NoChange()
        {
            var (service, initialType, targetType) = Setup();
            var command = new CellChangeTypeCommand(null, targetType, service);

            command.Execute();

            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Undo_NullCell_NoChange()
        {
            var (service, initialType, targetType) = Setup();
            var command = new CellChangeTypeCommand(null, targetType, service);

            command.Execute();

            Assert.AreEqual(0, eventCount);

            command.Undo();

            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Execute_NullTargetType_NoChange()
        {
            var (service, initialType, targetType) = Setup();
            var cell = TestCellFactory.CreateCell(initialType);
            var command = new CellChangeTypeCommand(cell, null, service);

            command.Execute();

            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Undo_NullTargetType_NoChange()
        {
            var (service, initialType, targetType) = Setup();
            var cell = TestCellFactory.CreateCell(initialType);
            var command = new CellChangeTypeCommand(cell, null, service);

            command.Execute();

            Assert.AreEqual(0, eventCount);

            command.Undo();

            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Execute_NullService_NoChange()
        {
            var (service, initialType, targetType) = Setup();
            var cell = TestCellFactory.CreateCell(initialType);
            var command = new CellChangeTypeCommand(cell, targetType, null);

            command.Execute();

            Assert.AreEqual(0, eventCount);
        }

        [Test]
        public void Undo_NullService_NoChange()
        {
            var (service, initialType, targetType) = Setup();
            var cell = TestCellFactory.CreateCell(initialType);
            var command = new CellChangeTypeCommand(cell, targetType, null);

            command.Execute();

            Assert.AreEqual(0, eventCount);

            command.Undo();

            Assert.AreEqual(0, eventCount);
        }
    }
}