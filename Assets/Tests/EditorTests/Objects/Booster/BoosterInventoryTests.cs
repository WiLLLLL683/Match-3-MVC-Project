using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Objects.UnitTests
{
    public class BoosterInventoryTests
    {
        [Test]
        public void AddBooster_NewBooster_NewBoosterAdded()
        {
            BoosterInventory inventory = new();

            Assert.AreEqual(0, inventory.GetBoosterAmount<TestBooster1>());

            inventory.AddBooster<TestBooster1>(1);

            Assert.AreEqual(1, inventory.GetBoosterAmount<TestBooster1>());
        }

        [Test]
        public void AddBooster_ExistingBooster_BoosterAmmountIncreased()
        {
            BoosterInventory inventory = new();
            inventory.AddBooster<TestBooster1>(1);

            Assert.AreEqual(1, inventory.GetBoosterAmount<TestBooster1>());

            inventory.AddBooster<TestBooster1>(1);

            Assert.AreEqual(2, inventory.GetBoosterAmount<TestBooster1>());
        }

        [Test]
        public void TakeBooster_ExistingBooster_BoosterAmmountDecreased()
        {
            BoosterInventory inventory = new();
            inventory.AddBooster<TestBooster1>(2);

            inventory.TakeBooster<TestBooster1>();

            Assert.AreEqual(1, inventory.GetBoosterAmount<TestBooster1>());
        }

        [Test]
        public void TakeBooster_ExistingBooster_BoosterReturned()
        {
            BoosterInventory inventory = new();
            inventory.AddBooster<TestBooster1>(1);

            IBooster booster = inventory.TakeBooster<TestBooster1>();

            Assert.That(booster is TestBooster1);
        }

        [Test]
        public void TakeBooster_NonExistingBooster_Error()
        {
            BoosterInventory inventory = new();

            inventory.TakeBooster<TestBooster1>();

            LogAssert.Expect(LogType.Error, "You have no booster of this type");
        }
    }
}