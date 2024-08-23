using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using TestUtils;
using Model.Objects;
using NSubstitute;
using Config;
using UnityEditor.Build.Player;

namespace Model.Factories.UnitTests
{
    //public class BlockTypeFactoryTests
    //{
    //    [Test]
    //    public void GetRandomBlockType_1BasicType_BasicType()
    //    {
    //        var service = new BlockTypeFactory(null, null, null, null, null);
    //        service.SetLevelConfig(TestServicesFactory.CreateListOfWeights(TestBlockFactory.RED_BLOCK), TestBlockFactory.DefaultBlockType);

    //        IBlockType blockType = service.CreateRandom();

    //        Assert.AreEqual(typeof(BasicBlockType), blockType.GetType());
    //        Assert.AreEqual(blockType.Id, TestBlockFactory.RED_BLOCK);
    //    }

    //    [Test]
    //    public void GetRandomBlockType_NullData_DefaultType()
    //    {
    //        var service = new BlockTypeFactory(null, null, null, null, null);
    //        service.SetLevelConfig(new List<BlockType_Weight>(), TestBlockFactory.DefaultBlockType);

    //        IBlockType blockType = service.CreateRandom();

    //        Assert.AreEqual(typeof(BasicBlockType), blockType.GetType());
    //        Assert.AreEqual(blockType.Id, TestBlockFactory.DEFAULT_BLOCK);
    //    }

    //    [Test]
    //    public void GetRandomBlockType_2Types50to50percent()
    //    {
    //        var service = new BlockTypeFactory(null, null, null, null, null);
    //        service.SetLevelConfig(TestServicesFactory.CreateListOfWeights(TestBlockFactory.RED_BLOCK, TestBlockFactory.BLUE_BLOCK), TestBlockFactory.DefaultBlockType);

    //        int blueCount = 0;
    //        int redCount = 0;
    //        for (int i = 0; i < 1000; i++)
    //        {
    //            IBlockType blockType = service.CreateRandom();
    //            if (blockType.Id == TestBlockFactory.BLUE_BLOCK)
    //                blueCount++;
    //            if (blockType.Id == TestBlockFactory.RED_BLOCK)
    //                redCount++;
    //        }

    //        Debug.Log("Blue persent = " + blueCount);
    //        Debug.Log("Red persent = " + redCount);
    //        Assert.That(blueCount >= 450);
    //        Assert.That(redCount >= 450);
    //        Assert.That(redCount + blueCount == 1000);
    //    }

    //    [Test]
    //    public void GetRandomBlockType_4Types25to25percent()
    //    {
    //        var service = new BlockTypeFactory(null, null, null, null, null);
    //        service.SetLevelConfig(TestServicesFactory.CreateListOfWeights(TestBlockFactory.RED_BLOCK, TestBlockFactory.BLUE_BLOCK, TestBlockFactory.GREEN_BLOCK, TestBlockFactory.YELLOW_BLOCK), TestBlockFactory.DefaultBlockType);

    //        int blueCount = 0;
    //        int redCount = 0;
    //        int greenCount = 0;
    //        int yellowCount = 0;

    //        for (int i = 0; i < 1000; i++)
    //        {
    //            IBlockType blockType = service.CreateRandom();
    //            if (blockType.Id == TestBlockFactory.BLUE_BLOCK)
    //                blueCount++;
    //            if (blockType.Id == TestBlockFactory.RED_BLOCK)
    //                redCount++;
    //            if (blockType.Id == TestBlockFactory.GREEN_BLOCK)
    //                greenCount++;
    //            if (blockType.Id == TestBlockFactory.YELLOW_BLOCK)
    //                yellowCount++;
    //        }

    //        Debug.Log("Blue persent = " + blueCount);
    //        Debug.Log("Red persent = " + redCount);
    //        Debug.Log("Green persent = " + greenCount);
    //        Debug.Log("Yellow persent = " + yellowCount);
    //        Assert.That(blueCount >= 200);
    //        Assert.That(redCount >= 200);
    //        Assert.That(greenCount >= 200);
    //        Assert.That(yellowCount >= 200);
    //        Assert.That(redCount + blueCount + greenCount + yellowCount == 1000);
    //    }

    //}
}