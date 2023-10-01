using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using System;
using Config;

namespace Model.Objects.UnitTests
{
    public class BlockTests
    {
        [Test]
        public void ChangePosition_NewPos_PositionEqNewPos()
        {
            Block block = CreateBlock();
            Vector2Int newPos = new Vector2Int(0, 1);

            block.SetPosition(newPos);

            Assert.AreEqual(newPos, block.Position);
        }

        [Test]
        public void ChangeType_NewType_TypeEqNewType()
        {
            Block block = CreateBlock();
            IBlockType newType = new BasicBlockType();

            block.SetType(newType);

            Assert.AreEqual(newType, block.Type);
        }

        [Test]
        public void Activate_CurrentType_Activated()
        {
            Block block = CreateBlock();

            bool isActivated = block.Activate();

            Assert.AreEqual(true, isActivated);
        }

        [Test]
        public void Activate_NewType_ActivatedNewType()
        {
            Block block = CreateBlock();

            bool beforeChange = block.Activate();
            block.SetType(new BasicBlockType());
            bool afterChange = block.Activate();

            Assert.AreNotEqual(beforeChange,afterChange);
            Assert.AreEqual(true, beforeChange);
            Assert.AreEqual(false, afterChange);
        }

        [Test]
        public void Destroy_Block_DestroyEvent()
        {
            Block block = CreateBlock();
            Block test = CreateBlock();
            void TestFunc(Block sender)
            {
                test = sender;
            }

            block.OnDestroy += TestFunc;
            try
            {
                block.Destroy();
            }
            finally
            {
                block.OnDestroy -= TestFunc;
            }

            Assert.AreSame(block,test);
        }

        private static Block CreateBlock()
        {
            Cell cellA = new Cell(new BasicCellType(), new Vector2Int(0, 0));
            Block block = new Block(new TestBlockType(), cellA.Position);
            return block;
        }

    }
}