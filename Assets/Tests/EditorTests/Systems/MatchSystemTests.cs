using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;

namespace Model.Systems.Tests
{
    public class MatchSystemTests
    {
        [Test]
        public void FindMatches_1MatchingBlock_ListWith1Cell()
        {
            Pattern[] matchPatterns = new Pattern[1];
            matchPatterns[0] = DotPattern1x1();
            Level level = new Level(1,1,matchPatterns);
            level.gameBoard.cells[0, 0].SetBlock(new Block(new BlueBlockType(), new Vector2Int(0,0)));
            MatchSystem matchSystem = new MatchSystem(level);

            List<Cell> matchedCells = matchSystem.FindMatches();

            Assert.AreEqual(level.gameBoard.cells[0, 0], matchedCells[0]);
        }

        [Test]
        public void FindMatches_MatchingLine_ListWithLineOfCells()
        {
            LogAssert.ignoreFailingMessages = true;

            Pattern[] matchPatterns = new Pattern[1];
            matchPatterns[0] = VertLinePattern1x3();
            Level level = new Level(3,3,matchPatterns);
            level.gameBoard.cells[0, 0].SetBlock(new Block(new BlueBlockType(), new Vector2Int(0,0)));
            level.gameBoard.cells[0, 1].SetBlock(new Block(new BlueBlockType(), new Vector2Int(0,0)));
            level.gameBoard.cells[0, 2].SetBlock(new Block(new BlueBlockType(), new Vector2Int(0,0)));
            MatchSystem matchSystem = new MatchSystem(level);

            List<Cell> matchedCells = matchSystem.FindMatches();

            Assert.AreEqual(level.gameBoard.cells[0, 0], matchedCells[0]);
            Assert.AreEqual(level.gameBoard.cells[0, 1], matchedCells[1]);
            Assert.AreEqual(level.gameBoard.cells[0, 2], matchedCells[2]);
            LogAssert.ignoreFailingMessages = false;
        }

        private Pattern DotPattern1x1()
        {
            bool[,] grid = new bool[1, 1];
            grid[0, 0] = true;
            return new Pattern(grid);
        }

        private Pattern VertLinePattern1x3()
        {
            bool[,] grid = new bool[1, 3];
            grid[0, 0] = true;
            grid[0, 1] = true;
            grid[0, 2] = true;
            return new Pattern(grid);
        }
    }
}