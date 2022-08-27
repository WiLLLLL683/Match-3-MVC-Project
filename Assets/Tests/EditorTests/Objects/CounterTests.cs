using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Objects.Tests
{
    public class CounterTests
    {
        [Test]
        public void UpdateGoal_CorrectTarget_CountMinusOne()
        {
            Counter goal = new Counter(new RedBlockType(),10);
            Block target = new Block(new RedBlockType(),new Vector2Int(0,0));

            goal.UpdateGoal(target.type);

            Assert.AreEqual(9, goal.Count);
        }

        [Test]
        public void UpdateGoal_BelowZero_CountZero()
        {
            Counter goal = new Counter(new RedBlockType(),0);
            Block target = new Block(new RedBlockType(),new Vector2Int(0,0));

            goal.UpdateGoal(target.type);

            Assert.AreEqual(0, goal.Count);
        }

        [Test]
        public void UpdateGoal_IncorrectTarget_CountSame()
        {
            Counter goal = new Counter(new BasicCellType(),10);
            Block target = new Block(new RedBlockType(),new Vector2Int(0,0));

            goal.UpdateGoal(target.type);

            Assert.AreEqual(10, goal.Count);
        }

        [Test]
        public void UpdateGoal_UpdateCountNotZero_UpdatedEvent()
        {
            Counter goal = new Counter(new RedBlockType(),10);
            Block target = new Block(new RedBlockType(),new Vector2Int(0,0));
            bool updated = false;
            bool completed = false;
            void TestUpdate(Counter goal,System.EventArgs eventArgs)
            {
                updated = true;
            }
            void TestComplete(Counter goal,System.EventArgs eventArgs)
            {
                completed = true;
            }

            goal.OnUpdateEvent += TestUpdate;
            goal.OnCompleteEvent += TestComplete;
            goal.UpdateGoal(target.type);
            goal.OnUpdateEvent -= TestUpdate;
            goal.OnCompleteEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(false, completed);
        }

        [Test]
        public void UpdateGoal_UpdateCountToZero_CompleteEvent()
        {
            Counter goal = new Counter(new RedBlockType(), 1);
            Block target = new Block(new RedBlockType(), new Vector2Int(0, 0));
            bool updated = false;
            bool completed = false;
            void TestUpdate(Counter goal, System.EventArgs eventArgs)
            {
                updated = true;
            }
            void TestComplete(Counter goal, System.EventArgs eventArgs)
            {
                completed = true;
            }

            goal.OnUpdateEvent += TestUpdate;
            goal.OnCompleteEvent += TestComplete;
            goal.UpdateGoal(target.type);
            goal.OnUpdateEvent -= TestUpdate;
            goal.OnCompleteEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(true, completed);
        }

        [Test]
        public void UpdateGoal_CountToZeroTwise_OneCompleteEvent()
        {
            Counter goal = new Counter(new RedBlockType(), 1);
            Block target = new Block(new RedBlockType(), new Vector2Int(0, 0));
            bool updated = false;
            bool completed = false;
            void TestUpdate(Counter goal, System.EventArgs eventArgs)
            {
                updated = true;
            }
            void TestComplete(Counter goal, System.EventArgs eventArgs)
            {
                completed = !completed;
            }

            goal.OnUpdateEvent += TestUpdate;
            goal.OnCompleteEvent += TestComplete;
            goal.UpdateGoal(target.type);
            goal.UpdateGoal(target.type);
            goal.OnUpdateEvent -= TestUpdate;
            goal.OnCompleteEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(true, completed);
        }
    }
}