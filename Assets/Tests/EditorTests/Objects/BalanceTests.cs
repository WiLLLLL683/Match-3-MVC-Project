using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnitTests;

namespace Model.Objects.UnitTests
{
    public class BalanceTests
    {
        [Test]
        public void GetRandomBlockType_1BasicType_BasicType()
        {
            var balance = TestUtils.CreateBalance(TestUtils.RED_BLOCK);

            BlockType blockType = balance.GetRandomBlockType();

            Assert.AreEqual(typeof(BasicBlockType), blockType.GetType());
            Assert.AreEqual(blockType.Id, TestUtils.RED_BLOCK);
        }

        [Test]
        public void GetRandomBlockType_NullData_DefaultType()
        {
            var balance = TestUtils.CreateBalance();

            BlockType blockType = balance.GetRandomBlockType();

            Assert.AreEqual(typeof(BasicBlockType), blockType.GetType());
            Assert.AreEqual(blockType.Id, TestUtils.DEFAULT_BLOCK);
        }

        [Test]
        public void GetRandomBlockType_2Types50to50percent()
        {
            var balance = TestUtils.CreateBalance(TestUtils.RED_BLOCK, TestUtils.BLUE_BLOCK);

            int blueCount = 0;
            int redCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                BlockType blockType = balance.GetRandomBlockType();
                if (blockType.Id == TestUtils.BLUE_BLOCK)
                    blueCount++;
                if (blockType.Id == TestUtils.RED_BLOCK)
                    redCount++;
            }

            Debug.Log("Blue persent = " + blueCount);
            Debug.Log("Red persent = " + redCount);
            Assert.That(blueCount >= 450);
            Assert.That(redCount >= 450);
            Assert.That(redCount + blueCount == 1000);
        }

        [Test]
        public void GetRandomBlockType_4Types25to25percent()
        {
            var balance = TestUtils.CreateBalance(TestUtils.RED_BLOCK, TestUtils.BLUE_BLOCK, TestUtils.GREEN_BLOCK, TestUtils.YELLOW_BLOCK);
            int blueCount = 0;
            int redCount = 0;
            int greenCount = 0;
            int yellowCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                BlockType blockType = balance.GetRandomBlockType();
                if (blockType.Id == TestUtils.BLUE_BLOCK)
                    blueCount++;
                if (blockType.Id == TestUtils.RED_BLOCK)
                    redCount++;
                if (blockType.Id == TestUtils.GREEN_BLOCK)
                    greenCount++;
                if (blockType.Id == TestUtils.YELLOW_BLOCK)
                    yellowCount++;
            }

            Debug.Log("Blue persent = " + blueCount);
            Debug.Log("Red persent = " + redCount);
            Debug.Log("Green persent = " + greenCount);
            Debug.Log("Yellow persent = " + yellowCount);
            Assert.That(blueCount >= 200);
            Assert.That(redCount >= 200);
            Assert.That(greenCount >= 200);
            Assert.That(yellowCount >= 200);
            Assert.That(redCount + blueCount + greenCount + yellowCount == 1000);
        }

    }
}