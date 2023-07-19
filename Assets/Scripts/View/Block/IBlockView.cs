using System;
using UnityEngine;
using Data;
using Presenter;

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

        //инициализация
        public void Init(ABlockType type, Vector2Int modelPosition, IGameBoardPresenter gameBoardPresenter);
        public void SetModelPosition(Vector2Int modelPosition);
        public void SetType(ABlockType blockType);
        //визуал
        public void PlayClickAnimation();
        public void PlayDestroyEffect();
    }
}