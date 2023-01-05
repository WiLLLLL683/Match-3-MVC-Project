using System;
using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using Model.Systems;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.GameLogic.Tests
{
    public class InventoryTests
    {
        #region Gold
        [Test]
        public void AddGold_PositiveAmmount_GoldAdded()
        {
            Inventory inventory = new();
            
            inventory.AddGold(100);

            Assert.AreEqual(100, inventory.Gold);
        }

        [Test]
        public void AddGold_ZeroAmmount_GoldNotAdded()
        {
            Inventory inventory = new();

            inventory.AddGold(0);

            Assert.AreEqual(0, inventory.Gold);
            LogAssert.Expect(LogType.Error, "Can't add negative ammount of Gold");
        }

        [Test]
        public void AddGold_NegativeAmmount_GoldNotAdded()
        {
            Inventory inventory = new();

            inventory.AddGold(-555);

            Assert.AreEqual(0, inventory.Gold);
            LogAssert.Expect(LogType.Error, "Can't add negative ammount of Gold");
        }

        [Test]
        public void TakeGold_PositiveAmmount_GoldRemoved()
        {
            Inventory inventory = new();
            inventory.AddGold(100);

            inventory.TakeGold(10);

            Assert.AreEqual(90, inventory.Gold);
        }

        [Test]
        public void TakeGold_NotEnoughGold_GoldNotRemoved()
        {
            Inventory inventory = new();

            inventory.TakeGold(10);

            Assert.AreEqual(0, inventory.Gold);
            LogAssert.Expect(LogType.Error, "Not enough Gold");
        }

        [Test]
        public void TakeGold_ZeroAmmount_GoldNotRemoved()
        {
            Inventory inventory = new();
            inventory.AddGold(100);

            inventory.TakeGold(0);

            Assert.AreEqual(100, inventory.Gold);
            LogAssert.Expect(LogType.Error, "Can't remove negative ammount of Gold");
        }

        [Test]
        public void TakeGold_NegativeAmmount_GoldNotRemoved()
        {
            Inventory inventory = new();
            inventory.AddGold(100);

            inventory.TakeGold(-10);

            Assert.AreEqual(100, inventory.Gold);
            LogAssert.Expect(LogType.Error, "Can't remove negative ammount of Gold");
        }
        #endregion

        #region Boosters
        [Test]
        public void AddBooster_NewBooster_NewBoosterAdded()
        {
            Inventory inventory = new();

            Assert.AreEqual(0, inventory.GetBoosterAmmount<TestBooster1>());

            inventory.AddBooster<TestBooster1>(1);

            Assert.AreEqual(1, inventory.GetBoosterAmmount<TestBooster1>());
        }

        [Test]
        public void AddBooster_ExistingBooster_BoosterAmmountIncreased()
        {
            Inventory inventory = new();
            inventory.AddBooster<TestBooster1>(1);

            Assert.AreEqual(1, inventory.GetBoosterAmmount<TestBooster1>());

            inventory.AddBooster<TestBooster1>(1);

            Assert.AreEqual(2, inventory.GetBoosterAmmount<TestBooster1>());
        }

        [Test]
        public void TakeBooster_ExistingBooster_BoosterAmmountDecreased()
        {
            Inventory inventory = new();
            inventory.AddBooster<TestBooster1>(2);

            inventory.TakeBooster<TestBooster1>();

            Assert.AreEqual(1, inventory.GetBoosterAmmount<TestBooster1>());
        }

        [Test]
        public void TakeBooster_ExistingBooster_BoosterReturned()
        {
            Inventory inventory = new();
            inventory.AddBooster<TestBooster1>(1);

            IBooster booster = inventory.TakeBooster<TestBooster1>();

            Assert.That(booster is TestBooster1);
        }

        [Test]
        public void TakeBooster_NonExistingBooster_Error()
        {
            Inventory inventory = new();

            inventory.TakeBooster<TestBooster1>();

            LogAssert.Expect(LogType.Error, "You have no booster of this type");
        }
        #endregion
    }
}