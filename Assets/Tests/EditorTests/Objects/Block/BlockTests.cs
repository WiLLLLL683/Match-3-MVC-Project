using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Model.Objects;
using System;
using Data;

namespace Model.Objects.UnitTests
{
    public class BlockTests
    {
        [Test]
        public void ChangePosition_NewPos_PositionEqNewPos()
        {
            Block block = CreateBlock();
            Cell cellB = new Cell(new BasicCellType(), new Vector2Int(0, 1));

            block.ChangePosition(cellB);

            Assert.AreEqual(cellB, block.Cell);
        }

        [Test]
        public void ChangeType_NewType_TypeEqNewType()
        {
            Block block = CreateBlock();
            ABlockType newType = new BasicBlockType();

            block.ChangeType(newType);

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
            block.ChangeType(new BasicBlockType());
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
            Block block = new Block(new TestBlockType(), cellA);
            return block;
        }

    }
}