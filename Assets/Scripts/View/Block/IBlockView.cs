using System;
using UnityEngine;
using Data;

namespace View
{
    /// <summary>
    /// Вью для блока, вызывается из IBlockPresenter
    /// </summary>
    public interface IBlockView
    {
        public GameObject gameObject { get; }

        public abstract event Action<Directions> OnMove;
        public abstract event Action OnActivate;
        public abstract event Action<Directions, Vector2> OnDrag;

        //инициализация
        public void Init(ABlockType type, Vector2 modelPosition);
        public void SetModelPosition(Vector2 modelPosition);
        public void SetType(ABlockType blockType);
        //визуал
        public void DragPosition(Vector2 deltaPosition);
        public void ReturnToModelPosition();
        public void PlayClickAnimation();
        public void PlayDestroyEffect();
    }
}