using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using System;

namespace Model.Objects.Tests
{
    public class BlockTests
    {
        [Test]
        public void SetPosition_NewPos_PositionEqNewPos()
        {
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));
            Vector2Int newPosition = new Vector2Int(1, 0);

            block.SetPosition(newPosition);

            Assert.AreEqual(newPosition, block.position);
        }

        [Test]
        public void ChangeType_NewType_TypeEqNewType()
        {
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));
            ABlockType newType = new BlueBlockType();

            block.ChangeType(newType);

            Assert.AreEqual(newType, block.type);
        }

        [Test]
        public void Activate_CurrentType_Activated()
        {
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));
            RedBlockType expectedType = new RedBlockType();
            expectedType.Activate();
            string expected = expectedType.testString;

            string beforeAct = block.type.testString;
            block.Activate();
            string afterAct = block.type.testString;

            Assert.AreNotEqual(beforeAct, afterAct);
            Assert.AreEqual(expected, afterAct);
        }

        [Test]
        public void Activate_NewType_ActivatedNewType()
        {
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));
            BlueBlockType expectedType = new BlueBlockType();
            expectedType.Activate();
            string expected = expectedType.testString;

            block.Activate();
            string beforeChange = block.type.testString;
            block.ChangeType(new BlueBlockType());
            block.Activate();
            string afterChange = block.type.testString;

            Assert.AreNotEqual(beforeChange,afterChange);
            Assert.AreEqual(expected, afterChange);
        }

        [Test]
        public void Destroy_Block_DestroyEvent()
        {
            Block block = new Block(new RedBlockType(), new Vector2Int(0,0));
            Block test = new Block(new BlueBlockType(), new Vector2Int(0, 0));
            void TestFunc(Block sender, EventArgs eventArgs)
            {
                test = sender;
            }

            block.OnDestroyEvent += TestFunc;
            try
            {
                block.Destroy();
            }
            finally
            {
                block.OnDestroyEvent -= TestFunc;
            }

            Assert.AreSame(block,test);
        }
    }
}