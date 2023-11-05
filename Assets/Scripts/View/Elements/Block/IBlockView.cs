﻿using System;
using UnityEngine;

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
        public void Init(Sprite iconSprite, ParticleSystem destroyEffectPrefab, Vector2Int modelPosition);
        public void ChangeModelPosition(Vector2Int modelPosition);
        public void ChangeType(Sprite iconSprite, ParticleSystem destroyEffectPrefab);
        //визуал
        public void PlayClickAnimation();
        public void PlayDestroyEffect();
    }
}