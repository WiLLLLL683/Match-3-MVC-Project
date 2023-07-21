using System;
using UnityEngine;
using Data;
using Presenter;

namespace View
{
    /// <summary>
    /// Визуальный элемент блока, вызывается из IBlockPresenter
    /// </summary>
    public interface IBlockView
    {
        public GameObject gameObject { get; }

        public event Action<Directions> OnMove;
        public event Action OnActivate;

        //инициализация
        public void Init(ABlockType type, Vector2Int modelPosition);
        public void ChangeModelPosition(Vector2Int modelPosition);
        public void ChangeType(ABlockType blockType);
        //визуал
        public void PlayClickAnimation();
        public void PlayDestroyEffect();
    }
}