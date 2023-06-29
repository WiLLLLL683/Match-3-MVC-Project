﻿using Model;
using System;
using UnityEngine;
using ViewElements;

namespace Controller
{
    public abstract class InputBase : MonoBehaviour
    {
        public abstract event Action<IBlockController> OnTouchBegan;
        public abstract event Action<IBlockController, Vector2> OnSwipeMoving;
        public abstract event Action<IBlockController, Directions> OnSwipeEnded;
        public abstract event Action<IBlockController> OnTap;

        public abstract void Enable();
        public abstract void Disable();
    }
}