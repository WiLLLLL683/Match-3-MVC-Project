using Model.Objects;
using NUnit.Framework;
using TestUtils;

namespace Model.Services.UnitTests
{
    public class CounterServiceTests
    {
        private int updatedEventCount = 0;
        private int completedEventCount = 0;

        private CounterService Setup()
        {
            var service = new CounterService();

            updatedEventCount = 0;
            completedEventCount = 0;
            service.OnUpdateEvent += (_) => updatedEventCount++;
            service.OnCompleteEvent += (_) => completedEventCount++;

            return service;
        }

        [Test]
        public void CheckTarget_CorrectTarget_CountMinusOne()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);

            service.DecreaseCount(goal, target, 1);

            Assert.AreEqual(9, goal.Count);
        }

        [Test]
        public void CheckTarget_BelowZero_CountZero()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 0);

            service.DecreaseCount(goal, target, 1);

            Assert.AreEqual(0, goal.Count);
        }

        [Test]
        public void CheckTarget_IncorrectTarget_CountSame()
        {
            var service = Setup();
            BasicBlockType target1 = TestBlockFactory.BlueBlockType;
            BasicBlockType target2 = TestBlockFactory.RedBlockType;
            var goal = new Counter(target1, 10);

            service.DecreaseCount(goal, target2, 1);

            Assert.AreEqual(10, goal.Count);
        }

        [Test]
        public void CheckTarget_UpdateCountNotZero_UpdatedEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);
            service.DecreaseCount(goal, target, 1);

            Assert.AreEqual(1, updatedEventCount);
            Assert.AreEqual(0, completedEventCount);
        }

        [Test]
        public void CheckTarget_UpdateCountToZero_CompleteEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 1);

            service.DecreaseCount(goal, target, 1);

            Assert.AreEqual(1, updatedEventCount);
            Assert.AreEqual(1, completedEventCount);
        }

        [Test]
        public void CheckTarget_CountToZeroTwice_OneCompleteEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 1);

            service.DecreaseCount(goal, target, 1);
            service.DecreaseCount(goal, target, 1);

            Assert.AreEqual(1, updatedEventCount);
            Assert.AreEqual(1, completedEventCount);
        }

        [Test]
        public void CheckCompletion_CountIsZero_TrueEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 0);

            var isCompleted = service.CheckCompletion(goal);

            Assert.AreEqual(true, isCompleted);
            Assert.AreEqual(1, completedEventCount);
        }

        [Test]
        public void CheckCompletion_CountIsNegative_TrueEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, -99);

            var isCompleted = service.CheckCompletion(goal);

            Assert.AreEqual(true, isCompleted);
            Assert.AreEqual(1, completedEventCount);
            Assert.AreEqual(0, goal.Count);
        }

        [Test]
        public void CheckCompletion_CountIsPositive_False()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 99);

            var isCompleted = service.CheckCompletion(goal);

            Assert.AreEqual(false, isCompleted);
            Assert.AreEqual(0, completedEventCount);
            Assert.AreEqual(99, goal.Count);
        }

        [Test]
        public void CheckCompletion_IsCompleted_TrueNoEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 0) { IsCompleted = true };

            var isCompleted = service.CheckCompletion(goal);

            Assert.AreEqual(true, isCompleted);
            Assert.AreEqual(0, completedEventCount);
            Assert.AreEqual(0, goal.Count);
        }

        [Test]
        public void CheckCompletion_Null_False()
        {
            var service = Setup();
            Counter goal = null;

            var isCompleted = service.CheckCompletion(goal);

            Assert.AreEqual(false, isCompleted);
            Assert.AreEqual(0, completedEventCount);
        }
    }
}