using Model;
using System;
using UnityEngine;
using View;

namespace Controller
{
    public abstract class InputBase : MonoBehaviour
    {
        public abstract event Action<Block> OnTouchBegan;
        public abstract event Action<Block, Vector2> OnSwipeMoving;
        public abstract event Action<Block, Directions> OnSwipeEnded;
        public abstract event Action<Block> OnTap;

        public abstract void Enable();
        public abstract void Disable();
    }
}