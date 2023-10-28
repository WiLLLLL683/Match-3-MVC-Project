using Model.Objects;
using Model.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestUtils;
using UnityEngine;

namespace Model.Services.UnitTests
{
    public class WinLoseServiceTests
    {
        private int winEventCount = 0;
        private int loseEventCount = 0;

        private (Level level, WinLoseService service) Setup()
        {
            var level = TestLevelFactory.CreateLevel(1, 1);
            var counterService = new CounterService();
            var service = new WinLoseService(counterService);
            service.SetLevel(level);

            winEventCount = 0;
            loseEventCount = 0;
            service.OnWin += () => winEventCount++;
            service.OnLose += () => loseEventCount++;

            return (level, service);
        }

        [Test]
        public void CheckWin_AllGoalsCompleted_True()
        {
            var (level, service) = Setup();
            level.goals = new Counter[2] {
                new Counter(TestBlockFactory.DefaultBlockType, 0),
                new Counter(TestBlockFactory.RedBlockType, 0) };

            bool isWin = service.CheckWin();

            Assert.AreEqual(true, isWin);
            Assert.AreEqual(1, winEventCount);
        }

        [Test]
        public void CheckWin_NoCompletedGoals_False()
        {
            var (level, service) = Setup();
            level.goals = new Counter[2] {
                new Counter(TestBlockFactory.DefaultBlockType, 100),
                new Counter(TestBlockFactory.RedBlockType, 100) };

            bool isWin = service.CheckWin();

            Assert.AreEqual(false, isWin);
            Assert.AreEqual(0, winEventCount);
        }

        [Test]
        public void CheckWin_NotAllGoalsCompleted_False()
        {
            var (level, service) = Setup();
            level.goals = new Counter[2] {
                new Counter(TestBlockFactory.DefaultBlockType, 100),
                new Counter(TestBlockFactory.RedBlockType, 0) };

            bool isWin = service.CheckWin();

            Assert.AreEqual(false, isWin);
            Assert.AreEqual(0, winEventCount);
        }

        [Test]
        public void CheckLose_AllRestrictionsCompleted_True()
        {
            var (level, service) = Setup();
            level.restrictions = new Counter[2] {
                new Counter(TestBlockFactory.DefaultBlockType, 0),
                new Counter(TestBlockFactory.RedBlockType, 0) };

            bool isLose = service.CheckLose();

            Assert.AreEqual(true, isLose);
            Assert.AreEqual(1, loseEventCount);
        }

        [Test]
        public void CheckLose_NoCompletedRestrictions_False()
        {
            var (level, service) = Setup();
            level.restrictions = new Counter[2] {
                new Counter(TestBlockFactory.DefaultBlockType, 100),
                new Counter(TestBlockFactory.RedBlockType, 100) };

            bool isLose = service.CheckLose();

            Assert.AreEqual(false, isLose);
            Assert.AreEqual(0, loseEventCount);
        }

        [Test]
        public void CheckLose_OneRestrictionCompleted_True()
        {
            var (level, service) = Setup();
            level.restrictions = new Counter[2] {
                new Counter(TestBlockFactory.DefaultBlockType, 100),
                new Counter(TestBlockFactory.RedBlockType, 0) };

            bool isLose = service.CheckLose();

            Assert.AreEqual(true, isLose);
            Assert.AreEqual(1, loseEventCount);
        }

        [Test]
        public void UpdateGoals_ValidTarget_CounterMinusOne()
        {
            var (level, service) = Setup();
            var target = TestBlockFactory.DefaultBlockType;
            level.goals = new Counter[2] {
                new Counter(TestBlockFactory.DefaultBlockType, 100),
                new Counter(TestBlockFactory.RedBlockType, 100)};

            service.UpdateGoals(target);

            Assert.AreEqual(99, level.goals[0].Count);
            Assert.AreEqual(100, level.goals[1].Count);
        }

        [Test]
        public void UpdateGoals_InValidTarget_NoChange()
        {
            var (level, service) = Setup();
            var target = TestBlockFactory.BlueBlockType;
            level.goals = new Counter[2] {
                new Counter(TestBlockFactory.DefaultBlockType, 100),
                new Counter(TestBlockFactory.RedBlockType, 100)};

            service.UpdateGoals(target);

            Assert.AreEqual(100, level.goals[0].Count);
            Assert.AreEqual(100, level.goals[1].Count);
        }
    }
}