using Model.Objects;
using NUnit.Framework;
using TestUtils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Infrastructure.UnitTests
{
    public class CurrencyInventoryTests
    {
        [Test]
        public void AddCurrency_PositiveAmount_GoldAdded()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = TestServicesFactory.CreateCurrencyInventory(type, 0);

            inventory.AddCurrency(type, 100);

            Assert.AreEqual(100, inventory.GetAmount(type));
        }

        [Test]
        public void AddCurrency_ZeroAmount_GoldNotAdded()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = TestServicesFactory.CreateCurrencyInventory(type, 0);

            inventory.AddCurrency(type, 0);

            Assert.AreEqual(0, inventory.GetAmount(type));
            LogAssert.Expect(LogType.Error, "Can't add negative ammount of " + type);
        }

        [Test]
        public void AddCurrency_NegativeAmount_GoldNotAdded()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = TestServicesFactory.CreateCurrencyInventory(type, 0);

            inventory.AddCurrency(type, -555);

            Assert.AreEqual(0, inventory.GetAmount(type));
            LogAssert.Expect(LogType.Error, "Can't add negative ammount of " + type);
        }

        [Test]
        public void TakeCurrency_PositiveAmount_GoldRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = TestServicesFactory.CreateCurrencyInventory(type, 100);

            inventory.SpendCurrency(type, 10);

            Assert.AreEqual(90, inventory.GetAmount(type));
        }

        [Test]
        public void TakeCurrency_NotEnough_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = TestServicesFactory.CreateCurrencyInventory(type, 0);

            inventory.SpendCurrency(type, 10);

            Assert.AreEqual(0, inventory.GetAmount(type));
            LogAssert.Expect(LogType.Error, "Not enough " + type);
        }

        [Test]
        public void TakeCurrency_ZeroAmount_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = TestServicesFactory.CreateCurrencyInventory(type, 100);

            inventory.SpendCurrency(type, 0);

            Assert.AreEqual(100, inventory.GetAmount(type));
            LogAssert.Expect(LogType.Error, "Can't remove negative ammount of " + type);
        }

        [Test]
        public void TakeCurrency_NegativeAmount_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = TestServicesFactory.CreateCurrencyInventory(type, 100);

            inventory.SpendCurrency(type, -10);

            Assert.AreEqual(100, inventory.GetAmount(type));
            LogAssert.Expect(LogType.Error, "Can't remove negative ammount of " + type);
        }

        [Test]
        public void TakeCurrency_NoCurrencyOfType_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new();

            inventory.SpendCurrency(type, 10);

            Assert.AreEqual(0, inventory.GetAmount(type));
            LogAssert.Expect(LogType.Error, "You have no " + type);
            LogAssert.Expect(LogType.Error, "You have no " + type);
        }

        [Test]
        public void GetAmount_ValidType_ValidAmount()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = TestServicesFactory.CreateCurrencyInventory(type, 100);

            int ammount = inventory.GetAmount(type);

            Assert.AreEqual(100, ammount);
        }

        [Test]
        public void GetAmount_NoCurrencyOfType_Zero()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyInventory inventory = new();

            int ammount = inventory.GetAmount(type);

            Assert.AreEqual(0, ammount);
            LogAssert.Expect(LogType.Error, "You have no " + type);
        }
    }
}