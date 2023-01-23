using System;
using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using Model.Systems;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Infrastructure.Tests
{
    public class CurrencyInventoryTests
    {
        [Test]
        public void AddGold_PositiveAmmount_GoldAdded()
        {
            CurrencyInventory inventory = new();
            
            inventory.AddGold(100);

            Assert.AreEqual(100, inventory.Gold);
        }

        [Test]
        public void AddGold_ZeroAmmount_GoldNotAdded()
        {
            CurrencyInventory inventory = new();

            inventory.AddGold(0);

            Assert.AreEqual(0, inventory.Gold);
            LogAssert.Expect(LogType.Error, "Can't add negative ammount of Gold");
        }

        [Test]
        public void AddGold_NegativeAmmount_GoldNotAdded()
        {
            CurrencyInventory inventory = new();

            inventory.AddGold(-555);

            Assert.AreEqual(0, inventory.Gold);
            LogAssert.Expect(LogType.Error, "Can't add negative ammount of Gold");
        }

        [Test]
        public void TakeGold_PositiveAmmount_GoldRemoved()
        {
            CurrencyInventory inventory = new();
            inventory.AddGold(100);

            inventory.TakeGold(10);

            Assert.AreEqual(90, inventory.Gold);
        }

        [Test]
        public void TakeGold_NotEnoughGold_GoldNotRemoved()
        {
            CurrencyInventory inventory = new();

            inventory.TakeGold(10);

            Assert.AreEqual(0, inventory.Gold);
            LogAssert.Expect(LogType.Error, "Not enough Gold");
        }

        [Test]
        public void TakeGold_ZeroAmmount_GoldNotRemoved()
        {
            CurrencyInventory inventory = new();
            inventory.AddGold(100);

            inventory.TakeGold(0);

            Assert.AreEqual(100, inventory.Gold);
            LogAssert.Expect(LogType.Error, "Can't remove negative ammount of Gold");
        }

        [Test]
        public void TakeGold_NegativeAmmount_GoldNotRemoved()
        {
            CurrencyInventory inventory = new();
            inventory.AddGold(100);

            inventory.TakeGold(-10);

            Assert.AreEqual(100, inventory.Gold);
            LogAssert.Expect(LogType.Error, "Can't remove negative ammount of Gold");
        }
    }
}