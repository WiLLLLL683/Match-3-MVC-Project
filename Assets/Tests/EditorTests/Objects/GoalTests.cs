using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Objects.Tests
{
    public class GoalTests
    {
        [Test]
        public void UpdateGoal_CorrectTarget_CountMinusOne()
        {
            Goal goal = new Goal(new RedBlockType(),10);
            Block target = new Block(new RedBlockType(),new Vector2Int(0,0));

            goal.UpdateGoal(target.type);

            Assert.AreEqual(9, goal.Count);
        }

        [Test]
        public void UpdateGoal_IncorrectTarget_CountSame()
        {
            Goal goal = new Goal(new BasicCellType(),10);
            Block target = new Block(new RedBlockType(),new Vector2Int(0,0));

            goal.UpdateGoal(target.type);

            Assert.AreEqual(10, goal.Count);
        }

        [Test]
        public void UpdateGoal_UpdateCountNotZero_UpdatedEvent()
        {
            Goal goal = new Goal(new RedBlockType(),10);
            Block target = new Block(new RedBlockType(),new Vector2Int(0,0));
            bool updated = false;
            bool completed = false;
            void TestUpdate(Goal goal,System.EventArgs eventArgs)
            {
                updated = true;
            }
            void TestComplete(Goal goal,System.EventArgs eventArgs)
            {
                completed = true;
            }

            goal.goalIsUpdatedEvent += TestUpdate;
            goal.goalIsCompletedEvent += TestComplete;
            goal.UpdateGoal(target.type);
            goal.goalIsUpdatedEvent -= TestUpdate;
            goal.goalIsCompletedEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(false, completed);
        }

        [Test]
        public void UpdateGoal_UpdateCountToZero_CompleteEvent()
        {
            Goal goal = new Goal(new RedBlockType(), 1);
            Block target = new Block(new RedBlockType(), new Vector2Int(0, 0));
            bool updated = false;
            bool completed = false;
            void TestUpdate(Goal goal, System.EventArgs eventArgs)
            {
                updated = true;
            }
            void TestComplete(Goal goal, System.EventArgs eventArgs)
            {
                completed = true;
            }

            goal.goalIsUpdatedEvent += TestUpdate;
            goal.goalIsCompletedEvent += TestComplete;
            goal.UpdateGoal(target.type);
            goal.goalIsUpdatedEvent -= TestUpdate;
            goal.goalIsCompletedEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(true, completed);
        }
    }
}