using System.Collections;
using System.Collections.Generic;
using Data;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;

namespace Data.Tests
{
    public class LevelDataTests
    {
        LevelData validLevel = new LevelData(
            new GameBoardData() { cellTypes = new BasicCellType[1, 1] },
            new CounterData[1] { new CounterData() { target = new BlueBlockType(), count = 1 } },
            new CounterData[1] { new CounterData() { target = new BlueBlockType(), count = 1 } },
            new BalanceData());

        [Test]
        public void ValidCheck_ValidLevel_True()
        {
            LevelData level = validLevel;

            bool test = level.ValidCheck();

            Assert.IsTrue(test);
        }

        [Test]
        public void ValidCheck_InvalidGameboard_False()
        {
            LevelData level = validLevel;
            level.gameBoard.cellTypes[0,0] = null;

            bool test = level.ValidCheck();

            Assert.IsNotNull(test);
        }

        [Test]
        public void ValidCheck_InvalidGoals_False()
        {
            LevelData level = validLevel;
            level.goals[0].target = null;

            bool test = level.ValidCheck();

            Assert.IsNotNull(test);
        }

        [Test]
        public void ValidCheck_InvalidRestrictions_False()
        {
            LevelData level = validLevel;
            level.restrictions[0].target = null;

            bool test = level.ValidCheck();

            Assert.IsNotNull(test);
        }

        [Test]
        public void ValidCheck_InvalidLevel_False()
        {
            LevelData level = validLevel;
            level.gameBoard.cellTypes[0, 0] = null;
            level.goals[0].target = null;
            level.restrictions[0].target = null;

            bool test = level.ValidCheck();

            Assert.IsNotNull(test);
        }
    }
}