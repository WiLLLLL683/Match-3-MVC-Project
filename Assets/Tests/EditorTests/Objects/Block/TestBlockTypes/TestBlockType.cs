using Config;
using System.Collections;
using UnityEngine;

namespace Model.Objects.UnitTests
{
    public class TestBlockType : BlockType
    {
        public TestBlockType(int id = 0)
        {

        }

        public override bool Activate()
        {
            return true;
        }
    }
}