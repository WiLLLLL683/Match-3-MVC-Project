using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Objects.UnitTests
{
    public class BalanceTests
    {
        [Test]
        public void GetRandomBlockType_1BasicType_BasicType()
        {
            Dictionary<ABlockType, int> typesWeight = new Dictionary<ABlockType, int>();
            typesWeight.Add(new BlueBlockType(), 100);
            Balance balance = new Balance(typesWeight);

            ABlockType blockType = balance.GetRandomBlockType();

            Assert.AreEqual(typeof(BlueBlockType), blockType.GetType());
        }

        [Test]
        public void GetRandomBlockType_NullData_BasicType()
        {
            Dictionary<ABlockType, int> typesWeight = new Dictionary<ABlockType, int>();
            Balance balance = new Balance(typesWeight);

            ABlockType blockType = balance.GetRandomBlockType();

            Assert.AreEqual(typeof(BasicBlockType), blockType.GetType());
        }

        [Test]
        public void GetRandomBlockType_2Types50persent_50persent()
        {
            Dictionary<ABlockType, int> typesWeight = new Dictionary<ABlockType, int>();
            typesWeight.Add(new BlueBlockType(), 50);
            typesWeight.Add(new RedBlockType(), 50);
            Balance balance = new Balance(typesWeight);

            int blueCount = 0;
            int redCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                ABlockType blockType = balance.GetRandomBlockType();
                if (blockType is BlueBlockType)
                    blueCount++;
                if (blockType is RedBlockType)
                    redCount++;
            }

            Debug.Log("Blue persent = " + blueCount);
            Debug.Log("Red persent = " + redCount);
            Assert.That(blueCount >= 450);
            Assert.That(redCount >= 450);
            Assert.That(redCount + blueCount == 1000);
        }

        [Test]
        public void GetRandomBlockType_4Types25persent_25persent()
        {
            Dictionary<ABlockType, int> typesWeight = new Dictionary<ABlockType, int>();
            typesWeight.Add(new BlueBlockType(), 25);
            typesWeight.Add(new RedBlockType(), 25);
            typesWeight.Add(new BasicBlockType(), 25);
            typesWeight.Add(new TestBlockType(), 25);
            Balance balance = new Balance(typesWeight);

            int blueCount = 0;
            int redCount = 0;
            int basicCount = 0;
            int testCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                ABlockType blockType = balance.GetRandomBlockType();
                if (blockType is BlueBlockType)
                    blueCount++;
                if (blockType is RedBlockType)
                    redCount++;
                if (blockType is BasicBlockType)
                    basicCount++;
                if (blockType is TestBlockType)
                    testCount++;
            }

            Debug.Log("Blue persent = " + blueCount);
            Debug.Log("Red persent = " + redCount);
            Debug.Log("Basic persent = " + basicCount);
            Debug.Log("Test persent = " + testCount);
            Assert.That(blueCount >= 200);
            Assert.That(redCount >= 200);
            Assert.That(basicCount >= 200);
            Assert.That(testCount >= 200);
            Assert.That(redCount + blueCount + basicCount + testCount == 1000);
        }

    }
}