using Model;
using System;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IInput
    {
        public GameObject gameObject { get; }
        
        public abstract event Action<IBlockPresenter> OnTouchBegan;
        public abstract event Action<IBlockPresenter, Vector2> OnSwipeMoving;
        public abstract event Action<IBlockPresenter, Directions> OnSwipeEnded;
        public abstract event Action<IBlockPresenter> OnTap;

        public abstract void Enable();
        public abstract void Disable();
    }
}