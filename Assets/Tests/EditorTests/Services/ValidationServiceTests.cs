using Model.Objects;
using NUnit.Framework;
using TestUtils;

namespace Model.Services.UnitTests
{
    public class ValidationServiceTests
    {
        private (IValidationService validation, GameBoard gameBoard) Setup(CellType cellType, params int[] preSpawnedBlocks)
        {
            var gameBoard = TestLevelFactory.CreateGameBoard(1, 1, 0, preSpawnedBlocks);
            if (cellType != null)
                gameBoard.Cells[0, 0].Type = cellType;
            else
                gameBoard.Cells[0, 0] = null;

            var validation = new ValidationService();
            validation.SetLevel(gameBoard);

            return (validation, gameBoard);
        }

        [Test]
        public void CellExistsAt_ValidCell_True()
        {
            var validation = Setup(TestCellFactory.BasicCellType).validation;

            bool isValid = validation.CellExistsAt(new(0,0));

            Assert.IsTrue(isValid);
        }
        [Test]
        public void CellExistsAt_OutOfBorders_False()
        {
            var validation = Setup(TestCellFactory.BasicCellType).validation;

            bool isValid = validation.CellExistsAt(new(100,100));

            Assert.IsFalse(isValid);
        }
        [Test]
        public void CellExistsAt_NullCell_False()
        {
            var validation = Setup(null).validation;

            bool isValid = validation.CellExistsAt(new(0,0));

            Assert.IsFalse(isValid);
        }
        [Test]
        public void BlockExistsAt_ValidBlock_True()
        {
            var validation = Setup(TestCellFactory.BasicCellType, TestBlockFactory.DEFAULT_BLOCK).validation;

            bool isValid = validation.BlockExistsAt(new(0,0));

            Assert.IsTrue(isValid);
        }
        [Test]
        public void BlockExistsAt_CellCantContainBlock_False()
        {
            var validation = Setup(TestCellFactory.NotPlayableCellType).validation;

            bool isValid = validation.BlockExistsAt(new(0,0));

            Assert.IsFalse(isValid);
        }
        [Test]
        public void BlockExistsAt_EmptyCell_False()
        {
            var validation = Setup(TestCellFactory.BasicCellType).validation;

            bool isValid = validation.BlockExistsAt(new(0,0));

            Assert.IsFalse(isValid);
        }
        [Test]
        public void BlockExistsAt_NullBlock_False()
        {
            var tuple = Setup(TestCellFactory.BasicCellType);
            var validation = tuple.validation;
            tuple.gameBoard.Cells[0, 0].Block = null;

            bool isValid = validation.BlockExistsAt(new(0,0));

            Assert.IsFalse(isValid);
        }
    }
}