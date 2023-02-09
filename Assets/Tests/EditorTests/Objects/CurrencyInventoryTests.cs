using System;
using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using Model.Systems;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Infrastructure.UnitTests
{
    public class CurrencyInventoryTests
    {
        [Test]
        public void AddCurrency_PositiveAmmount_GoldAdded()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new(type);

            inventory.AddCurrency(type, 100);

            Assert.AreEqual(100, inventory.GetAmmount(type));
        }

        [Test]
        public void AddCurrency_ZeroAmmount_GoldNotAdded()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new(type);

            inventory.AddCurrency(type, 0);

            Assert.AreEqual(0, inventory.GetAmmount(type));
            LogAssert.Expect(LogType.Error, "Can't add negative ammount of " + type);
        }

        [Test]
        public void AddCurrency_NegativeAmmount_GoldNotAdded()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new(type);

            inventory.AddCurrency(type, -555);

            Assert.AreEqual(0, inventory.GetAmmount(type));
            LogAssert.Expect(LogType.Error, "Can't add negative ammount of " + type);
        }

        [Test]
        public void TakeCurrency_PositiveAmmount_GoldRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new(type);
            inventory.AddCurrency(type, 100);

            inventory.TakeCurrency(type, 10);

            Assert.AreEqual(90, inventory.GetAmmount(type));
        }

        [Test]
        public void TakeCurrency_NotEnough_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new(type);

            inventory.TakeCurrency(type, 10);

            Assert.AreEqual(0, inventory.GetAmmount(type));
            LogAssert.Expect(LogType.Error, "Not enough " + type);
        }

        [Test]
        public void TakeCurrency_ZeroAmmount_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new(type);
            inventory.AddCurrency(type, 100);

            inventory.TakeCurrency(type, 0);

            Assert.AreEqual(100, inventory.GetAmmount(type));
            LogAssert.Expect(LogType.Error, "Can't remove negative ammount of " + type);
        }

        [Test]
        public void TakeCurrency_NegativeAmmount_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new(type);
            inventory.AddCurrency(type, 100);

            inventory.TakeCurrency(type, -10);

            Assert.AreEqual(100, inventory.GetAmmount(type));
            LogAssert.Expect(LogType.Error, "Can't remove negative ammount of " + type);
        }

        [Test]
        public void TakeCurrency_NoCurrencyOfType_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new();

            inventory.TakeCurrency(type, 10);

            Assert.AreEqual(0, inventory.GetAmmount(type));
            LogAssert.Expect(LogType.Error, "You have no " + type);
            LogAssert.Expect(LogType.Error, "You have no " + type);
        }

        [Test]
        public void GetAmmount_ValidType_ValidAmmount()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new(type);
            inventory.AddCurrency(type, 100);

            int ammount = inventory.GetAmmount(type);

            Assert.AreEqual(100, ammount);
        }

        [Test]
        public void GetAmmount_NoCurrencyOfType_Zero()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new();

            int ammount = inventory.GetAmmount(type);

            Assert.AreEqual(0, ammount);
            LogAssert.Expect(LogType.Error, "You have no " + type);
        }
    }
}