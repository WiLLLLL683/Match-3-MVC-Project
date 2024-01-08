using Model.Objects;
using Model.Services;
using NUnit.Framework;
using TestUtils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Services.UnitTests
{
    public class CurrencyServiceTests
    {
        [Test]
        public void AddCurrency_PositiveAmount_GoldAdded()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyService service = TestServicesFactory.CreateCurrencyService(type, 0);

            service.AddCurrency(type, 100);

            Assert.AreEqual(100, service.GetAmount(type));
        }

        [Test]
        public void AddCurrency_NegativeAmount_GoldNotAdded()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyService service = TestServicesFactory.CreateCurrencyService(type, 0);

            service.AddCurrency(type, -555);

            Assert.AreEqual(0, service.GetAmount(type));
            LogAssert.Expect(LogType.Error, "Can't add negative ammount of " + type);
        }

        [Test]
        public void TakeCurrency_PositiveAmount_GoldRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyService service = TestServicesFactory.CreateCurrencyService(type, 100);

            service.SpendCurrency(type, 10);

            Assert.AreEqual(90, service.GetAmount(type));
        }

        [Test]
        public void TakeCurrency_NotEnough_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyService service = TestServicesFactory.CreateCurrencyService(type, 0);

            service.SpendCurrency(type, 10);

            Assert.AreEqual(0, service.GetAmount(type));
            LogAssert.Expect(LogType.Error, "Not enough " + type);
        }

        [Test]
        public void TakeCurrency_ZeroAmount_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyService service = TestServicesFactory.CreateCurrencyService(type, 100);

            service.SpendCurrency(type, 0);

            Assert.AreEqual(100, service.GetAmount(type));
            LogAssert.Expect(LogType.Error, "Can't remove negative ammount of " + type);
        }

        [Test]
        public void TakeCurrency_NegativeAmount_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyService service = TestServicesFactory.CreateCurrencyService(type, 100);

            service.SpendCurrency(type, -10);

            Assert.AreEqual(100, service.GetAmount(type));
            LogAssert.Expect(LogType.Error, "Can't remove negative ammount of " + type);
        }

        [Test]
        public void TakeCurrency_NoCurrencyOfType_GoldNotRemoved()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyService service = new(new Game());

            service.SpendCurrency(type, 10);

            Assert.AreEqual(0, service.GetAmount(type));
            LogAssert.Expect(LogType.Error, "You have no " + type);
            LogAssert.Expect(LogType.Error, "You have no " + type);
        }

        [Test]
        public void GetAmount_ValidType_ValidAmount()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyService service = TestServicesFactory.CreateCurrencyService(type, 100);

            int ammount = service.GetAmount(type);

            Assert.AreEqual(100, ammount);
        }

        [Test]
        public void GetAmount_NoCurrencyOfType_Zero()
        {
            CurrencyType type = CurrencyType.Gold;
            CurrencyService service = new(new Game());

            int ammount = service.GetAmount(type);

            Assert.AreEqual(0, ammount);
            LogAssert.Expect(LogType.Error, "You have no " + type);
        }
    }
}