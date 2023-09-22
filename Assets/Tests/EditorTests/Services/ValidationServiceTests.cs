using Model.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnitTests;
using UnityEngine;

namespace Model.Services.UnitTests
{
    public class ValidationServiceTests
    {
        private (IValidationService validation, GameBoard gameBoard) Setup(ICellType cellType, params int[] preSpawnedBlocks)
        {
            var gameBoard = TestUtils.CreateGameBoard(1, 1, 0, preSpawnedBlocks);
            if (cellType != null)
                gameBoard.Cells[0, 0].ChangeType(cellType);
            else
                gameBoard.Cells[0, 0] = null;

            var validation = new ValidationService();
            validation.SetLevel(gameBoard);

            return (validation, gameBoard);
        }

        [Test]
        public void CellExistsAt_ValidCell_True()
        {
            var validation = Setup(TestUtils.BasicCellType).validation;

            bool isValid = validation.CellExistsAt(new(0,0));

            Assert.IsTrue(isValid);
        }
        [Test]
        public void CellExistsAt_OutOfBorders_False()
        {
            var validation = Setup(TestUtils.BasicCellType).validation;

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
            var validation = Setup(TestUtils.BasicCellType, TestUtils.DEFAULT_BLOCK).validation;

            bool isValid = validation.BlockExistsAt(new(0,0));

            Assert.IsTrue(isValid);
        }
        [Test]
        public void BlockExistsAt_CellCantContainBlock_False()
        {
            var validation = Setup(TestUtils.NotPlayableCellType).validation;

            bool isValid = validation.BlockExistsAt(new(0,0));

            Assert.IsFalse(isValid);
        }
        [Test]
        public void BlockExistsAt_EmptyCell_False()
        {
            var validation = Setup(TestUtils.BasicCellType).validation;

            bool isValid = validation.BlockExistsAt(new(0,0));

            Assert.IsFalse(isValid);
        }
        [Test]
        public void BlockExistsAt_NullBlock_False()
        {
            var tuple = Setup(TestUtils.BasicCellType);
            var validation = tuple.validation;
            tuple.gameBoard.Cells[0, 0].SetBlock(null);

            bool isValid = validation.BlockExistsAt(new(0,0));

            Assert.IsFalse(isValid);
        }
    }
}