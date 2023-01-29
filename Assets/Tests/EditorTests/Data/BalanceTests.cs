using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using Model.Objects.UnitTests;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Data.UnitTests
{
    public class BalanceTests
    {
        [Test]
        public void GetRandomBlockType_1BasicType_BasicType()
        {
            List<BlockType_Weight> typesWeight = new();
            typesWeight.Add(new BlockType_Weight(new BlueBlockType(), 100));
            Balance balance = new Balance(typesWeight);

            ABlockType blockType = balance.GetRandomBlockType();

            Assert.AreEqual(typeof(BlueBlockType), blockType.GetType());
        }

        [Test]
        public void GetRandomBlockType_NullData_BasicType()
        {
            List<BlockType_Weight> typesWeight = new();
            Balance balance = new Balance(typesWeight);

            ABlockType blockType = balance.GetRandomBlockType();

            Assert.AreEqual(typeof(BasicBlockType), blockType.GetType());
        }

        [Test]
        public void GetRandomBlockType_2Types50persent_50persent()
        {
            List<BlockType_Weight> typesWeight = new();
            typesWeight.Add(new BlockType_Weight(new BlueBlockType(), 50));
            typesWeight.Add(new BlockType_Weight(new RedBlockType(), 50));
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
            List<BlockType_Weight> typesWeight = new();
            typesWeight.Add(new BlockType_Weight(new BlueBlockType(), 25));
            typesWeight.Add(new BlockType_Weight(new RedBlockType(), 25));
            typesWeight.Add(new BlockType_Weight(new BasicBlockType(), 25));
            typesWeight.Add(new BlockType_Weight(new TestBlockType(), 25));
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