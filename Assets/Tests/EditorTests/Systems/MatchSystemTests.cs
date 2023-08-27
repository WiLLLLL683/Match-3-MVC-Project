using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using Data;
using System.Linq;
using UnitTests;
using NSubstitute;
using Model.Services;

namespace Model.Systems.UnitTests
{
    public class MatchSystemTests
    {
        private IValidationService validation = Substitute.For<IValidationService>();

        [SetUp]
        public void Setup()
        {
            Debug.Log("Before");
            validation.BlockExistsAt(default).ReturnsForAnyArgs(true);
        }

        [TearDown]
        public void Cleanup()
        {
            Debug.Log("After");
        }

        [Test]
        public void FindMatches_1MatchingBlock_ListWith1Cell()
        {
            Pattern[] matchPatterns = new Pattern[1];
            matchPatterns[0] = DotPattern1x1();
            Level level = DotLevel1x1(matchPatterns);
            MatchSystem matchSystem = new MatchSystem(validation);
            matchSystem.SetLevel(level);

            List<Cell> matchedCells = matchSystem.FindAllMatches().ToList();

            Assert.AreEqual(level.gameBoard.cells[0, 0], matchedCells[0]);
        }

        [Test]
        public void FindMatches_EmptyCells_EmptyList()
        {
            Pattern[] matchPatterns = new Pattern[1];
            matchPatterns[0] = DotPattern1x1();
            Level level = TestUtils.CreateLevel(1,1, matchPatterns);
            MatchSystem matchSystem = new MatchSystem(validation);
            matchSystem.SetLevel(level);

            List<Cell> matchedCells = matchSystem.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
            LogAssert.Expect(LogType.Warning, "Tried to get Block but Cell was empty");
        }

        [Test]
        public void FindMatches_NotPlayableCells_EmptyList()
        {
            Pattern[] matchPatterns = new Pattern[1];
            matchPatterns[0] = DotPattern1x1();
            Level level = TestUtils.CreateLevel(1,1, matchPatterns);
            level.gameBoard.cells[0, 0].ChangeType(TestUtils.NotPlayableCellType);
            MatchSystem matchSystem = new MatchSystem(validation);
            matchSystem.SetLevel(level);

            List<Cell> matchedCells = matchSystem.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
            //LogAssert.Expect(LogType.Warning, "Tried to get Block but Cell was notPlayable");
        }


        [Test]
        public void FindMatches_MatchingLine_ListWithLineOfCells()
        {
            LogAssert.ignoreFailingMessages = true;

            Pattern[] matchPatterns = new Pattern[1];
            matchPatterns[0] = VertLinePattern1x3();
            Level level = VertLineLevel1x3(matchPatterns);
            MatchSystem matchSystem = new MatchSystem(validation);
            matchSystem.SetLevel(level);

            List<Cell> matchedCells = matchSystem.FindAllMatches().ToList();

            Assert.AreEqual(level.gameBoard.cells[0, 0], matchedCells[0]);
            Assert.AreEqual(level.gameBoard.cells[0, 1], matchedCells[1]);
            Assert.AreEqual(level.gameBoard.cells[0, 2], matchedCells[2]);
            LogAssert.ignoreFailingMessages = false;
        }


        [Test]
        public void FindMatches_MatchingLineShifted_ListWithLineOfCells()
        {
            LogAssert.ignoreFailingMessages = true;

            Pattern[] matchPatterns = new Pattern[1];
            matchPatterns[0] = VertLinePattern1x3();
            Level level = VertLineLevel3x3(matchPatterns);
            MatchSystem matchSystem = new MatchSystem(validation);
            matchSystem.SetLevel(level);

            List<Cell> matchedCells = matchSystem.FindAllMatches().ToList();

            Assert.AreEqual(level.gameBoard.cells[2, 0], matchedCells[0]);
            Assert.AreEqual(level.gameBoard.cells[2, 1], matchedCells[1]);
            Assert.AreEqual(level.gameBoard.cells[2, 2], matchedCells[2]);
            LogAssert.ignoreFailingMessages = false;
        }


        [Test]
        public void FindMatches_MatchingCross_CrossWithLineOfCells()
        {
            LogAssert.ignoreFailingMessages = true;

            Pattern[] matchPatterns = new Pattern[1];
            matchPatterns[0] = CrossPattern3x3();
            Level level = CrossLevel3x3(matchPatterns);
            MatchSystem matchSystem = new MatchSystem(validation);
            matchSystem.SetLevel(level);

            List<Cell> matchedCells = matchSystem.FindAllMatches().ToList();

            Assert.AreEqual(level.gameBoard.cells[0, 1], matchedCells[0]);
            Assert.AreEqual(level.gameBoard.cells[1, 0], matchedCells[1]);
            Assert.AreEqual(level.gameBoard.cells[1, 1], matchedCells[2]);
            Assert.AreEqual(level.gameBoard.cells[1, 2], matchedCells[3]);
            Assert.AreEqual(level.gameBoard.cells[2, 1], matchedCells[4]);
            LogAssert.ignoreFailingMessages = false;
        }        
        
        [Test]
        public void FindMatches_NoMatch_EmptyList()
        {
            LogAssert.ignoreFailingMessages = true;

            Pattern[] matchPatterns = new Pattern[1];
            matchPatterns[0] = CrossPattern3x3();
            Level level = NoMatchLevel3x3(matchPatterns);
            MatchSystem matchSystem = new MatchSystem(validation);
            matchSystem.SetLevel(level);

            List<Cell> matchedCells = matchSystem.FindAllMatches().ToList();

            Assert.AreEqual(0, matchedCells.Count);
            LogAssert.ignoreFailingMessages = false;
        }




        private Level DotLevel1x1(Pattern[] matchPatterns)
        {
            Level level = TestUtils.CreateLevel(1, 1, matchPatterns);
            level.gameBoard.cells[0, 0].SpawnBlock(new BasicBlockType());
            return level;
        }
        private Level VertLineLevel1x3(Pattern[] matchPatterns)
        {
            Level level = TestUtils.CreateLevel(1, 3, matchPatterns);
            level.gameBoard.cells[0, 0].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[0, 1].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[0, 2].SpawnBlock(new BasicBlockType());
            return level;
        }
        private Level VertLineLevel3x3(Pattern[] matchPatterns)
        {
            Level level = TestUtils.CreateLevel(3, 3, matchPatterns);
            level.gameBoard.cells[2, 0].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[2, 1].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[2, 2].SpawnBlock(new BasicBlockType());
            return level;
        }
        private Level NoMatchLevel3x3(Pattern[] matchPatterns)
        {
            Level level = TestUtils.CreateLevel(3, 3, matchPatterns);
            level.gameBoard.cells[0, 0].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[0, 1].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[0, 2].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[1, 0].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[1, 1].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[1, 2].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[2, 0].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[2, 1].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[2, 2].SpawnBlock(new BasicBlockType());
            return level;
        }
        private Level CrossLevel3x3(Pattern[] matchPatterns)
        {
            Level level = TestUtils.CreateLevel(3, 3, matchPatterns);
            level.gameBoard.cells[0, 1].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[1, 0].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[1, 1].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[1, 2].SpawnBlock(new BasicBlockType());
            level.gameBoard.cells[2, 1].SpawnBlock(new BasicBlockType());
            return level;
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
        private Pattern CrossPattern3x3()
        {
            bool[,] grid = new bool[3, 3];
            grid[0, 1] = true;
            grid[1, 0] = true;
            grid[1, 1] = true;
            grid[1, 2] = true;
            grid[2, 1] = true;
            return new Pattern(grid);
        }
    }
}