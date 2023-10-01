using System.Collections;
using System.Collections.Generic;
using Config;
using NUnit.Framework;
using UnitTests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Objects.UnitTests
{
    public class CounterTests
    {
        [Test]
        public void UpdateGoal_CorrectTarget_CountMinusOne()
        {
            var target = TestUtils.BlueBlockType;
            var goal = new Counter(target, 10);

            goal.CheckTarget(target);

            Assert.AreEqual(9, goal.Count);
        }

        [Test]
        public void UpdateGoal_BelowZero_CountZero()
        {
            var target = TestUtils.BlueBlockType;
            var goal = new Counter(target, 0);

            goal.CheckTarget(target);

            Assert.AreEqual(0, goal.Count);
        }

        [Test]
        public void UpdateGoal_IncorrectTarget_CountSame()
        {
            BasicBlockType target1 = (BasicBlockType)TestUtils.BlueBlockType;
            BasicBlockType target2 = (BasicBlockType)TestUtils.RedBlockType;
            var goal = new Counter(target1, 10);

            goal.CheckTarget(target2);

            Assert.AreEqual(10, goal.Count);
        }

        [Test]
        public void UpdateGoal_UpdateCountNotZero_UpdatedEvent()
        {
            var target = TestUtils.BlueBlockType;
            var goal = new Counter(target, 10);
            bool updated = false;
            bool completed = false;
            void TestUpdate(ICounterTarget _, int __) => updated = true;
            void TestComplete(ICounterTarget _) => completed = true;

            goal.OnUpdateEvent += TestUpdate;
            goal.OnCompleteEvent += TestComplete;
            goal.CheckTarget(target);
            goal.OnUpdateEvent -= TestUpdate;
            goal.OnCompleteEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(false, completed);
        }

        [Test]
        public void UpdateGoal_UpdateCountToZero_CompleteEvent()
        {
            var target = TestUtils.BlueBlockType;
            var goal = new Counter(target, 1);
            bool updated = false;
            bool completed = false;
            void TestUpdate(ICounterTarget _, int __) => updated = true;
            void TestComplete(ICounterTarget _) => completed = true;

            goal.OnUpdateEvent += TestUpdate;
            goal.OnCompleteEvent += TestComplete;
            goal.CheckTarget(target);
            goal.OnUpdateEvent -= TestUpdate;
            goal.OnCompleteEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(true, completed);
        }

        [Test]
        public void UpdateGoal_CountToZeroTwice_OneCompleteEvent()
        {
            var target = TestUtils.BlueBlockType;
            var goal = new Counter(target, 1);
            bool updated = false;
            bool completed = false;
            void TestUpdate(ICounterTarget _, int __) => updated = true;
            void TestComplete(ICounterTarget _) => completed = !completed;

            goal.OnUpdateEvent += TestUpdate;
            goal.OnCompleteEvent += TestComplete;
            goal.CheckTarget(target);
            goal.CheckTarget(target);
            goal.OnUpdateEvent -= TestUpdate;
            goal.OnCompleteEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(true, completed);
        }
    }
}