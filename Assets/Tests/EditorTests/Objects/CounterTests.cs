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
            Counter goal = new Counter(new BasicBlockType(),10);
            Block target = CreateBlock();

            goal.UpdateGoal(target.type);

            Assert.AreEqual(9, goal.Count);
        }

        [Test]
        public void UpdateGoal_BelowZero_CountZero()
        {
            Counter goal = new Counter(new BasicBlockType(),0);
            Block target = CreateBlock();

            goal.UpdateGoal(target.type);

            Assert.AreEqual(0, goal.Count);
        }

        [Test]
        public void UpdateGoal_IncorrectTarget_CountSame()
        {
            Counter goal = new Counter(new RedBlockType(),10);
            Block target = CreateBlock();

            goal.UpdateGoal(target.type);

            Assert.AreEqual(10, goal.Count);
        }

        [Test]
        public void UpdateGoal_UpdateCountNotZero_UpdatedEvent()
        {
            Counter goal = new Counter(new BasicBlockType(),10);
            Block target = CreateBlock();
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
            Counter goal = new Counter(new BasicBlockType(), 1);
            Block target = CreateBlock();
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
            Counter goal = new Counter(new BasicBlockType(), 1);
            Block target = CreateBlock();
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

        private static Block CreateBlock()
        {
            Cell cellA = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Block block = new Block(new BasicBlockType(), cellA);
            return block;
        }

    }
}