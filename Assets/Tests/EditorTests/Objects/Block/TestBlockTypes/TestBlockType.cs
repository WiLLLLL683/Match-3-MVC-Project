using Data;
using System.Collections;
using UnityEngine;

namespace Model.Objects.UnitTests
{
    public class TestBlockType : ABlockType
    {
        public override bool Activate()
        {
            return true;
        }
    }
}