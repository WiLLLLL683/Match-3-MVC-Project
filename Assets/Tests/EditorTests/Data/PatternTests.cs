using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Data.UnitTests
{
    public class PatternTests
    {
        [Test]
        public void FindPattern_1MatchingBlock_ListWith1Cell()
        {
            GameBoard gameBoard = new GameBoard(1,1);
            gameBoard.Cells[0, 0].SpawnBlock(new BlueBlockType());
            bool[,] grid = new bool[1,1];
            grid[0,0] = true;
            Pattern pattern = new Pattern(grid);

            List<Cell> cells = pattern.Match(gameBoard,new Vector2Int(0,0));

            Assert.AreEqual(1, cells.Count);
            Assert.AreEqual(gameBoard.Cells[0,0], cells[0]);
        }

        [Test]
        public void FindPattern_2MatchingBlock_ListWith2Cell()
        {
            GameBoard gameBoard = new GameBoard(2, 2);
            gameBoard.Cells[0, 0].SpawnBlock(new BlueBlockType());
            gameBoard.Cells[1, 0].SpawnBlock(new BlueBlockType());
            gameBoard.Cells[0, 1].SpawnBlock(new RedBlockType());
            gameBoard.Cells[1, 1].SpawnBlock(new RedBlockType());
            bool[,] grid = new bool[2, 2];
            grid[0, 0] = true;
            grid[1, 0] = true;
            Pattern pattern = new Pattern(grid);

            List<Cell> cells = pattern.Match(gameBoard,new Vector2Int(0,0));

            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(gameBoard.Cells[0,0], cells[0]);
            Assert.AreEqual(gameBoard.Cells[1,0], cells[1]);
        }

        [Test]
        public void FindPattern_2MatchingBlockShifted_ListWith2Cell()
        {
            GameBoard gameBoard = new GameBoard(2, 2);
            gameBoard.Cells[0, 0].SpawnBlock(new BlueBlockType());
            gameBoard.Cells[1, 0].SpawnBlock(new BlueBlockType());
            gameBoard.Cells[0, 1].SpawnBlock(new RedBlockType());
            gameBoard.Cells[1, 1].SpawnBlock(new RedBlockType());
            bool[,] grid = new bool[2, 2];
            grid[0, 0] = true;
            grid[1, 0] = true;
            Pattern pattern = new Pattern(grid);

            List<Cell> cells = pattern.Match(gameBoard,new Vector2Int(0,1));

            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(gameBoard.Cells[0,1], cells[0]);
            Assert.AreEqual(gameBoard.Cells[1,1], cells[1]);
        }

        [Test]
        public void FindPattern_NoMatchingCell_Null()
        {
            GameBoard gameBoard = new GameBoard(2,2);
            gameBoard.Cells[0,0].SpawnBlock(new BlueBlockType());
            gameBoard.Cells[1,0].SpawnBlock(new RedBlockType());
            gameBoard.Cells[0,1].SpawnBlock(new BlueBlockType());
            gameBoard.Cells[1,1].SpawnBlock(new RedBlockType());
            bool[,] grid = new bool[2,2];
            grid[0,0] = true;
            grid[1,0] = true;
            Pattern pattern = new Pattern(grid);

            List<Cell> cells = pattern.Match(gameBoard,new Vector2Int(0,0));

            Assert.AreEqual(0, cells.Count);
        }

        [Test]
        public void FindPattern_EmptyPattern_Null()
        {
            GameBoard gameBoard = new GameBoard(1, 1);
            gameBoard.Cells[0, 0].SpawnBlock(new BlueBlockType());
            bool[,] grid = new bool[1, 1];
            grid[0, 0] = false;
            Pattern pattern = new Pattern(grid);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(0, 0));

            Assert.AreEqual(0, cells.Count);
        }

        [Test]
        public void FindPattern_NotValidCell_Null()
        {
            GameBoard gameBoard = new GameBoard(1, 1);
            gameBoard.Cells[0, 0].SpawnBlock(new BlueBlockType());
            bool[,] grid = new bool[1, 1];
            grid[0, 0] = true;
            Pattern pattern = new Pattern(grid);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(10, 10));

            Assert.AreEqual(0, cells.Count);
            LogAssert.Expect(LogType.Error, "Cell position out of GameBoards range");
        }

        [Test]
        public void FindPattern_NotPlayableCell_Null()
        {
            GameBoard gameBoard = new GameBoard(1, 1);
            gameBoard.Cells[0, 0].ChangeType(new NotPlayableCellType());
            bool[,] grid = new bool[1, 1];
            grid[0, 0] = true;
            Pattern pattern = new Pattern(grid);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(0, 0));

            Assert.AreEqual(0, cells.Count);
            LogAssert.Expect(LogType.Error, "Tried to get Block but Cell was notPlayable");
        }

        [Test]
        public void FindPattern_EmptyCell_Null()
        {
            GameBoard gameBoard = new GameBoard(1, 1);
            bool[,] grid = new bool[1, 1];
            grid[0, 0] = true;
            Pattern pattern = new Pattern(grid);

            List<Cell> cells = pattern.Match(gameBoard, new Vector2Int(0, 0));

            Assert.AreEqual(0, cells.Count);
            LogAssert.Expect(LogType.Error, "Tried to get Block but Cell was empty");
        }
    }
}