using Model.Objects;
using NUnit.Framework;
using TestUtils;

namespace Model.Services.UnitTests
{
    public class CounterServiceTests
    {
        [Test]
        public void UpdateGoal_CorrectTarget_CountMinusOne()
        {
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);
            var service = new CounterService();

            service.CheckTarget(goal, target);

            Assert.AreEqual(9, goal.Count);
        }

        [Test]
        public void UpdateGoal_BelowZero_CountZero()
        {
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 0);
            var service = new CounterService();

            service.CheckTarget(goal, target);

            Assert.AreEqual(0, goal.Count);
        }

        [Test]
        public void UpdateGoal_IncorrectTarget_CountSame()
        {
            BasicBlockType target1 = TestBlockFactory.BlueBlockType;
            BasicBlockType target2 = TestBlockFactory.RedBlockType;
            var goal = new Counter(target1, 10);
            var service = new CounterService();

            service.CheckTarget(goal, target2);

            Assert.AreEqual(10, goal.Count);
        }

        [Test]
        public void UpdateGoal_UpdateCountNotZero_UpdatedEvent()
        {
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);
            var service = new CounterService();
            bool updated = false;
            bool completed = false;
            void TestUpdate(Counter _) => updated = true;
            void TestComplete(Counter _) => completed = true;

            service.OnUpdateEvent += TestUpdate;
            service.OnCompleteEvent += TestComplete;
            service.CheckTarget(goal, target);
            service.OnUpdateEvent -= TestUpdate;
            service.OnCompleteEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(false, completed);
        }

        [Test]
        public void UpdateGoal_UpdateCountToZero_CompleteEvent()
        {
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 1);
            var service = new CounterService();
            bool updated = false;
            bool completed = false;
            void TestUpdate(Counter _) => updated = true;
            void TestComplete(Counter _) => completed = true;

            service.OnUpdateEvent += TestUpdate;
            service.OnCompleteEvent += TestComplete;
            service.CheckTarget(goal, target);
            service.OnUpdateEvent -= TestUpdate;
            service.OnCompleteEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(true, completed);
        }

        [Test]
        public void UpdateGoal_CountToZeroTwice_OneCompleteEvent()
        {
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 1);
            var service = new CounterService();
            bool updated = false;
            bool completed = false;
            void TestUpdate(Counter _) => updated = true;
            void TestComplete(Counter _) => completed = true;

            service.OnUpdateEvent += TestUpdate;
            service.OnCompleteEvent += TestComplete;
            service.CheckTarget(goal, target);
            service.CheckTarget(goal, target);
            service.OnUpdateEvent -= TestUpdate;
            service.OnCompleteEvent -= TestComplete;

            Assert.AreEqual(true, updated);
            Assert.AreEqual(true, completed);
        }
    }
}