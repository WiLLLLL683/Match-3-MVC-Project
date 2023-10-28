using Model.Objects;
using Model.Objects.UnitTests;
using Model.Services;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Services.UnitTests
{
    public class BoosterServiceTests
    {
        [Test]
        public void AddBooster_NewBooster_NewBoosterAdded()
        {
            BoosterService inventory = new();

            Assert.AreEqual(0, inventory.GetBoosterAmount<TestBooster1>());

            inventory.AddBooster<TestBooster1>(1);

            Assert.AreEqual(1, inventory.GetBoosterAmount<TestBooster1>());
        }

        [Test]
        public void AddBooster_ExistingBooster_BoosterAmountIncreased()
        {
            BoosterService inventory = new();
            inventory.AddBooster<TestBooster1>(1);

            Assert.AreEqual(1, inventory.GetBoosterAmount<TestBooster1>());

            inventory.AddBooster<TestBooster1>(1);

            Assert.AreEqual(2, inventory.GetBoosterAmount<TestBooster1>());
        }

        [Test]
        public void SpendBooster_ExistingBooster_BoosterAmountDecreased()
        {
            BoosterService inventory = new();
            inventory.AddBooster<TestBooster1>(2);

            inventory.SpendBooster<TestBooster1>();

            Assert.AreEqual(1, inventory.GetBoosterAmount<TestBooster1>());
        }

        [Test]
        public void SpendBooster_ExistingBooster_BoosterReturned()
        {
            BoosterService inventory = new();
            inventory.AddBooster<TestBooster1>(1);

            IBooster booster = inventory.SpendBooster<TestBooster1>();

            Assert.That(booster is TestBooster1);
        }

        [Test]
        public void SpendBooster_NonExistingBooster_Error()
        {
            BoosterService inventory = new();

            inventory.SpendBooster<TestBooster1>();

            LogAssert.Expect(LogType.Error, "You have no booster of this type");
        }
    }
}