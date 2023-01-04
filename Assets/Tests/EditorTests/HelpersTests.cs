using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Tests
{
    public class HelpersTests
    {
        [Test]
        public void CheckValidCellByPosition_ValidCell_Valid()
        {
            GameBoard gameboard = new GameBoard(1, 1);
            Vector2Int position = new Vector2Int(0,0);

            bool isValid = Helpers.CheckValidCellByPosition(gameboard, position);

            Assert.IsTrue(isValid);
        }

        [Test]
        public void CheckValidCellByPosition_InValidCell_InValid()
        {
            GameBoard gameboard = new GameBoard(1, 1);
            Vector2Int position = new Vector2Int(100,100);

            bool isValid = Helpers.CheckValidCellByPosition(gameboard, position);

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            Assert.IsFalse(isValid);
        }

        [Test]
        public void CheckValidBlockByPosition_ValidBlock_Valid()
        {
            GameBoard gameboard = new GameBoard(1, 1);
            Vector2Int position = new Vector2Int(0, 0);
            gameboard.cells[position.x, position.y].SpawnBlock(new BlueBlockType());

            bool isValid = Helpers.CheckValidBlockByPosition(gameboard, position);

            Assert.IsTrue(isValid);
        }

        [Test]
        public void CheckValidBlockByPosition_InValidPos_InValid()
        {
            GameBoard gameboard = new GameBoard(1, 1);
            Vector2Int position = new Vector2Int(100, 100);

            bool isValid = Helpers.CheckValidBlockByPosition(gameboard, position);

            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
            Assert.IsFalse(isValid);
        }

        [Test]
        public void CheckValidBlockByPosition_EmptyCell_InValid()
        {
            GameBoard gameboard = new GameBoard(1, 1);
            Vector2Int position = new Vector2Int(0, 0);

            bool isValid = Helpers.CheckValidBlockByPosition(gameboard, position);

            LogAssert.Expect(LogType.Error, "Tried to get Block but Cell was empty");
            Assert.IsFalse(isValid);
        }

        [Test]
        public void CheckValidBlockByPosition_NotPlayableCell_InValid()
        {
            GameBoard gameboard = new GameBoard(1, 1);
            Vector2Int position = new Vector2Int(0, 0);
            gameboard.cells[0, 0] = new Cell(new NotPlayableCellType(), position);

            bool isValid = Helpers.CheckValidBlockByPosition(gameboard, position);

            LogAssert.Expect(LogType.Error, "Tried to get Block but Cell was notPlayable");
            Assert.IsFalse(isValid);
        }
    }
}
