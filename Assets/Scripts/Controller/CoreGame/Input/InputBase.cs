using Model;
using System;
using UnityEngine;
using ViewElements;

namespace Controller
{
    public abstract class InputBase : MonoBehaviour
    {
        public abstract event Action<BlockView> OnTouchBegan;
        public abstract event Action<BlockView, Vector2> OnSwipeMoving;
        public abstract event Action<BlockView, Directions> OnSwipeEnded;
        public abstract event Action<BlockView> OnTap;

        public abstract void Enable();
        public abstract void Disable();
    }
}