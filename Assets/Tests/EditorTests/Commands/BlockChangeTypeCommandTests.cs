using Model.Objects;
using Model.Services;
using NUnit.Framework;
using TestUtils;

namespace Model.Commands.UnitTests
{
    public class BlockChangeTypeCommandTests
    {
        private (GameBoard gameBoard, BlockChangeTypeService service) Setup()
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(1, 1, 0);
            var validation = new ValidationService();
            var service = new BlockChangeTypeService(validation);
            validation.SetLevel(gameBoard);
            service.SetLevel(gameBoard);

            return (gameBoard, service);
        }

        [Test]
        public void Execute_BlueToRed_Red()
        {
            var (gameBoard, service) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.BLUE_BLOCK, gameBoard.Cells[0,0], gameBoard);

            ICommand action = new BlockChangeTypeCommand(block, TestBlockFactory.RedBlockType, service);
            action.Execute();

            Assert.AreEqual(TestBlockFactory.RED_BLOCK, block.Type.Id);
        }

        [Test]
        public void Undo_BlueToRed_Blue()
        {
            var (gameBoard, service) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.BLUE_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            
            ICommand action = new BlockChangeTypeCommand(block, TestBlockFactory.RedBlockType, service);
            action.Execute();
            Assert.AreEqual(TestBlockFactory.RED_BLOCK, block.Type.Id);

            action.Undo();

            Assert.AreEqual(TestBlockFactory.BLUE_BLOCK, block.Type.Id);
        }

        [Test]
        public void Execute_BlueToNull_NoChange()
        {
            var (gameBoard, service) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.BLUE_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            ICommand action = new BlockChangeTypeCommand(block, null, service);
            action.Execute();

            Assert.AreEqual(TestBlockFactory.BLUE_BLOCK, block.Type.Id);
        }

        [Test]
        public void Undo_BlueToNull_NoChange()
        {
            var (gameBoard, service) = Setup();
            var block = TestBlockFactory.CreateBlockInCell(TestBlockFactory.BLUE_BLOCK, gameBoard.Cells[0, 0], gameBoard);

            ICommand action = new BlockChangeTypeCommand(block, null, service);
            action.Execute();
            Assert.AreEqual(TestBlockFactory.BLUE_BLOCK, block.Type.Id);

            action.Undo();

            Assert.AreEqual(TestBlockFactory.BLUE_BLOCK, block.Type.Id);
        }

        [Test]
        public void Execute_InvalidBlock_NoChange()
        {
            var (gameBoard, service) = Setup();

            ICommand action = new BlockChangeTypeCommand(null, null, service);
            action.Execute();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
        }

        [Test]
        public void Undo_InvalidBlock_NoChange()
        {
            var (gameBoard, service) = Setup();

            ICommand action = new BlockChangeTypeCommand(null, null, service);
            action.Execute();
            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);

            action.Undo();

            Assert.AreEqual(null, gameBoard.Cells[0, 0].Block);
        }
    }
}