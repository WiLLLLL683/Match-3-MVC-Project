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

        //Increase
        [Test]
        public void IncreaseCount_PositiveAmount_CountIncreaseUpdateEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);

            service.IncreaseCount(goal, target, 10);

            Assert.AreEqual(20, goal.Count);
            Assert.AreEqual(1, updatedEventCount);
        }

        [Test]
        public void IncreaseCount_NegativeAmount_CountSameNoEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);

            service.IncreaseCount(goal, target, -10);

            Assert.AreEqual(10, goal.Count);
            Assert.AreEqual(0, updatedEventCount);
        }

        [Test]
        public void IncreaseCount_IsCompleted_CountSameNoEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);
            goal.IsCompleted = true;

            service.IncreaseCount(goal, target, 10);

            Assert.AreEqual(10, goal.Count);
            Assert.AreEqual(0, updatedEventCount);
        }

        [Test]
        public void IncreaseCount_WrongTargetType_CountSameNoEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var target2 = TestBlockFactory.RedBlockType;
            var goal = new Counter(target, 10);

            service.IncreaseCount(goal, target2, 10);

            Assert.AreEqual(10, goal.Count);
            Assert.AreEqual(0, updatedEventCount);
        }

        [Test]
        public void IncreaseCount_WrongTargetId_CountSameNoEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.CreateBlockType(0);
            var target2 = TestBlockFactory.CreateBlockType(999);
            var goal = new Counter(target, 10);

            service.IncreaseCount(goal, target2, 10);

            Assert.AreEqual(10, goal.Count);
            Assert.AreEqual(0, updatedEventCount);
        }

        //Decrease
        [Test]
        public void DecreaseCount_PositiveAmount_CountDecreaseUpdateEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);

            service.DecreaseCount(goal, target, 1);

            Assert.AreEqual(9, goal.Count);
            Assert.AreEqual(1, updatedEventCount);
        }

        [Test]
        public void DecreaseCount_ToZero_CountZeroUpdateEventCompleteEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);

            service.DecreaseCount(goal, target, 10);

            Assert.AreEqual(0, goal.Count);
            Assert.AreEqual(true, goal.IsCompleted);
            Assert.AreEqual(1, updatedEventCount);
            Assert.AreEqual(1, completedEventCount);
        }

        [Test]
        public void DecreaseCount_ToZeroTwice_CountZeroUpdateEventCompleteEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);

            service.DecreaseCount(goal, target, 10);
            service.DecreaseCount(goal, target, 10);

            Assert.AreEqual(0, goal.Count);
            Assert.AreEqual(true, goal.IsCompleted);
            Assert.AreEqual(1, updatedEventCount);
            Assert.AreEqual(1, completedEventCount);
        }

        [Test]
        public void DecreaseCount_NegativeAmount_CountSameNoEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);

            service.DecreaseCount(goal, target, -1);

            Assert.AreEqual(10, goal.Count);
            Assert.AreEqual(0, updatedEventCount);
        }

        [Test]
        public void DecreaseCount_IsCompleted_CountSameNoEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var goal = new Counter(target, 10);
            goal.IsCompleted = true;

            service.DecreaseCount(goal, target, 1);

            Assert.AreEqual(10, goal.Count);
            Assert.AreEqual(0, updatedEventCount);
        }

        [Test]
        public void DecreaseCount_WrongTargetType_CountSameNoEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.BlueBlockType;
            var target2 = TestBlockFactory.RedBlockType;
            var goal = new Counter(target, 10);

            service.DecreaseCount(goal, target2, 1);

            Assert.AreEqual(10, goal.Count);
            Assert.AreEqual(0, updatedEventCount);
        }

        [Test]
        public void DecreaseCount_WrongTargetId_CountSameNoEvent()
        {
            var service = Setup();
            var target = TestBlockFactory.CreateBlockType(0);
            var target2 = TestBlockFactory.CreateBlockType(999);
            var goal = new Counter(target, 10);

            service.DecreaseCount(goal, target2, 1);

            Assert.AreEqual(10, goal.Count);
            Assert.AreEqual(0, updatedEventCount);
        }

        //CheckCompletion
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
        public void CheckCompletion_CountIsPositive_FalseNoEvent()
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
        public void CheckCompletion_Null_FalseNoEvent()
        {
            var service = Setup();
            Counter goal = null;

            var isCompleted = service.CheckCompletion(goal);

            Assert.AreEqual(false, isCompleted);
            Assert.AreEqual(0, completedEventCount);
        }
    }
}