using Data;
using System.Collections;
using UnityEngine;

namespace Model.Objects.UnitTests
{
    public class TestBlockType : ABlockType
    {
        public TestBlockType(int id = 0) : base(id)
        {

        }

        public override bool Activate()
        {
            return true;
        }
    }
}