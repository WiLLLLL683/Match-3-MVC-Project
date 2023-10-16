using NUnit.Framework;
using UnityEngine;
using TestUtils;

namespace Model.Objects.UnitTests
{
    public class BlockTests
    {
        [Test]
        public void ChangePosition_NewPos_PositionEqNewPos()
        {
            Block block = TestBlockFactory.CreateBlock(TestBlockFactory.DEFAULT_BLOCK, new(0, 0));
            Vector2Int newPos = new(0, 1);

            block.SetPosition(newPos);

            Assert.AreEqual(newPos, block.Position);
        }

        [Test]
        public void ChangeType_NewType_TypeEqNewType()
        {
            Block block = TestBlockFactory.CreateBlock(TestBlockFactory.DEFAULT_BLOCK, new(0, 0));
            BlockType newType = TestBlockFactory.RedBlockType;

            block.SetType(newType);

            Assert.AreEqual(newType, block.Type);
        }

        [Test]
        public void Activate_CurrentType_Activated()
        {
            Block block = TestBlockFactory.CreateBlock(new TestBlockType(), new(0, 0));

            bool isActivated = block.Activate();

            Assert.AreEqual(true, isActivated);
        }

        [Test]
        public void Activate_NewType_ActivatedNewType()
        {
            Block block = TestBlockFactory.CreateBlock(new TestBlockType(), new(0, 0));

            bool beforeChange = block.Activate();
            block.SetType(TestBlockFactory.RedBlockType);
            bool afterChange = block.Activate();

            Assert.AreNotEqual(beforeChange,afterChange);
            Assert.AreEqual(true, beforeChange);
            Assert.AreEqual(false, afterChange);
        }
    }
}