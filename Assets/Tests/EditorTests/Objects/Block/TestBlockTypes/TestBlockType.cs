using System.Collections;
using UnityEngine;

namespace Model.Objects.Tests
{
    public class TestBlockType : ABlockType
    {
        public override bool Activate()
        {
            return true;
        }
    }
}