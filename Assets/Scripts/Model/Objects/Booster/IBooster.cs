using System;
using System.Collections;
using UnityEngine;

namespace Model.Objects
{
    public interface IBooster
    {
        public Sprite Icon { get; }
        public int Ammount { get; }
    }
}