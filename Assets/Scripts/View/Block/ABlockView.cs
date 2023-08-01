using System;
using UnityEngine;
using Data;
using Presenter;

namespace View
{
    /// <summary>
    /// Визуальный элемент блока, вызывается из IBlockPresenter
    /// </summary>
    public abstract class ABlockView : MonoBehaviour
    {
        //public GameObject gameObject { get; }

        public abstract event Action<Directions> OnMove;
        public abstract event Action OnActivate;

        //инициализация
        public abstract void Init(ABlockType type, Vector2Int modelPosition);
        public abstract void ChangeModelPosition(Vector2Int modelPosition);
        public abstract void ChangeType(ABlockType blockType);
        //визуал
        public abstract void PlayClickAnimation();
        public abstract void PlayDestroyEffect();
    }
}