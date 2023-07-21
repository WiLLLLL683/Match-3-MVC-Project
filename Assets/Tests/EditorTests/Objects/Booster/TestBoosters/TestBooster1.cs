using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects.UnitTests
{
    public class TestBooster1 : IBooster
    {
        public Sprite Icon => throw new NotImplementedException();

        public int Amount => throw new NotImplementedException();
    }
}