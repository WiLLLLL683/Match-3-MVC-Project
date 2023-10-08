using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using System;
using Config;
using UnitTests;

namespace Model.Objects.UnitTests
{
    public class BlockTests
    {
        [Test]
        public void ChangePosition_NewPos_PositionEqNewPos()
        {
            Block block = TestUtils.CreateBlock(TestUtils.DEFAULT_BLOCK, new(0, 0));
            Vector2Int newPos = new(0, 1);

            block.SetPosition(newPos);

            Assert.AreEqual(newPos, block.Position);
        }

        [Test]
        public void ChangeType_NewType_TypeEqNewType()
        {
            Block block = TestUtils.CreateBlock(TestUtils.DEFAULT_BLOCK, new(0, 0));
            BlockType newType = TestUtils.RedBlockType;

            block.SetType(newType);

            Assert.AreEqual(newType, block.Type);
        }

        [Test]
        public void Activate_CurrentType_Activated()
        {
            Block block = TestUtils.CreateBlock(new TestBlockType(), new(0, 0));

            bool isActivated = block.Activate();

            Assert.AreEqual(true, isActivated);
        }

        [Test]
        public void Activate_NewType_ActivatedNewType()
        {
            Block block = TestUtils.CreateBlock(new TestBlockType(), new(0, 0));

            bool beforeChange = block.Activate();
            block.SetType(TestUtils.RedBlockType);
            bool afterChange = block.Activate();

            Assert.AreNotEqual(beforeChange,afterChange);
            Assert.AreEqual(true, beforeChange);
            Assert.AreEqual(false, afterChange);
        }

        [Test]
        public void Destroy_Block_DestroyEvent()
        {
            Block block = TestUtils.CreateBlock(TestUtils.DEFAULT_BLOCK, new(0, 0));
            Block test = null;
            void TestFunc(Block sender) => test = sender;

            block.OnDestroy += TestFunc;
            block.Destroy();
            block.OnDestroy -= TestFunc;

            Assert.AreSame(block,test);
        }
    }
}