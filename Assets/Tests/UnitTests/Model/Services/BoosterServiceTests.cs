using Model.Factories;
using Model.Objects;
using Model.Services;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using TestUtils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Services.UnitTests
{
    public class BoosterServiceTests
    {
        private const int BOOSTER_1 = 1;
        private int boosterUsedCount;

        private (BoosterService service, Game game) Setup()
        {
            boosterUsedCount = 0;
            //model
            var game = TestLevelFactory.CreateGame(1,1);
            game.BoosterInventory = new();
            //dependencies
            var factory = Substitute.For<IBoosterFactory>();
            var booster1 = Substitute.For<IBooster>();
            factory.Create(BOOSTER_1).Returns(booster1);
            booster1.WhenForAnyArgs(x => x.Execute(default, default, default, default)).Do(_ => boosterUsedCount++);
            var validationService = Substitute.For<IValidationService>();
            var destroyService = Substitute.For<IBlockDestroyService>();
            var moveService = Substitute.For<IBlockMoveService>();
            //service
            var service = new BoosterService(game, factory, validationService, moveService);
            return (service, game);
        }

        [Test]
        public void AddBooster_NewBooster_NewBoosterAdded()
        {
            var(service, game) = Setup();
            Assert.IsFalse(game.BoosterInventory.boosters.ContainsKey(BOOSTER_1));

            service.AddBooster(BOOSTER_1, 99);

            Assert.IsTrue(game.BoosterInventory.boosters.ContainsKey(BOOSTER_1));
            Assert.AreEqual(99, game.BoosterInventory.boosters[BOOSTER_1]);
        }

        [Test]
        public void AddBooster_ExistingBooster_BoosterAmountIncreased()
        {
            var (service, game) = Setup();
            game.BoosterInventory.boosters.Add(BOOSTER_1, 99);
            Assert.AreEqual(99, game.BoosterInventory.boosters[BOOSTER_1]);

            service.AddBooster(BOOSTER_1, 1);

            Assert.AreEqual(100, game.BoosterInventory.boosters[BOOSTER_1]);
        }

        [Test]
        public void AddBooster_NegativeAmount_NoChange()
        {
            var (service, game) = Setup();
            game.BoosterInventory.boosters.Add(BOOSTER_1, 99);
            Assert.AreEqual(99, game.BoosterInventory.boosters[BOOSTER_1]);

            service.AddBooster(BOOSTER_1, -1);

            LogAssert.Expect(LogType.Warning, "Can't add or remove negative ammount of Boosters");
            Assert.AreEqual(99, game.BoosterInventory.boosters[BOOSTER_1]);
        }

        [Test]
        public void RemoveBooster_ExistingBooster_BoosterAmountDecreased()
        {
            var (service, game) = Setup();
            game.BoosterInventory.boosters.Add(BOOSTER_1, 99);

            service.RemoveBooster(BOOSTER_1, 9);

            Assert.IsTrue(game.BoosterInventory.boosters.ContainsKey(BOOSTER_1));
            Assert.AreEqual(90, game.BoosterInventory.boosters[BOOSTER_1]);
        }

        [Test]
        public void RemoveBooster_ExistingBoosterBelowZero_BoosterRemoved()
        {
            var (service, game) = Setup();
            game.BoosterInventory.boosters.Add(BOOSTER_1, 99);

            service.RemoveBooster(BOOSTER_1, 1111);

            Assert.IsTrue(game.BoosterInventory.boosters.ContainsKey(BOOSTER_1));
            Assert.AreEqual(0, game.BoosterInventory.boosters[BOOSTER_1]);
        }

        [Test]
        public void RemoveBooster_NegativeAmount_NoChange()
        {
            var (service, game) = Setup();
            game.BoosterInventory.boosters.Add(BOOSTER_1, 99);

            service.RemoveBooster(BOOSTER_1, -1);

            Assert.IsTrue(game.BoosterInventory.boosters.ContainsKey(BOOSTER_1));
            Assert.AreEqual(99, game.BoosterInventory.boosters[BOOSTER_1]);
        }

        [Test]
        public void RemoveBooster_NonExistingBooster_NoChange()
        {
            var (service, game) = Setup();
            Assert.IsFalse(game.BoosterInventory.boosters.ContainsKey(BOOSTER_1));

            service.RemoveBooster(BOOSTER_1, 9);

            LogAssert.Expect(LogType.Warning, "You have no booster of this type");
            Assert.IsFalse(game.BoosterInventory.boosters.ContainsKey(BOOSTER_1));
        }

        [Test]
        public void UseBooster_ExistingBooster_BoosterUsed()
        {
            var (service, game) = Setup();
            game.BoosterInventory.boosters.Add(BOOSTER_1, 99);

            service.UseBooster(BOOSTER_1, new(0, 0));

            Assert.AreEqual(1, boosterUsedCount);
        }

        [Test]
        public void UseBooster_NonExistingBooster_NoChange()
        {
            var (service, game) = Setup();
            Assert.IsFalse(game.BoosterInventory.boosters.ContainsKey(BOOSTER_1));

            service.UseBooster(BOOSTER_1, new(0, 0));

            LogAssert.Expect(LogType.Warning, "You have no booster of this type");
            Assert.AreEqual(0, boosterUsedCount);
        }

        [Test]
        public void UseBooster_ZeroAmountExists_NoChange()
        {
            var (service, game) = Setup();
            game.BoosterInventory.boosters.Add(BOOSTER_1, 0);

            service.UseBooster(BOOSTER_1, new(0, 0));

            LogAssert.Expect(LogType.Warning, "You have no booster of this type");
            Assert.AreEqual(0, boosterUsedCount);
        }

        [Test]
        public void GetBoosterAmount_ExistingBooster_AmountReturned()
        {
            var (service, game) = Setup();
            game.BoosterInventory.boosters.Add(BOOSTER_1, 99);

            int amount = service.GetBoosterAmount(BOOSTER_1);

            Assert.AreEqual(99, amount);
        }

        [Test]
        public void GetBoosterAmount_NonExistingBooster_ZeroReturned()
        {
            var (service, game) = Setup();
            Assert.IsFalse(game.BoosterInventory.boosters.ContainsKey(BOOSTER_1));

            int amount = service.GetBoosterAmount(BOOSTER_1);

            LogAssert.Expect(LogType.Warning, "You have no booster of this type");
            Assert.AreEqual(0, amount);
        }

        [Test]
        public void GetBoosterAmount_ZeroAmountExists_ZeroReturned()
        {
            var (service, game) = Setup();
            game.BoosterInventory.boosters.Add(BOOSTER_1, 0);

            int amount = service.GetBoosterAmount(BOOSTER_1);

            LogAssert.Expect(LogType.Warning, "You have no booster of this type");
            Assert.AreEqual(0, amount);
        }
    }
}